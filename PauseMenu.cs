using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject CanvasPause;
    public GameObject CanvasSettings;
    private GameControls controls;

    private void Awake()
    {
        controls = new GameControls();
        controls.UI.Pause.performed += ctx =>
        {
            TogglePause();
        };
        if (Input.GetKeyDown(KeyCode.P))
        {
            TogglePause();
        }
    }

    private void Start()
    {
        CanvasPause.SetActive(false);
    }

    private void OnEnable()
    {
        controls.UI.Enable();
    }

    private void OnDisable()
    {
        controls.UI.Disable();
    }

    public void TogglePause()
    {
        if (Time.timeScale == 0f)
        {
            Debug.Log("Resuming game");
            ResumeGame();
        }
        else
        {
            Debug.Log("Pausing game");
            PauseGame();
        }
    }

    public void PauseGame()
    {
        if (CanvasPause == null)
        {
            Debug.LogError("Pause Menu Canvas is NOT assigned in the Inspector!");
            return;
        }
        CanvasPause.SetActive(true);
        CanvasSettings.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        CanvasPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void OpenSettings()
    {
        CanvasSettings.SetActive(true);
    }

    public void CloseSettings()
    {
        CanvasSettings.SetActive(false);
    }
}