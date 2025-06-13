/*
* Author: Tan Hong Yan John
* Date: 14 June 2025
* Description: Adding Score
*/

using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public int score = 0;
    public bool canAddScore = true;

    public void AddScore(PlayerBehaviour player)
    {
        if (canAddScore)
        {
            player.Score += score;
        }
    }
}
