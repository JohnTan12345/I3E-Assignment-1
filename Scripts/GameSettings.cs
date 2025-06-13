/*
* Author: Tan Hong Yan John
* Date: 10 June 2025
* Description: FPS Limiter so that my GPU doesn't have a meltdown
*/

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
