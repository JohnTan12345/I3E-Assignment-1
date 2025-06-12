using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public void AddtoInventory(PlayerBehaviour player)
    {
        player.HasKeycard = true;
        Destroy(gameObject);
    }
}
