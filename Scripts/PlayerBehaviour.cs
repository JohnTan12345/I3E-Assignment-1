using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    // UI
    [SerializeField]
    private GameObject GreenHPBar;
    [SerializeField]
    private GameObject HPText;
    // Variables
    private int maxhealth = 100;
    [SerializeField]
    private int health = 100;
    public int Health // Update Health GUI the very moment it changes
    {
        get
        {
            return health;
        }
        set
        {
            if (value != health)
            {
                health = value;
                UpdateHealthUI();
            }

        }
    }
    public bool canTakeDamage = true;
    public int coins = 0;
    public List<string> items = new();
    public bool isDead = false;

    private bool interactable = false;
    private GameObject interactableObject;
    [SerializeField]
    private GameObject raycastSpawner;

    void Start()
    {
        UpdateHealthUI();
    }

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
            StarterAssetsInputs starterAssetsInputs = GetComponent<StarterAssetsInputs>();
            starterAssetsInputs.cursorLocked = false;
            starterAssetsInputs.cursorInputForLook = false;
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

    private void UpdateHealthUI()
    {
        GreenHPBar.transform.localScale = new Vector3((float)health / maxhealth, 1f, 1f);
        HPText.GetComponent<TextMeshProUGUI>().text = string.Format("{0}/{1}", health, maxhealth);
    }
}
