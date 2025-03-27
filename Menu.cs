using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [Header("Canvases")]
    public GameObject CanvasMain;
    public GameObject CanvasSettings;
    public GameObject CanvasPause;
    public GameObject CanvasGameOver;
    public GameObject registerCanvas;
    public GameObject loginCanvas;
    public GameObject finishCanvas;

    private void Start()
    {
        Time.timeScale = 0f;
        CanvasMain.SetActive(true);
        CanvasSettings.SetActive(false);
        CanvasPause.SetActive(false);
        CanvasGameOver.SetActive(false);
        finishCanvas.SetActive(false);
        loginCanvas.SetActive(false);
        registerCanvas.SetActive(false);
    }

    public void StartGame()
    {
        CanvasMain.SetActive(false);
        CanvasSettings.SetActive(false);
        CanvasPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ShowLoginCanvas()
    {
        loginCanvas.SetActive(true);
        registerCanvas.SetActive(false);
    }

    public void ShowRegisterCanvas()
    {
        registerCanvas.SetActive(true);
        loginCanvas.SetActive(false);
    }

    public void OpenSettings()
    {
        CanvasSettings.SetActive(true);
    }

    public void CloseSettings()
    {
        CanvasSettings.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}