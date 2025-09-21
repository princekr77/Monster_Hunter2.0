using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    [System.Serializable]
    public class TargetType
    {
        public string tag;                // e.g. "Monster", "Boss", "Treasure"
        public GameObject iconPrefab;     // prefab to represent this type on minimap
    }

    [Header("References")]
    public Transform player;
    public Camera minimapCam;
    public RectTransform minimapRect;       // UI panel of minimap
    public RectTransform playerIcon;        // player arrow/circle in minimap
    public RectTransform iconsContainer;    // parent for target icons

    [Header("Settings")]
    public float height = 30f;              // camera height above player
    public bool rotateWithPlayer = true;    // rotate camera or not
    public List<TargetType> targetTypes;    // list of types to detect

    private Dictionary<Transform, RectTransform> targetIcons = new Dictionary<Transform, RectTransform>();

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player").transform;

        FindAllTargets();
    }

    void LateUpdate()
    {
        if (player == null) return;

        // --- Camera follows player ---
        Vector3 newPos = player.position;
        newPos.y += height;
        transform.position = newPos;

        if (rotateWithPlayer)
            transform.rotation = Quaternion.Euler(90f, player.eulerAngles.y, 0f);
        else
            transform.rotation = Quaternion.Euler(90f, 0f, 0f);

        // --- Player icon ---
        if (playerIcon != null)
        {
            playerIcon.anchoredPosition = Vector2.zero; // always center
            playerIcon.localEulerAngles = new Vector3(0, 0, -player.eulerAngles.y); // rotate with player
        }

        // --- Update Target Icons ---
        List<Transform> destroyed = new List<Transform>();
        foreach (var kvp in targetIcons)
        {
            Transform target = kvp.Key;
            RectTransform icon = kvp.Value;

            if (target != null)
            {
                // World position → viewport (0..1)
                Vector3 viewportPos = minimapCam.WorldToViewportPoint(target.position);

                // Optional: clamp inside panel
                viewportPos.x = Mathf.Clamp01(viewportPos.x);
                viewportPos.y = Mathf.Clamp01(viewportPos.y);

                // Convert viewport to local position in panel
                float x = (viewportPos.x - 0.5f) * minimapRect.rect.width;
                float y = (viewportPos.y - 0.5f) * minimapRect.rect.height;

                icon.localPosition = new Vector3(x, y, 0f);
            }
            else
            {
                destroyed.Add(target);
            }
        }

        // Remove destroyed icons
        foreach (Transform t in destroyed)
        {
            if (targetIcons[t] != null)
                Destroy(targetIcons[t].gameObject);
            targetIcons.Remove(t);
        }
    }


    // --- Auto detect all targets based on type list ---
    public void FindAllTargets()
    {
        targetIcons.Clear();

        foreach (TargetType type in targetTypes)
        {
            GameObject[] targets = GameObject.FindGameObjectsWithTag(type.tag);
            foreach (GameObject t in targets)
            {
                AddTarget(t.transform, type.iconPrefab);
            }
        }
    }

    // --- Add single target ---
    public void AddTarget(Transform target, GameObject iconPrefab)
    {
        if (iconsContainer == null || iconPrefab == null) return;

        GameObject iconObj = Instantiate(iconPrefab, iconsContainer);
        RectTransform icon = iconObj.GetComponent<RectTransform>();
        targetIcons[target] = icon;
    }
}
