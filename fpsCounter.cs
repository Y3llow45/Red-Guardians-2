using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TMP_Text fpsText;
    private float deltaTime = 0.0f;

    void Start()
    {
        Application.targetFrameRate = 75;
    }

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        fpsText.text = string.Format("FPS: {0:0.}", fps);
    }
}