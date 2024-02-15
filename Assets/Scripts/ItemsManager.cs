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
    
    [SerializeField] public List<Item> mashingItems;
    [SerializeField] private GridLayoutGroup mashingItemGroup;


    [SerializeField] public List<Item> liquidItems;
    [SerializeField] private GridLayoutGroup liquidItemGroup;
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CreateChoopingItems();
        
    }




    private void CreateChoopingItems()
    {
        
        foreach (var item in choopingItems)
        {
            GameObject newObject = new GameObject();
           
            newObject.name = item.choopingItems.sprite.name;
            newObject.AddComponent<Image>();
            Button btn = newObject.AddComponent<Button>();
            Debug.Log(item.choopingItems.sprite);
            btn.image.sprite = item.choopingItems.sprite;
            newObject.GetComponent<RectTransform>().parent = choopingItemGroup.GetComponent<RectTransform>();
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

           

        }
    }






    private void InstatitateObject(GameObject obj, Vector3 startingPosition)
    {
        GameObject temp = Instantiate(obj);
       
        obj.transform.position = startingPosition;
    }




    public void CreateliquidItems()
    {

        int number=0;
        
        foreach (var item in liquidItems)
        {
            GameObject newObject = new GameObject();
            int num = number;
            newObject.name = item.sprite.name;
            newObject.AddComponent<Image>();
            Button btn = newObject.AddComponent<Button>();
           
            btn.image.sprite = item.sprite;
            newObject.GetComponent<RectTransform>().parent = liquidItemGroup.GetComponent<RectTransform>();
            btn.onClick.AddListener(() => {

               
                MaskMakingLevel.Instance.bottleController.SelectedBottle(num);

                //InstatitateObject(item.prefab, MaskMakingLevel.Instance.bottleController.bottleStarting.position);
                btn.GetComponentInParent<ScrollRect>().gameObject.SetActive(false);
                choopingItemGroup.GetComponent<RectTransform>().parent.parent.gameObject.SetActive(false);
                btn.interactable = false;
                

            });

            number++;


        }
    }






   










}
[System.Serializable]
public class Chopping_Item_Properties
{
    public Item choopingItems;
    public Vector3 knifeStartingPositions;
    public Vector3 knifeEndingPositions;
}
