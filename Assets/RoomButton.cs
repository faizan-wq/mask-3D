using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomButton : MonoBehaviour
{
    public ButtonType type;
    public RoomType roomType;
    public int cashRequiredToUnlock;
    public GameObject itemToUnlock;
    private Button button;
    public RoomsController roomsController;
    public int cameraPositionNumber;
    private Text price;
    private GameObject addButton;
    private ParticleManager particleManager;

    // Start is called before the first frame update
    private void Awake()
    {
        button = GetComponent<Button>();
        price = transform.GetChild(0).GetChild(0).GetChild(0).GetComponent<Text>();
        price.text = cashRequiredToUnlock.ToString();
        addButton = transform.GetChild(0).GetChild(1).gameObject;
    }
    private void Start()
    {
        particleManager = ParticleManager.Instance;
        UnlockComplete();
    }
    private void Update()
    {
        CheckButtonStatus();
    }


    private void CheckButtonStatus()
    {

     

        if (cashRequiredToUnlock > PlayerPrefs.GetInt("Cash"))
        {
            addButton.gameObject.SetActive(true);
            price.transform.parent.gameObject.SetActive(false);
         
           


        }
        else
        {

            addButton.gameObject.SetActive(false);
            price.transform.parent.gameObject.SetActive(true);
         
            //Invoke(nameof(UnlockComplete), 2);
        }
       
        
      








    }
    public void UnlockComplete()
    {
        GameObject obj = gameObject;
        string storingName = type.ToString() + roomType.ToString() + obj.name;


        if (PlayerPrefs.GetInt(storingName) == 1)
        {
            obj.SetActive(false);
            itemToUnlock.transform.localScale = Vector3.one;
            itemToUnlock.SetActive(true);
            return;
        }

      
       
        button.onClick.AddListener(() => {

          

            if (cashRequiredToUnlock > PlayerPrefs.GetInt("Cash"))
            {

                
                GD.Controller.Instance.RewardedVideo(result => {

                    if (result)
                    {
                       
                        StartCoroutine(CreateChangeButton(obj, storingName));

                    }
                    


                });
               

            }
            else
            {

              
               
               
                PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") - cashRequiredToUnlock);
                StartCoroutine(CreateChangeButton(obj, storingName));

            }


           




        });
       

    }
  
    IEnumerator CreateChangeButton(GameObject obj, string storingName)
    {
       
        roomsController.SetSecondVirtualcameratarget(itemToUnlock.transform, cameraPositionNumber);
        yield return new WaitForSeconds(3);
        PlayerPrefs.SetInt(storingName, 1);
        particleManager.soundManager.PlayQuickSoundClip("start_01");
        particleManager.PlayAnimation("Unlocking Room", itemToUnlock.transform.position);
        itemToUnlock.SetActive(true);


        obj.SetActive(false);
    }



}
public enum ButtonType
{
    Rewarded,
    Cash
}
public enum RoomType
{
    Gaming,
    Isometric,
    Japanese,
    Beds
}