using System;
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
    [HideInInspector]public SoundManager soundManager;


    int childNumber = 0;

    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }

    }
    private void Start()
    {
        soundManager = ParticleManager.Instance.soundManager;
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
                soundManager.PlayQuickSoundClip("ui button");
                btn.interactable = false;

            });

            if (childNumber >= 2)
            {
                btn.interactable = false;
                Button adButton = newObject.GetComponent<RectTransform>().GetChild(1).GetComponent<Button>();
                adButton.gameObject.SetActive(true);
                adButton.onClick.AddListener(() => {

                    GD.Controller.Instance.RewardedVideo(result => { 
                    
                    if(result)
                        {
                            //btn.interactable = true;
                            btn.onClick?.Invoke();
                            adButton.gameObject.SetActive(false);
                        }
                  
                    
                    });
                  
                  

                });

            }

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
                soundManager.PlayQuickSoundClip("ui button");
                btn.interactable = false;

            });


            if (childNumber >= 2)
            {
                btn.interactable = false;
                Button adButton = newObject.GetComponent<RectTransform>().GetChild(1).GetComponent<Button>();
                adButton.gameObject.SetActive(true);
                adButton.onClick.AddListener(() => {


                    GD.Controller.Instance.RewardedVideo(result => {

                        if(result)
                        {
                            //btn.interactable = true;
                            btn.onClick?.Invoke();
                            adButton.gameObject.SetActive(false);
                        }
                    

                    });


                        //AdMob_GF.ShowRewardedAdmobOrInterstitial();
                  

                });

            }
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

        if(LevelSpawner.isCokeLevel)
        {
            Item temp = new Item(liquidItems[5]);
            Item item2 = new Item(liquidItems[0]);

            
             liquidItems[0] = temp;
            liquidItems[5] = item2;


        }
        if (LevelSpawner.isMilk)
        {
            Item temp = new Item(liquidItems[4]);
            Item item2 = new Item(liquidItems[0]);

            liquidItems[0] = temp;
            liquidItems[4] = item2;
        }





        int temp_counter = 0;
        int number=0;
        
        foreach (var item in liquidItems)
        {
            
            GameObject newObject = liquidItemGroup.GetComponent<RectTransform>().GetChild(temp_counter).gameObject;
            Button btn = newObject.GetComponent<Button>();

            int num = number;

          


            newObject.name = item.sprite.name;
          
            btn.transform.GetChild(0).GetComponent<Image>().sprite = item.sprite;
        
            btn.onClick.AddListener(() =>
            {

               

                if(LevelSpawner.isCokeLevel)
                {
                    if(num==5)
                    {
                        MaskMakingLevel.Instance.bottleController.SelectedBottle(0);
                    }
                    else if(num == 0)
                    {
                        MaskMakingLevel.Instance.bottleController.SelectedBottle(5);
                    }
                   


                }
                else if(LevelSpawner.isMilk)
                {
                    if (num == 4)
                    {
                        MaskMakingLevel.Instance.bottleController.SelectedBottle(0);
                    }
                    else if (num == 0)
                    {
                        MaskMakingLevel.Instance.bottleController.SelectedBottle(4);
                    }

                }
                else
                {
                    MaskMakingLevel.Instance.bottleController.SelectedBottle(num);
                }
              
                if(item.prefab.name== "WineBottle")
                {
                    soundManager.PlayQuickSoundClip("ChampagneOpen");
                }


               
                btn.GetComponentInParent<ScrollRect>().gameObject.SetActive(false);
                choopingItemGroup.GetComponent<RectTransform>().parent.parent.gameObject.SetActive(false);
                btn.interactable = false;
                soundManager.PlayQuickSoundClip("ui button");

            });
            if (num >= 1)
            {
                btn.interactable = false;
                Button adButton = newObject.GetComponent<RectTransform>().GetChild(1).GetComponent<Button>();
                adButton.gameObject.SetActive(true);
                adButton.onClick.AddListener(() => {

                   

                    GD.Controller.Instance.RewardedVideo(result => {

                        if (result)
                        {
                            //btn.interactable = true;
                            btn.onClick?.Invoke();
                            adButton.gameObject.SetActive(false);
                        }


                    });


                });

            }

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
