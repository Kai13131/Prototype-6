using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bomb : MonoBehaviour
{
    public float timeLimit = 10f;
    public string correctWire = "Blue";
    public float interactionDistance = 5f;

    public Camera playerCamera;

    public TMP_Text timerText;

    public GameObject winPanel;
    public GameObject gameOverPanel;

    private float timer;
    private bool gameEnded = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = timeLimit;

        winPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        timerText.text = ("F2");
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        timerText.text = Mathf.Max(0f, timer).ToString("F2");

        if(timer <= 0f)
        {
            Explode();
            return;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            TryCutWire();
        }
    }

    void TryCutWire()
    {
        Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray,out hit, interactionDistance))
        {
            BombWire wire = hit.collider.GetComponentInParent<BombWire>();

            if(wire == null || wire.IsCut())
            {
                return;
            }
            if (wire.wireName == correctWire)
            {
                wire.Cut();
                WinGame();
            }
            else
            {
                wire.Cut();
                Explode();
            }
        }
    }
    public void WinGame()
    {
        if (gameEnded)
            return;

        gameEnded = true;

        timerText.text = "DEFUSED";
        winPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Explode()
    {
        if (gameEnded)
            return;

        gameEnded = true;

        timerText.text = "00.00";
        gameOverPanel.SetActive(true);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void RestartGame()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
