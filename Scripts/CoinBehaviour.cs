using System.Collections;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    // Variables
    public void AddCoin(PlayerBehaviour player)
    {
        player.coins++;
        Destroy(gameObject);
    }
    private void Start()
    {
        StartCoroutine(RotateCoin());
    }
    IEnumerator RotateCoin() // Spins the coin every frame
    {
        while (true)
        {
            transform.Rotate(0, 0, 0.1f);
            yield return null;
        }
    }

}
