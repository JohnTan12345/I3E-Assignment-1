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
    GameObject GreenHPBar;
    [SerializeField]
    GameObject HPText;
    [SerializeField]
    GameObject coinUI;
    [SerializeField]
    GameObject coinUIText;
    [SerializeField]
    GameObject InteractUI;
    [SerializeField]
    GameObject KeycardUI;
    // Variables
    private int maxhealth = 100;
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
    private int coins = 0;
    public int Coins
    {
        get
        {
            return coins;
        }
        set
        {
            coins = value;
            UpdateCoinUI();
        }
    }
    public bool canTakeDamage = true;
    private bool hasKeycard = false;
    public bool HasKeycard
    {
        get
        {
            return hasKeycard;
        }
        set
        {
            hasKeycard = value;
            UpdateKeycardUI();
        }
    }
    public bool isDead = false;

    private bool interactable = false;
    private GameObject interactableObject;
    [SerializeField]
    private GameObject raycastSpawner;

    void Start()
    {
        UpdateHealthUI();
        coinUI.SetActive(false);
        KeycardUI.SetActive(false);
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastSpawner.transform.position, raycastSpawner.transform.forward, out hit, 1.5f) && hit.collider.CompareTag("Interactable"))
        {
            interactable = true;
            interactableObject = hit.collider.gameObject;
            InteractUI.SetActive(true);
        }
        else
        {
            interactable = false;
            interactableObject = null;
            InteractUI.SetActive(false);
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
            if (interactableObject.CompareTag("Interactable"))
            {
                if (interactableObject.GetComponent<CoinBehaviour>() != null)
                {
                    interactableObject.GetComponent<CoinBehaviour>().AddCoin(this);
                }
                else if (interactableObject.GetComponent<ItemBehaviour>() != null)
                {
                    interactableObject.GetComponent<ItemBehaviour>().AddtoInventory(this);
                }
                else if (interactableObject.GetComponent<KeycardDoorBehaviour>() != null)
                {
                    interactableObject.GetComponent<KeycardDoorBehaviour>().UseKeycard(this);
                }
                else if (interactableObject.GetComponent<LeverBehaviour>() != null)
                {
                    interactableObject.GetComponent<LeverBehaviour>().ActivateLever();
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

    // GUI Functions
    private void UpdateHealthUI()
    {
        GreenHPBar.transform.localScale = new Vector3((float)health / maxhealth, 1f, 1f);
        HPText.GetComponent<TextMeshProUGUI>().text = string.Format("{0}/{1}", health, maxhealth);
    }
    private void UpdateCoinUI()
    {
        coinUIText.GetComponent<TextMeshProUGUI>().text = string.Format("{0} / {1} Collected", coins, 13);
        StartCoroutine(CoinUIShow());
    }
    private void UpdateKeycardUI()
    {
        KeycardUI.SetActive(hasKeycard);
    }
    IEnumerator CoinUIShow()
    {
        coinUI.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        coinUI.SetActive(false);
    }
}
