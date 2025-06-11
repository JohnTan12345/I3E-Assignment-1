using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    // Variables
    public int health = 100;
    public bool canTakeDamage = true;
    public int coins = 0;
    public List<string> items = new();
    public bool isDead = false;

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
        else
        {
            interactable = false;
            interactableObject = null;
        }
    }

    void Update()
    {
        CharacterController characterController = GetComponent<CharacterController>();

        if (isDead)
        {
            characterController.enabled = false;
        }
    }

    void OnInteract()
    {
        if (interactable && interactableObject != null)
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

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.CompareTag("HealthModifier"))
        {
            Debug.Log(other.gameObject.name);
            other.gameObject.GetComponent<ModifyHealthBehaviour>().ModifyHealth(this);
        }
    }

    private void Interacted()
    {
        interactable = false;
        interactableObject = null;
    }
}
