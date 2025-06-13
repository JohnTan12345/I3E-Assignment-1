/*
* Author: Tan Hong Yan John
* Date: 11 June 2025
* Description: Spinning Collectibles
*/

using System.Collections;
using UnityEngine;

public class CollectibleSpin : MonoBehaviour
{
    void FixedUpdate()
    {
        transform.Rotate(0, 1, 0, Space.World);
    }
}
