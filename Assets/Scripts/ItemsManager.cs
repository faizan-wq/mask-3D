using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{

    public static ItemsManager Instance;
   
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
            liquidItemGroup = LevelUIManager.Instance.bottleItemsImages;


    }



    private void CreatehammeingItems()
    {
        childNumber = 0;
        foreach (var item in mashingItems)
        {
            
            //GameObject newObject = Instantiate(ItemImage);
            GameObject newObject = mashingItemGroup.GetComponent<RectTransform>().GetChild(childNumber).gameObject;


            newObject.name = item.hammeringItem.sprite.name;
            //newObject.AddComponent<Image>();
            

             Button btn = newObject.GetComponent<Button>();
            
            btn.transform.GetChild(0).GetComponent<Image>().sprite = item.hammeringItem.sprite;
            //newObject.GetComponent<RectTransform>().parent = mashingItemGroup.GetComponent<RectTransform>();
            newObject.transform.localScale = Vector3.one;
            btn.onClick.AddListener(() => {

                int num = item.GetHashCode();
                selectedItem = item.hammeringItem;

                MaskMakingLevel.Instance.hammeringItemLimit = item.count;
                //MaskMakingLevel.Instance.knifeController.knifeStartingPosition.position = item.knifeStartingPositions;
                //MaskMakingLevel.Instance.knifeController.knifeEndingPosition.position = item.knifeEndingPositions;

                MaskMakingLevel.Instance.hammerController.SelectHammeringTutorial(0, false);
                MaskMakingLevel.Instance.hammerController.SelectHammeringTutorial(1, true);
                // InstatitateObject(item.hammeringItem.prefab, MaskMakingLevel.Instance.hammerController.firstHammerPosition.position);
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
            //GameObject newObject = new GameObject();
            GameObject newObject = choopingItemGroup.GetComponent<RectTransform>().GetChild(childNumber).gameObject;
            //GameObject newObject =  Instantiate(ItemImage);


            newObject.name = item.choopingItems.sprite.name;
            //newObject.AddComponent<Image>();
            Button btn = newObject.GetComponent<Button>();

            //btn.image.sprite = item.choopingItems.sprite;
            btn.transform.GetChild(0).GetComponent<Image>().sprite = item.choopingItems.sprite;
            //newObject.GetComponent<RectTransform>().parent = choopingItemGroup.GetComponent<RectTransform>();
            newObject.transform.localScale = Vector3.one;
            btn.onClick.AddListener(() => {

                int num = item.GetHashCode();
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
        childNumber = 0;
        int number=0;
        
        foreach (var item in liquidItems)
        {
            //GameObject newObject = new GameObject();
            //GameObject newObject = Instantiate(ItemImage);
            GameObject newObject = choopingItemGroup.GetComponent<RectTransform>().GetChild(childNumber).gameObject;

            int num = number;
            newObject.name = item.sprite.name;
            //newObject.AddComponent<Image>();
            Button btn = newObject.GetComponent<Button>();

            //btn.image.sprite = item.sprite;
            btn.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
            //newObject.GetComponent<RectTransform>().parent = liquidItemGroup.GetComponent<RectTransform>();
            btn.onClick.AddListener(() => {

               
                MaskMakingLevel.Instance.bottleController.SelectedBottle(num);

                //InstatitateObject(item.prefab, MaskMakingLevel.Instance.bottleController.bottleStarting.position);
                btn.GetComponentInParent<ScrollRect>().gameObject.SetActive(false);
                choopingItemGroup.GetComponent<RectTransform>().parent.parent.gameObject.SetActive(false);
                btn.interactable = false;
                

            });

            number++;

            childNumber++;
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
