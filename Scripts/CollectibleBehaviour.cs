using System.Collections;
using UnityEngine;

public class CollectibleSpin : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(0, 1, 0, Space.World);
    }
}
