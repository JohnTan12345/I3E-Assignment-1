using System.Collections;
using UnityEngine;

public class CollectibleSpin : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(RotateCoin());
    }
    IEnumerator RotateCoin() // Spins the coin every frame
    {
        while (true)
        {
            transform.Rotate(0, 0.1f, 0, Space.World);
            yield return null;
        }
    }
}
