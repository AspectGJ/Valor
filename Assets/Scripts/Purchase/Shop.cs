using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;

[Serializable]

public class Item
{
    public string name;
    public string id;
    public int price;
    public int amount;

}


public class Shop : MonoBehaviour, IStoreListener
{
    IStoreController storeController;

    public TextMeshProUGUI healthPotionText;
    public TextMeshProUGUI manaPotionText;

    public Item healthPotion;
    public Item manaPotion;

    // Start is called before the first frame update
    void Start()
    {
        healthPotion.amount = PlayerPrefs.GetInt("HealthPotion");
        manaPotion.amount = PlayerPrefs.GetInt("ManaPotion");
    }

    void SetupBuilder()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());
        builder.AddProduct(healthPotion.id, ProductType.Consumable);
        builder.AddProduct(manaPotion.id, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        print("Store Initialized");
        storeController = controller;
    }

    public void Health_Btn_Pressed()
    {
        
        storeController.InitiatePurchase(healthPotion.id);
    }

    public void Mana_Btn_Pressed()
    {
        
        storeController.InitiatePurchase(manaPotion.id);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        var product = purchaseEvent.purchasedProduct;

        print("Purchased: " + product.definition.id);

        if (product.definition.id == healthPotion.id)
        {
            AddHealthPotion();
        }
        else if (product.definition.id == manaPotion.id)
        {
            AddManaPotion();
        }

        return PurchaseProcessingResult.Complete;
    }

    public void AddHealthPotion()
    {
        healthPotion.amount = PlayerPrefs.GetInt("HealthPotion");
        healthPotion.amount++;
        PlayerPrefs.SetInt("HealthPotion", healthPotion.amount);
        
        if(healthPotionText != null)
        {
            healthPotionText.text = ": " + healthPotion.amount.ToString();
        }
    }

    public void AddManaPotion()
    {
        manaPotion.amount = PlayerPrefs.GetInt("ManaPotion");
        manaPotion.amount++;
        PlayerPrefs.SetInt("ManaPotion", manaPotion.amount);

        if(manaPotionText != null)
        {
            manaPotionText.text = ": " + manaPotion.amount.ToString();
        }
        
    }




    public void OnInitializeFailed(InitializationFailureReason error)
    {
        print("Store Initialization Failed: " + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        print("Store Initialization Failed: " + error + message);
    }

    

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        print("Purchase Failed: " + product.definition.id + " Reason: " + failureReason);
    }

    
}
