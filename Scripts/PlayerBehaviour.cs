/*
* Author: Tan Hong Yan John
* Date: 10 June 2025
* Description: Player functions as well as most of the UI functions
*/

using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class PlayerBehaviour : MonoBehaviour
{
    // UI
    public GameObject MainUI;
    private GameObject greenHPBar;
    private GameObject HPText;
    private GameObject coinUI;
    private GameObject coinUIText;
    private GameObject interactUI;
    private GameObject keycardUI;
    private GameObject planksUI;
    private GameObject tutorialUI;
    private GameObject hintsUIGroup;
    private GameObject scoreUI;
    private GameObject DeathMessageUI;
    private GameObject WinUI;
    private GameObject WinCoinUI;
    private GameObject WinScoreUI;
    private Button tutorialButton;
    private Button RespawnButton;
    private Button QuitButton;

    // Variables
    private int maxhealth = 100;
    private int health = 100;
    private int totalCollectibles = 5;
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
    private int score = 0;
    public int Score
    {
        get
        {
            return score;
        }
        set
        {
            score = value;
            UpdateScoreUI();
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
            UpdateObjectUI(keycardUI, value);
        }
    }
    private bool hasPlanks = false;
    public bool HasPlanks
    {
        get
        {
            return hasPlanks;
        }
        set
        {
            hasPlanks = value;
            UpdateObjectUI(planksUI, value);
        }
    }
    private bool isDead = false;
    public bool IsDead
    {
        get
        {
            return isDead;
        }
        set
        {
            isDead = value;
            PlayerDied();
        }
    }
    public bool doneTutorial = false;
    private bool interactable = false;

    private GameObject interactableObject;
    private CharacterController characterController;
    private StarterAssetsInputs starterAssetsInputs;
    public Transform spawnPointPos;
    public GameObject raycastSpawner;
    public AudioSource WinSFX;

    void Start() // Get all needed UI and update Health and Score.
    {

        greenHPBar = MainUI.transform.Find("HP Bar").Find("HP Red").Find("HP Green").gameObject;
        HPText = MainUI.transform.Find("HP Bar").Find("HP Red").Find("HP Indicator").gameObject;
        coinUI = MainUI.transform.Find("Coins").gameObject;
        coinUIText = coinUI.transform.Find("Coin Object").Find("Coin Count").gameObject;
        interactUI = MainUI.transform.Find("Interact Pop Up").gameObject;
        keycardUI = MainUI.transform.Find("Keycard").gameObject;
        planksUI = MainUI.transform.Find("Planks").gameObject;
        tutorialUI = MainUI.transform.Find("Tutorial").gameObject;
        hintsUIGroup = MainUI.transform.Find("Hints").gameObject;
        scoreUI = MainUI.transform.Find("Score Text").gameObject;
        DeathMessageUI = MainUI.transform.Find("Death Message").gameObject;
        RespawnButton = DeathMessageUI.transform.Find("Respawn Btn").GetComponent<Button>();
        QuitButton = DeathMessageUI.transform.Find("Give Up Btn").GetComponent<Button>();
        WinUI = MainUI.transform.Find("Win UI").gameObject;
        WinCoinUI = WinUI.transform.Find("Coins").gameObject;
        WinScoreUI = WinUI.transform.Find("Score").gameObject;
        characterController = GetComponent<CharacterController>();
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();

        if (doneTutorial)
        {
            Destroy(tutorialUI);
            TutorialRead();
        }
        else
        {
            tutorialButton = tutorialUI.transform.Find("OK Btn").GetComponent<Button>();
            tutorialButton.onClick.AddListener(TutorialRead);
        }

        RespawnButton.onClick.AddListener(Respawn);
        QuitButton.onClick.AddListener(QuitGame);
        UpdateHealthUI();
        UpdateScoreUI();
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(raycastSpawner.transform.position, raycastSpawner.transform.forward, out hit, 3f))
        {
            if (hit.collider.CompareTag("Interactable"))
            {
                interactable = true;
                interactableObject = hit.collider.gameObject;
                interactUI.SetActive(true);
                RemoveHints();
            }
            else if (hit.collider.CompareTag("HintBox"))
            {
                hit.collider.GetComponent<HintBehaviour>().hint();
                NoInteractables();
            }
            else
            {
                NoInteractables();
                RemoveHints();
            }
        }
        else
        {
            NoInteractables();
            RemoveHints();
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
                else if (interactableObject.GetComponentInParent<PlaceableObjectBehaviour>() != null)
                {
                    if (!hasPlanks)
                    {
                        interactableObject.GetComponentInParent<PlaceableObjectBehaviour>().PickUp(this);
                    }
                    else
                    {
                        interactableObject.GetComponentInParent<PlaceableObjectBehaviour>().Place(this);
                    }
                }

                if (interactableObject.GetComponent<ScoreScript>() != null)
                {
                    interactableObject.GetComponent<ScoreScript>().AddScore(this);
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
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "WinArea")
        {
            PlayerWin();
        }
    }

    // GUI Functions
    private void UpdateHealthUI()
    {
        greenHPBar.transform.localScale = new Vector3((float)health / maxhealth, 1f, 1f);
        HPText.GetComponent<TextMeshProUGUI>().text = string.Format("{0}/{1}", health, maxhealth);
    }
    private void UpdateCoinUI()
    {
        if (coins < totalCollectibles)
        {
            coinUIText.GetComponent<TextMeshProUGUI>().text = string.Format("{0} / {1} Collected", coins, totalCollectibles);
        }
        else
        {
            coinUIText.GetComponent<TextMeshProUGUI>().text = "All Collected!";
        }
        StartCoroutine(CoinUIShow());
    }
    private void UpdateObjectUI(GameObject UI, bool Active)
    {
        UI.SetActive(Active);
    }
    private void TutorialRead()
    {
        Destroy(tutorialUI);
        gameObject.GetComponent<StarterAssetsInputs>().cursorLocked = true;
        gameObject.GetComponent<StarterAssetsInputs>().cursorInputForLook = true;
        gameObject.GetComponent<CharacterController>().enabled = true;
    }
    private void RemoveHints()
    {
        foreach (Transform child in hintsUIGroup.transform)
        {
            child.gameObject.SetActive(false);
        }
    }
    private void UpdateScoreUI()
    {
        scoreUI.GetComponent<TextMeshProUGUI>().text = string.Format("Score: {0}", score);
    }
    IEnumerator CoinUIShow()
    {
        coinUI.SetActive(true);
        yield return new WaitForSecondsRealtime(2.5f);
        coinUI.SetActive(false);
    }

    // Other Functions
    private void NoInteractables()
    {
        interactable = false;
        interactableObject = null;
        interactUI.SetActive(false);
    }

    private void Respawn()
    {
        Health = 100;
        this.gameObject.transform.position = spawnPointPos.position;
        this.gameObject.transform.rotation = spawnPointPos.rotation;
        characterController.enabled = true;
        starterAssetsInputs.cursorLocked = true;
        starterAssetsInputs.cursorInputForLook = true;
        DeathMessageUI.SetActive(false);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void PlayerDied()
    {
        characterController.enabled = false;
        starterAssetsInputs.cursorLocked = false;
        starterAssetsInputs.cursorInputForLook = false;
        DeathMessageUI.SetActive(true);
    }

    private void PlayerWin()
    {
        WinCoinUI.GetComponent<TextMeshProUGUI>().text = string.Format("Coins Collected: {0}/{1}", coins, totalCollectibles);
        WinScoreUI.GetComponent<TextMeshProUGUI>().text = string.Format("Score: {0}", score);
        characterController.enabled = false;
        starterAssetsInputs.cursorLocked = false;
        starterAssetsInputs.cursorInputForLook = false;
        WinUI.SetActive(true);
        WinSFX.Play();
    }
}