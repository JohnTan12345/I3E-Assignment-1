using System.Collections;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(RotateCoin());
    }
    IEnumerator RotateCoin()
    { 
        while (true)
        {
            transform.Rotate(0, 0, 0.1f);
            yield return null; // Wait for the next frame
        }
    }

}
