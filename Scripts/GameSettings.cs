using UnityEngine;

public class GameSettings : MonoBehaviour
{
    private int targetFPS = 60;
    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = targetFPS;
    }
}
