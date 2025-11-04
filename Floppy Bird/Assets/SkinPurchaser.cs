using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SkinPurchaser : MonoBehaviour
{
    public GameObject redBird;
    public GameObject spaceBird;
    public GameObject coiny;
    public GameObject santa;
    public GameObject concept;
    public List<Skin> skins;
    //public GameObject ques;
    //public GameObject question;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public CheckboxUpdater checkboxUpdater;
    public ColourChanger colourChanger;
    public GameObject skinSelector;
    public GameObject purchaseButton;
    public Image[] images;


    public float coins;
    public TextMeshProUGUI coinsTextMenu;
    public TextMeshProUGUI coinsTextCustomize;

    public static SkinPurchaser instance;

    [Header("Select Buttons")]
    public GameObject redBirdSelectButton;
    public GameObject spaceBirdSelectButton;
    public GameObject coinySelectButton;
    public GameObject santaSelectButton;
    public GameObject conceptSelectButton;

    [Header("Purchase Buttons")]
    public GameObject redBirdPurchaseButton;
    public GameObject spaceBirdPurchaseButton;
    public GameObject coinyPurchaseButton;
    public GameObject santaPurchaseButton;
    public GameObject conceptPurchaseButton;



    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        PlayerPrefs.SetInt("Santa", 0);

        InitializeSkins();
        coins = PlayerPrefs.GetFloat("Coins", 0f);
        UpdatePurchasedSkins();
        
    }


    public void PurchaseSkin(string skinName)
    {
        // Check for null reference
        if (skins == null)
        {
            Debug.LogWarning("Skins have not been initialized!");
            return;
        }

        // Find the target skin
        var skin = skins.Find(s => s.name == skinName);
        if (skin == null)
        {
            Debug.LogWarning($"Skin '{skinName}' not found.");
            return;
        }

        if (skin.purchased)
        {
            Debug.Log($"Skin '{skinName}' already purchased.");
            return;
        }

        if (coins < skin.price)
        {
            Debug.Log("Not enough coins to purchase this skin!");
            // Play failure sound here
            return;
        }

        // Deduct coins and mark as purchased
        coins -= skin.price;
        skin.purchased = true;
        PlayerPrefs.SetInt(skin.name, 1);
        PlayerPrefs.SetFloat("Coins", coins);
        PlayerPrefs.Save();

        // Update UI
        purchaseButton = GameObject.FindGameObjectWithTag(skin.name + "Purchase");
        if (purchaseButton != null)
        {
            purchaseButton.SetActive(false);
        }

        // Show select button (activate the GameObject)
        GameObject selectButton = GetSelectButton(skin.name);
        if (selectButton != null)
        {
            selectButton.SetActive(true);  // This activates the parent GameObject
            Debug.Log($"Select button activated for: {skin.name}");
        }
        else
        {
            Debug.LogError($"Select button not found for {skin.name}");
        }

        coinsTextMenu.text = coins.ToString();
        coinsTextCustomize.text = coins.ToString();

        // Play purchase success sound
        Debug.Log($"Purchased skin: {skinName}");
    }

    public void UpdatePurchasedSkins()
    {
        if (skins != null)
        {
            foreach (var skin in skins)
            {
                SetSkinSelector(skin.name);
                Debug.Log(skinSelector);
                int skinPurchased = PlayerPrefs.GetInt(skin.name, 0);
                if (skinPurchased == 1)
                {
                    skin.purchased = true;

                    // Show select button (activate the GameObject)
                    GameObject selectButton = GetSelectButton(skin.name);
                    if (selectButton != null)
                    {
                        selectButton.SetActive(true);  // This activates the parent GameObject
                        Debug.Log($"Select button activated for: {skin.name}");
                    }
                    else
                    {
                        Debug.LogError($"Select button not found for {skin.name}");
                    }
                    // Hide purchase button
                    purchaseButton = GetPurchaseButton(skin.name);
                    if (purchaseButton != null)
                    {
                        purchaseButton.SetActive(false);
                    }

                    Debug.Log($"purchase button for {skin.name} active? {purchaseButton.activeInHierarchy} ");

                }
            }
        }
        if (skins == null)
        {
            Debug.Log("--------- Skins have not been initialized! ---------------");
        }
    }

    public void SetSkinSelector(string skinName)
    {
        if(skinName == "RedBird")
        {
            skinSelector = redBird;
        } else if (skinName == "SpaceBird")
        {
            skinSelector = spaceBird;
        } else if (skinName == "Coiny")
        {
            skinSelector = coiny;
        }
        else if (skinName == "Santa")
        {
            skinSelector = santa;
        }
        else if (skinName == "Concept")
        {
            skinSelector = concept;
        }
        else
        {
            Debug.Log($"Couldnt find skin {skinName}");
            skinSelector = redBird;
        }
    }
    private GameObject GetSelectButton(string skinName)
    {
        switch (skinName)
        {
            case "RedBird": return redBirdSelectButton;
            case "SpaceBird": return spaceBirdSelectButton;
            case "Coiny": return coinySelectButton;
            case "Santa": return santaSelectButton;
            case "Concept": return conceptSelectButton;
            default: return null;
        }
    }

    private GameObject GetPurchaseButton(string skinName)
    {
        switch (skinName)
        {
            case "SpaceBird": return spaceBirdPurchaseButton;
            case "Coiny": return coinyPurchaseButton;
            case "Santa": return santaPurchaseButton;
            case "Concept": return conceptPurchaseButton;
            default: return null;
        }
    }

    public void SelectBox(string skinName)
    {
        if (skins == null)
        {
            Debug.Log("--------- Skins have not been initialized! ---------------");
            return; 
        }
            foreach (var skin in skins)
            {
                SetSkinSelector(skin.name);
                if (skin.purchased == true)
                {
                    checkboxUpdater = skinSelector.GetComponentInChildren<CheckboxUpdater>();
                    colourChanger = skinSelector.GetComponentInChildren<ColourChanger>();
                    if (skin.name == skinName)
                    {
                        // UpdateBox() method changes checbox to selected, see method for reason of not refactoring.
                        checkboxUpdater.UpdateBox();
                        // ChangeColour() method changes text to white, see method for reason of not refactoring.
                        colourChanger.ChangeColour();
                    }
                    else
                    {
                        checkboxUpdater.UncheckBox();
                        colourChanger.SetUnselectedColour();
                    }
                }
            }
    }

    public void InitializeSkins()
    {
        if (skins != null)
        {
            return;
        }
        skins = new List<Skin>();
        skins.Add(new Skin("RedBird", true, true, 0));
        skins.Add(new Skin("SpaceBird", false, false, 200));
        skins.Add(new Skin("Coiny", false, false, 2000)); // change cost
        skins.Add(new Skin("Concept", false, false, 500));
        skins.Add(new Skin("Santa", false, false, 1000));
    }
}

public class Skin
{
    public Skin(string name, bool selected, bool purchased, int price)
    {
        this.name = name;
        this.selected = selected;
        this.purchased = purchased;
        this.price = price;
    }

    public string name;
    public bool selected;
    public bool purchased;
    public int price;
}

