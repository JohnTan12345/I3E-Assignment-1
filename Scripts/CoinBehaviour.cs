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
    public void AddCoin(PlayerBehaviour player)
    {
        player.Coins++;
        Destroy(gameObject);
    }
}
