using System.Collections;
using UnityEngine;

public class ModifyHealthBehaviour : MonoBehaviour
{
    public bool damage = false;
    public int amount = 0;

    public void ModifyHealth(PlayerBehaviour player)
    {
        if (player.canTakeDamage && !player.isDead)
        {
            if (damage)
            {
                player.health -= amount;
            }
            else
            {
                player.health += amount;
            }
            player.canTakeDamage = false;
            StartCoroutine(CanTakeDamageTimer(player));
            if (player.health <= 0)
            {
                player.isDead = true;
            }
        }

    }
    IEnumerator CanTakeDamageTimer(PlayerBehaviour player)
    {
        yield return new WaitForSeconds(1f);
        player.canTakeDamage = true;
    }
}
