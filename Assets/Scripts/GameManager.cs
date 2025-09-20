using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public GameObject WinUI;

    [Header("Monster Tracking")]
    public int totalMonsters; // set manually in Inspector or auto-count

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void MonsterKilled()
    {
        totalMonsters--;

        Debug.Log("Monster killed. Remaining: " + totalMonsters);

        if (totalMonsters <= 0)
        {
            WinGame();

        }
    }

    public void WinGame()
    {
        Debug.Log("You Win!");
        WinUI.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    
}
