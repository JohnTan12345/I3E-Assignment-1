/*
* Author: Tan Hong Yan John
* Date: 10 June 2025
* Description: Cap FPS at 60
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
