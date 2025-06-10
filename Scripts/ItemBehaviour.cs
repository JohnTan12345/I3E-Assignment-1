using UnityEngine;

public class ItemBehaviour : MonoBehaviour
{
    public void AddtoInventory(PlayerBehaviour player)
    {
        player.items.Add(gameObject.name);
        Destroy(gameObject);
    }
}
