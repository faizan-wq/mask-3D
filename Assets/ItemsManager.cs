using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{

    public static ItemsManager Instance;
    [SerializeField] public List<Item> choopingItems;
    [SerializeField] private GridLayoutGroup choopingItemGroup;
    [HideInInspector] public Item selectedItem;
    
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
            selectedItem = item;
            newObject.name = item.sprite.name;
            newObject.AddComponent<Image>();
            Button btn = newObject.AddComponent<Button>();
            Debug.Log(item.sprite);
            btn.image.sprite = item.sprite;
            newObject.GetComponent<RectTransform>().parent = choopingItemGroup.GetComponent<RectTransform>();
            btn.onClick.AddListener(() => {

               
                MaskMakingLevel.Instance.knifeController.KnifeMoveToChoppingPosition();
                InstatitateObject(item.prefab, MaskMakingLevel.Instance.choppingItemPosition.position);
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

        
        foreach (var item in liquidItems)
        {
            GameObject newObject = new GameObject();

            newObject.name = item.sprite.name;
            newObject.AddComponent<Image>();
            Button btn = newObject.AddComponent<Button>();
            Debug.Log(item.sprite);
            btn.image.sprite = item.sprite;
            newObject.GetComponent<RectTransform>().parent = liquidItemGroup.GetComponent<RectTransform>();
            btn.onClick.AddListener(() => {


                MaskMakingLevel.Instance.bottleController.SelectedBottle(0);
                //InstatitateObject(item.prefab, MaskMakingLevel.Instance.bottleController.bottleStarting.position);
                btn.GetComponentInParent<ScrollRect>().gameObject.SetActive(false);
                choopingItemGroup.GetComponent<RectTransform>().parent.parent.gameObject.SetActive(false);
                btn.interactable = false;
                

            });
          



        }
    }






   










}
