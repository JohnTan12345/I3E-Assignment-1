/*
* Author: Tan Hong Yan John
* Date: 10 June 2025
* Description: Add item (currently just keycard) to inventory
*/

using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public AudioClip pickUpSFX;
    public void AddtoInventory(PlayerBehaviour player)
    {
        player.HasKeycard = true;
        gameObject.SetActive(false);
        AudioSource.PlayClipAtPoint(pickUpSFX, transform.position);
    }
}
