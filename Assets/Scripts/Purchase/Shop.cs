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
    IExtensionProvider storeExtensionProvider;

    public TextMeshProUGUI healthPotionText;
    public TextMeshProUGUI manaPotionText;

    public Item healthPotion;
    public Item manaPotion;

    // Start is called before the first frame update
    void Start()
    {
        IAPStart();
        healthPotion.amount = PlayerPrefs.GetInt("HealthPotion", 0);
        manaPotion.amount = PlayerPrefs.GetInt("ManaPotion", 0);
    }

    private void Update()
    {
        if (healthPotionText != null)
        {
            healthPotionText.text = ": " + healthPotion.amount.ToString();
        }

        if (manaPotionText != null)
        {
            manaPotionText.text = ": " + manaPotion.amount.ToString();
        }
    }

    private void IAPStart()
    {
        var module = StandardPurchasingModule.Instance();
        ConfigurationBuilder builder = ConfigurationBuilder.Instance(module);

        builder.AddProduct(healthPotion.id, ProductType.Consumable);
        builder.AddProduct(manaPotion.id, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
    }

    public void IAPButton(string productID)
    {
        Product product = storeController.products.WithID(productID);

        if (product != null && product.availableToPurchase)
        {
            print("Buying");
            storeController.InitiatePurchase(product);
        }
        else
        {
            print("Product not found or not available for purchase");
        }
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
        storeExtensionProvider = extensions;
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
        //var product = purchaseEvent.purchasedProduct;

        //print("Purchased: " + product.definition.id);

        //if (product.definition.id == healthPotion.id)
        //{
        //    AddHealthPotion();
        //}
        //else if (product.definition.id == manaPotion.id)
        //{
        //    AddManaPotion();
        //}

        //return PurchaseProcessingResult.Complete;

        if(String.Equals(purchaseEvent.purchasedProduct.definition.id, healthPotion.id, StringComparison.Ordinal))
        {
            AddHealthPotion();
            return PurchaseProcessingResult.Complete;
        }
        else if (String.Equals(purchaseEvent.purchasedProduct.definition.id, manaPotion.id, StringComparison.Ordinal))
        {
            AddManaPotion();
            return PurchaseProcessingResult.Complete;
        }
        else
        {
            return PurchaseProcessingResult.Pending;
        }

        

    }

    public void AddHealthPotion()
    {
        healthPotion.amount = PlayerPrefs.GetInt("HealthPotion");
        healthPotion.amount++;
        PlayerPrefs.SetInt("HealthPotion", healthPotion.amount);
        
        
    }

    public void AddManaPotion()
    {
        manaPotion.amount = PlayerPrefs.GetInt("ManaPotion");
        manaPotion.amount++;
        PlayerPrefs.SetInt("ManaPotion", manaPotion.amount);

        
        
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
