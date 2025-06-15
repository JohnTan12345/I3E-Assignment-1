/*
* Author: Tan Hong Yan John
* Date: 11 June 2025
* Description: HP Changes after collision with damage object
*/

using System.Collections;
using UnityEngine;

public class ModifyHealthBehaviour : MonoBehaviour
{
    public bool damage = false;
    public int amount = 0;

    public void ModifyHealth(PlayerBehaviour player)
    {
        if (player.canTakeDamage && !player.IsDead)
        {
            if (damage)
            {
                player.Health -= amount;
            }
            else
            {
                player.Health += amount;
            }
            player.canTakeDamage = false;
            StartCoroutine(CanTakeDamageTimer(player));
            if (player.Health <= 0)
            {
                player.Health = 0;
                player.IsDead = true;
            }
        }

    }
    IEnumerator CanTakeDamageTimer(PlayerBehaviour player) // Give the player time to react to the damage if not instant
    {
        yield return new WaitForSeconds(1f);
        player.canTakeDamage = true;
    }
}
