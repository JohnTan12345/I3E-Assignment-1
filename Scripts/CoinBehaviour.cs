/*
* Author: Tan Hong Yan John
* Date: 10 June 2025
* Description: Coin Function
*/

using System.Collections;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    // Variables
    public AudioClip collectSFX;
    public void AddCoin(PlayerBehaviour player)
    {
        player.Coins++;
        AudioSource.PlayClipAtPoint(collectSFX, transform.position);
        Destroy(gameObject);
    }
}
