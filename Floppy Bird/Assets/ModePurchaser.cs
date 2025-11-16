using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

public class ModePurchaser : MonoBehaviour

{   public List<Skin> modes;
    public CheckboxUpdater checkboxUpdater;
    public ColourChanger colourChanger;
    public float coins;
    private GameObject purchaseButton;
    private GameObject selectButton;
    public TextMeshProUGUI coinsTextMenu;
    public TextMeshProUGUI coinsTextCustomize;
    public GameObject modeSelector;

    [Header("Select Buttons")]
    public GameObject defaultSelectButton;
    public GameObject christmasSelectButton;
    public GameObject underwaterSelectButton;

    [Header("Purchase Buttons")]
    public GameObject christmasPurchaseButton;
    public GameObject underwaterPurchaseButton;

    [Header("Modes")]
    public GameObject defaultMode;
    public GameObject christmasMode;
    public GameObject underWaterMode;




    AudioManager audioManager;



    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
    }

    void Start()
    {

        InitializeModes();
        coins = PlayerPrefs.GetFloat("Coins", 0f);
        UpdatePurchasedModes();


    }


    public void PurchaseMode(string modeName)
    {
        // Check for null reference
        if (modes == null)
        {
            Debug.LogWarning("Modes have not been initialized!");
            return;
        }

        // Find the target skin
        var mode = modes.Find(s => s.name == modeName);
        if (mode == null)
        {
            Debug.LogWarning($"Mode '{modeName}' not found.");
            return;
        }

        if (mode.purchased)
        {
            Debug.Log($"Skin '{modeName}' already purchased.");
            return;
        }

        if (coins < mode.price)
        {
            audioManager.PlaySFX(audioManager.unsuccesfulPurchaseClip);

            Debug.Log("Not enough coins to purchase this skin!");
            // TODO
            // Play failure sound here
            return;
        }

        // Deduct coins and mark as purchased
        coins -= mode.price;
        mode.purchased = true;
        audioManager.PlaySFX(audioManager.purchaseClip);
        PlayerPrefs.SetInt(mode.name, 1);
        PlayerPrefs.SetFloat("Coins", coins);
        PlayerPrefs.Save();

        // Update UI
        purchaseButton = GetPurchaseButton(modeName);
        if (purchaseButton != null)
        {
            purchaseButton.SetActive(false);
        }

        // Show select button (activate the GameObject)
        selectButton = GetSelectButton(modeName);
        if (selectButton != null)
        {
            selectButton.SetActive(true);  // This activates the parent GameObject
            Debug.Log($"Select button activated for: {mode.name}");
        }
        else
        {
            Debug.LogError($"Select button not found for {mode.name}");
        }

        coinsTextMenu.text = coins.ToString();
        coinsTextCustomize.text = coins.ToString();

        // Play purchase success sound
    }

    public void UpdatePurchasedModes()
    {
        if (modes != null)
        {
            foreach (var mode in modes)
            {
                int modePurchased = PlayerPrefs.GetInt(mode.name, 0);
                if (modePurchased == 1)
                {
                    mode.purchased = true;

                    // Show select button (activate the GameObject)
                    selectButton = GetSelectButton(mode.name);
                    if (selectButton != null)
                    {
                        selectButton.SetActive(true);  // This activates the parent GameObject
                    }
                    else
                    {
                        Debug.LogError($"Select button not found for {mode.name}");
                    }
                    // Hide purchase button
                    purchaseButton = GetPurchaseButton(mode.name);
                    if (purchaseButton != null)
                    {
                        purchaseButton.SetActive(false);
                    }


                }
            }
        }
        if (modes == null)
        {
            Debug.Log("--------- Modes have not been initialized! ---------------");
        }
    }

    private GameObject GetSelectButton(string modeName)
    {
        switch (modeName)
        {
            case "Default": return defaultSelectButton;
            case "Christmas": return christmasSelectButton;
            case "Underwater": return underwaterSelectButton;
            default:
                {
                    Debug.Log($"Couldn't find select button for mode: {modeName}");
                    return null;
                }
        }
    }

    private GameObject GetPurchaseButton(string modeName)
    {
        switch (modeName)
        {
            case "Christmas": return christmasPurchaseButton;
            case "Underwater": return underwaterPurchaseButton;
            default:
                {
                    Debug.Log($"Couldn't find purchase button for mode: {modeName}");
                    return null;
                }
        }
    }

    public void SetModeSelector(string modeName)
    {
        switch (modeName)
        {
            case "Default":
                {
                    modeSelector = defaultMode;
                    return;
                }
            case "Christmas":
                {
                    modeSelector = christmasMode;
                    return;
                }
            case "Underwater":
                {
                    modeSelector = underWaterMode;
                    return;
                }
            default:
                {
                    Debug.Log($"Couldn't find select button for mode: {modeName}");
                    modeSelector = defaultMode;
                    return;
                }
        }
    }

    public void SelectBox(string modeName)
    {
        if (modes == null)
        {
            Debug.Log("--------- Skins have not been initialized! ---------------");
            return;
        }
        foreach (var mode in modes)
        {
            SetModeSelector(mode.name);
            if (mode.purchased == true)
            {
                checkboxUpdater = modeSelector.GetComponentInChildren<CheckboxUpdater>();
                colourChanger = modeSelector.GetComponentInChildren<ColourChanger>();


                if (mode.name == modeName)
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


    public void InitializeModes()
    {
        if (modes != null)
        {
            return;
        }
        modes = new List<Skin>();
        modes.Add(new Skin("Default", true, true, 0));
        modes.Add(new Skin("Christmas", false, false, 500));
        modes.Add(new Skin("Underwater", false, false, 500));
    }
}

public class Mode
{
    public Mode(string name, bool selected, bool purchased, int price)
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
