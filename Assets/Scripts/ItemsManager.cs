using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{

    public static ItemsManager Instance;
    [HideInInspector]public MaskMakingLevel maskMakingLevel;
    [SerializeField] private GridLayoutGroup choopingItemGroup;
    [HideInInspector] public Item selectedItem;
   
    [SerializeField] private List<Chopping_Item_Properties> choopingItems;
    
    [SerializeField] public List<Hammering_Item_Properties> mashingItems;
    [SerializeField] private GridLayoutGroup mashingItemGroup;


    [SerializeField] public List<Item> liquidItems;
    [SerializeField] private GridLayoutGroup liquidItemGroup;

    [SerializeField] private GameObject ItemImage;
    int childNumber = 0;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }

    }

    public void InvokeAfterWait()
    {

        if (LevelUIManager.Instance == null)
            return;

        maskMakingLevel = MaskMakingLevel.Instance;


        if (LevelUIManager.Instance.choppingItemsImages != null)
        {
            choopingItemGroup = LevelUIManager.Instance.choppingItemsImages;
            CreateChoopingItems();
        }
        if (LevelUIManager.Instance.hammeringItemsImages != null)
        {
            mashingItemGroup = LevelUIManager.Instance.hammeringItemsImages;
            CreatehammeingItems();
        }

        if (LevelUIManager.Instance.bottleItemsImages != null)
        {
            liquidItemGroup = LevelUIManager.Instance.bottleItemsImages;
            ItemsManager.Instance.CreateliquidItems();


        }

    }



    private void CreatehammeingItems()
    {
        childNumber = 0;
        foreach (var item in mashingItems)
        {
            
            GameObject newObject = mashingItemGroup.GetComponent<RectTransform>().GetChild(childNumber).gameObject;
            Button btn = newObject.GetComponent<Button>();

            if (childNumber >= 1)
            {
                btn.interactable = false;
                Button adButton = newObject.transform.GetChild(1).GetComponent<Button>();
                adButton.onClick.AddListener(() => {

                    AdMob_GF.ShowRewardedAdmobOrInterstitial();
                    btn.interactable = true;
                    adButton.gameObject.SetActive(false);

                });

            }

            newObject.name = item.hammeringItem.sprite.name;
            




            btn.transform.GetChild(0).GetComponent<Image>().sprite = item.hammeringItem.sprite;
            newObject.transform.localScale = Vector3.one;
            btn.onClick.AddListener(() => {

                int num = item.GetHashCode();
                selectedItem = item.hammeringItem;

               maskMakingLevel.hammeringItemLimit = item.count;
            
                maskMakingLevel.hammerController.SelectHammeringTutorial(0, false);
                maskMakingLevel.hammerController.SelectHammeringTutorial(1, true);
                btn.GetComponentInParent<ScrollRect>().gameObject.SetActive(false);
                choopingItemGroup.GetComponent<RectTransform>().parent.parent.gameObject.SetActive(false);
                btn.interactable = false;

            });

            childNumber++;

        }
    }



    private void CreateChoopingItems()
    {
        childNumber = 0;
        
        foreach (var item in choopingItems)
        {
            

            GameObject newObject = choopingItemGroup.GetComponent<RectTransform>().GetChild(childNumber).gameObject;
            Button btn = newObject.GetComponent<Button>();
            if (childNumber>=1)
            {
                btn.interactable = false;
                Button adButton = newObject.transform.GetChild(1).GetComponent<Button>();
                adButton.onClick.AddListener(()=> {

                    AdMob_GF.ShowRewardedAdmobOrInterstitial();
                    btn.interactable = true;
                    adButton.gameObject.SetActive(false);

                });

            }

            newObject.name = item.choopingItems.sprite.name;
           

            btn.transform.GetChild(0).GetComponent<Image>().sprite = item.choopingItems.sprite;
            newObject.transform.localScale = Vector3.one;
            btn.onClick.AddListener(() => {

               
                selectedItem = item.choopingItems;

                MaskMakingLevel.Instance.knifeController.knifeStartingPosition.position = item.knifeStartingPositions;
                MaskMakingLevel.Instance.knifeController.knifeEndingPosition.position = item.knifeEndingPositions;

                MaskMakingLevel.Instance.knifeController.KnifeMoveToChoppingPosition();
                InstatitateObject(item.choopingItems.prefab, MaskMakingLevel.Instance.choppingItemPosition.position);
                btn.GetComponentInParent<ScrollRect>().gameObject.SetActive(false);
                choopingItemGroup.GetComponent<RectTransform>().parent.parent.gameObject.SetActive(false);
                btn.interactable = false;

            });

            childNumber++;

        }
    }






    private void InstatitateObject(GameObject obj, Vector3 startingPosition)
    {
        GameObject temp = Instantiate(obj);
       
        obj.transform.position = startingPosition;
    }




    public void CreateliquidItems()
    {
        int temp_counter = 0;
        int number=0;
        
        foreach (var item in liquidItems)
        {
            
            GameObject newObject = liquidItemGroup.GetComponent<RectTransform>().GetChild(temp_counter).gameObject;
            Button btn = newObject.GetComponent<Button>();

            int num = number;

            if (num >= 1)
            {
                btn.interactable = false;
                Button adButton = newObject.transform.GetChild(1).GetComponent<Button>();
                adButton.onClick.AddListener(() => {

                    AdMob_GF.ShowRewardedAdmobOrInterstitial();
                    btn.interactable = true;
                    adButton.gameObject.SetActive(false);

                });

            }



            newObject.name = item.sprite.name;
          
            btn.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
        
            btn.onClick.AddListener(() =>
            {


                MaskMakingLevel.Instance.bottleController.SelectedBottle(num);

               
                btn.GetComponentInParent<ScrollRect>().gameObject.SetActive(false);
                choopingItemGroup.GetComponent<RectTransform>().parent.parent.gameObject.SetActive(false);
                btn.interactable = false;


            });

            number++;

            temp_counter++;
        }
    }






   










}
[System.Serializable]
public class Hammering_Item_Properties
{
    public Item hammeringItem;
    public int count;

}




[System.Serializable]
public class Chopping_Item_Properties
{
    public Item choopingItems;
    public Vector3 knifeStartingPositions;
    public Vector3 knifeEndingPositions;
}
