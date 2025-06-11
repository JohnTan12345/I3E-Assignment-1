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
