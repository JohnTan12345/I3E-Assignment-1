using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Variables
    [SerializeField]
    private int health = 100;
    public int coins = 0;
    [SerializeField]
    public List<string> items = new();
    private bool interactable = false;
    private GameObject interactableObject;
    [SerializeField]
    private GameObject raycastSpawner;

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastSpawner.transform.position, raycastSpawner.transform.forward, out hit, 1.5f) && (hit.collider.CompareTag("Collectible"))) // May Add Interactible objects later
        {
            interactable = true;
            interactableObject = hit.collider.gameObject;
        }

    }

    void OnInteract()
    {
        if (interactable)
        {
            if (interactableObject.CompareTag("Collectible"))
            {
                if (interactableObject.GetComponent<CoinBehaviour>() != null)
                {
                    interactableObject.GetComponent<CoinBehaviour>().AddCoin(this);
                    Interacted();
                }
                else if (interactableObject.GetComponent<ItemBehaviour>() != null)
                {
                    interactableObject.GetComponent<ItemBehaviour>().AddtoInventory(this);
                    Interacted();
                }
            }
        }
    }

    void Interacted()
    {
        interactable = false;
        interactableObject = null;
    }

}
