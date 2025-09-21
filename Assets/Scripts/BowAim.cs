using UnityEngine;
using UnityEngine.UI;

public class BowAim : MonoBehaviour
{
    public Image aimCircle;          // Assign in Inspector
    public Camera playerCamera;      // Camera attached to player
    public float aimDistance = 20f;  // Default distance if no target
    public LayerMask targetLayer;    // Monsters layer

    public Color normalColor = Color.white;
    public Color targetColor = Color.red;

    private bool isAiming = false;

    void Update()
    {
        // Toggle aim button (Right Mouse Button)
        if (Input.GetMouseButtonDown(1))
        {
            isAiming = true;
            aimCircle.gameObject.SetActive(true);
        }
        if (Input.GetMouseButtonUp(1))
        {
            isAiming = false;
            aimCircle.gameObject.SetActive(false);
        }

        if (isAiming)
        {
            UpdateAimCircle();
        }
    }

    void UpdateAimCircle()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        Vector3 targetPos;

        if (Physics.Raycast(ray, out hit, 100f, targetLayer))
        {
            // Hit a monster → move circle to hit point & change color
            targetPos = hit.point;
            aimCircle.color = targetColor;
        }
        else
        {
            // No monster → circle at default forward position & normal color
            targetPos = playerCamera.transform.position + playerCamera.transform.forward * aimDistance;
            aimCircle.color = normalColor;
        }

        // Convert world position to screen space
        Vector3 screenPos = playerCamera.WorldToScreenPoint(targetPos);
        aimCircle.transform.position = screenPos;
    }
}
