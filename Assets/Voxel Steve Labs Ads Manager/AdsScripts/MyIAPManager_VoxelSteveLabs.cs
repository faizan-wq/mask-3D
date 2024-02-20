//#if UNITY_ANDROID || UNITY_IPHONE || UNITY_STANDALONE_OSX || UNITY_TVOS
//// You must obfuscate your secrets using Window > Unity IAP > Receipt Validation Obfuscator
//// before receipt validation will compile in this sample.
//// #define RECEIPT_VALIDATION

//#endif

//using System;
//using UnityEngine.Purchasing.Security;
//using UnityEngine;
//using UnityEngine.Purchasing;
//using System.Globalization;
//using GD;

//public class MyIAPManager_VoxelSteveLabs : MonoBehaviour, IStoreListener
//{
//    public static MyIAPManager_VoxelSteveLabs Instance;
//    private CrossPlatformValidator validator;
//    private string m_LastTransationID;
//    private string m_LastReceipt;


//    private IStoreController controller; // The Unity Purchasing system.
//    private IExtensionProvider extensions; // The store-specific Purchasing subsystems.
//                                           //	[SerializeField]
//    private string currentPurchaseID = null;
//    public static string kProductIDSubscription = "subscription";

//    // Apple App Store-specific product identifier for the subscription product.
//    [SerializeField]
//    private static string kProductNameAppleSubscription = "com.unity3d.subscription.new";
//    [SerializeField]
//    // Google Play Store-specific product identifier subscription product.
//    private static string kProductNameGooglePlaySubscription = "com.unity3d.subscription.original";
//    [SerializeField]
//    public PurchaseID_Controller_VoxelSteveLabs[] purchaseIDController;

//    public string getLoaclPrice(string sku)
//    {
//        for (int i = 0; i < purchaseIDController.Length; i++)
//        {
//            if (purchaseIDController[i].purchaseID == sku)
//                return purchaseIDController[i].localPrice;
//        }

//        return "$0.99"; 
//    }
//    public string getLoaclPrice(PackType type)
//    {
//        for (int i = 0; i < purchaseIDController.Length; i++)
//        {
//            if (purchaseIDController[i].itemType == type)
//                return purchaseIDController[i].localPrice;
//        }

//        return "$0.99"; 
//    }


//    GD.PackType CurrentItemType;
//    NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
//    private void Awake()
//    {
//        Instance = this;
//    }
//    public void Start()
//    {
//        DontDestroyOnLoad(gameObject);

//        var module = StandardPurchasingModule.Instance();
//        var builder = ConfigurationBuilder.Instance(module);

//        string receipt = builder.Configure<IAppleConfiguration>().appReceipt;
//        //		In App Purchases may be restricted in a device’s settings, which can be checked for as follows:
//        bool canMakePayments = builder.Configure<IAppleConfiguration>().canMakePayments;
//        foreach (PurchaseID_Controller_VoxelSteveLabs pIDC in purchaseIDController)
//        {
//            builder.AddProduct(pIDC.purchaseID, pIDC.purchaseableType);
//            float temp = 0.99f;
//            if(float.TryParse(pIDC.price,out temp))
//            {
//                pIDC.localPrice = temp.ToString("C", nfi) ;
//            }
//        }

//        builder.AddProduct(kProductIDSubscription, ProductType.Subscription, new IDs {
//            { kProductNameGooglePlaySubscription, GooglePlay.Name },
//            { kProductNameAppleSubscription, MacAppStore.Name }
//        });

//        UnityPurchasing.Initialize(this, builder);
       
//    }
//    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
//    {
//        this.controller = controller;
//        this.extensions = extensions;

//        extensions.GetExtension<IAppleExtensions>().RestoreTransactions(result =>
//        {
//            if (result)
//            {
//                Debug.Log("Result for restoration started is true");
//            }
//            else
//            {
//                // Restoration failed.
//            }
//        });

//        for (int i = 0; i < purchaseIDController.Length; i++)
//        {
//            purchaseIDController[i].localPrice = controller.products.WithID(purchaseIDController[i].purchaseID).metadata.localizedPriceString;
//        }
//    }
//    public void OnInitializeFailed(InitializationFailureReason error)
//    {

//    }
    
    
    
    

    
    
    
//    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs e)
//    {
//        bool validPurchase = true;
//        m_LastTransationID = e.purchasedProduct.transactionID;
//        m_LastReceipt = e.purchasedProduct.receipt;


////#if UNITY_ANDROID || UNITY_IOS || UNITY_STANDALONE_OSX
////        try
////        {
////            validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
////            var result = validator.Validate(e.purchasedProduct.receipt);

////            foreach (IPurchaseReceipt productReceipt in result)
////            {
////                Debug.Log(productReceipt.productID);
////                Debug.Log(productReceipt.purchaseDate);
////                Debug.Log(productReceipt.transactionID);
////            }
////        }

////        catch (IAPSecurityException)
////        {
////            Debug.Log("Invalid receipt, not unlocking content");
////            validPurchase = false;
////        }
////#endif



//        if (validPurchase)
//        {
//            Debug.Log("Purchase is valid");
//            if (String.Equals(e.purchasedProduct.definition.id, currentPurchaseID, StringComparison.Ordinal))
//            {
//                Debug.Log("Product Type : " + e.purchasedProduct.definition.type);

//                for (int i = 0; i < purchaseIDController.Length; i++)
//                {
//                    if (purchaseIDController[i].purchaseID == e.purchasedProduct.definition.id)
//                    {
//                        Data_VoxelSteveLabs.PurchaseSuccessful(CurrentItemType);
                        
                        
//                        // var product = controller.products.WithID(currentPurchaseID);
//                        // string currency = product.metadata.isoCurrencyCode;
//                        // int amount = decimal.ToInt32 (product.metadata.localizedPrice * 100);
//                        // GameAnalyticsSDK.GameAnalytics.NewBusinessEvent(currency, amount,purchaseIDController[i].itemType.ToString(), currentPurchaseID, "my_cart_type");
                        
                        
//                        break;
//                    }
//                    Debug.LogWarning("Target Purchased Succefully of ID " + e.purchasedProduct.definition.id);
//                }
//            }
//        }

//        return PurchaseProcessingResult.Complete;
//    }
//    public void OnPurchaseFailed(Product i, PurchaseFailureReason p)
//    {
//    }
//    public void BuyProductID(string productId, GD.PackType PurchaseType)
//    {
//        CurrentItemType = PurchaseType;
//        currentPurchaseID = productId;
//        Debug.Log("Purchasing id received is " + productId);
//        Debug.Log(string.Format("Purchasing product id is ", productId));
//        // If Purchasing has been initialized ...
//        if (IsInitialized())
//        {
//            // ... look up the Product reference with the general product identifier and the Purchasing 
//            // system's products collection.

//            //for Cash



//            Product product = controller.products.WithID(productId);

//            // If the look up found a product for this device's store and that product is ready to be sold ... 
//            if (product != null && product.availableToPurchase)
//            {
//                Debug.Log(string.Format("Purchasing product asychronously: '{0}'", product.definition.id));
//                // ... buy the product. Expect a response either through ProcessPurchase or OnPurchaseFailed 
//                // asynchronously.
//                controller.InitiatePurchase(product);
//            }
//            // Otherwise ...
//            else
//            {
//                // ... report the product look-up failure situation  
//                Debug.Log("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
//            }
//        }
//        // Otherwise ...
//        else
//        {
//            // ... report the fact Purchasing has not succeeded initializing yet. Consider waiting longer or 
//            // retrying initiailization.
//            Debug.Log("BuyProductID FAIL. Not initialized.");
//        }
//    }

//    private bool IsInitialized()
//    {
//        return controller != null && extensions != null;
//    }

//    // Restore purchases previously made by this customer. Some platforms automatically restore purchases, like Google. 
//    // Apple currently requires explicit purchase restoration for IAP, conditionally displaying a password prompt.
//    public void RestorePurchases()
//    {
//        // If Purchasing has not yet been set up ...
//        if (!IsInitialized())
//        {
//            // ... report the situation and stop restoring. Consider either waiting longer, or retrying initialization.
//            Debug.Log("RestorePurchases FAIL. Not initialized.");
//            return;
//        }

//        // If we are running on an Apple device ... 
//        if (Application.platform == RuntimePlatform.IPhonePlayer ||
//            Application.platform == RuntimePlatform.OSXPlayer)
//        {
//            // ... begin restoring purchases
//            Debug.Log("RestorePurchases started ...");

//            // Fetch the Apple store-specific subsystem.
//            var apple = extensions.GetExtension<IAppleExtensions>();
//            // Begin the asynchronous process of restoring purchases. Expect a confirmation response in 
//            // the Action<bool> below, and ProcessPurchase if there are previously purchased products to restore.
//            apple.RestoreTransactions((result) =>
//            {
//                Debug.Log("RestorePurchases continuing: " + result + ". If no further messages, no purchases available to restore.");
//                for (int i = 0; i < purchaseIDController.Length; i++)
//                {
//                    if (purchaseIDController[i].purchaseableType == ProductType.NonConsumable)
//                    {
//                        BuyProductID(purchaseIDController[i].purchaseID, purchaseIDController[i].itemType);
//                    }
//                }
//            });

//        }
//        // Otherwise ...
//        else
//        {
//            // We are not running on an Apple device. No work is necessary to restore purchases.
//            Debug.Log("RestorePurchases FAIL. Not supported on this platform. Current = " + Application.platform);
//        }
//    }

//    public void InAppCaller(GD.PackType InAppType)
//    {
//        for (int i = 0; i < purchaseIDController.Length; i++)
//        {
//            if (purchaseIDController[i].itemType == InAppType)
//            {
//                BuyProductID(purchaseIDController[i].purchaseID, InAppType);
//                break;
//            }
//        }
//    }

//}