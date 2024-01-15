using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsManager : MonoBehaviour
{

    public static ItemsManager Instance;
    [SerializeField] public List<Item> choopingItems;
    [SerializeField] private GridLayoutGroup choopingItemGroup; 
    [SerializeField] public List<Item> mashingItems;
    [SerializeField] private GridLayoutGroup mashingItemGroup;
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
            newObject.transform.parent = choopingItemGroup.transform;
            newObject.name = item.sprite.name;
            Button btn = newObject.AddComponent<Button>();
            btn.image.sprite = item.sprite;

            btn.onClick.AddListener(() => {

                InstatitateObject(item.prefab, MaskMakingLevel.Instance.choppingItemPosition.position);
                btn.interactable = false;

            });
            


        }
    }
    private void InstatitateObject(GameObject obj, Vector3 startingPosition)
    {
        GameObject temp = Instantiate(obj);
       
        obj.transform.position = startingPosition;
    }


}

