using UnityEngine;
using UnityEngine.UI;
using GD;
public class LocalPrice : MonoBehaviour
{
    Text priceText;
    void Awake()
    {
        priceText = GetComponent<Text>();
    }
    //public PackType itemType;
    //void Start()
    //{
    //    print("My Item Type: " + itemType);
    //    //for (int i = 0; i < MyIAPManager_VoxelSteveLabs.Instance.purchaseIDController.Length; i++)
    //    //{
    //    //    if (itemType == MyIAPManager_VoxelSteveLabs.Instance.purchaseIDController[i].itemType)
    //    //    {
    //    //        priceText.text = MyIAPManager_VoxelSteveLabs.Instance.purchaseIDController[i].localPrice;
    //    //        break;
    //    //    }
    //    //}
    //}
}