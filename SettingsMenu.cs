using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject CanvasSettings;

    public void OpenSettings()
    {
        CanvasSettings.SetActive(true);
    }

    public void CloseSettings()
    {
        CanvasSettings.SetActive(false);
    }
}