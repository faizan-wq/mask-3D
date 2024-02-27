using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DailyRewardManager : MonoBehaviour
{
    public static DailyRewardManager Instance;
    public RectTransform DailyRewardParent;
    public int[] dailyRewardValues;
    public GameObject target;
    private const string previousDate="PreviousDate";
    private const string todayDate = "todayDate";
    private const string Days = "DaysReward";
    
    public List<DailyReward_Item_Property> dailyReward_Item_Properties;
    private Transform temp_DailyReward=null;
    private DailyReward_Item_Property temp_Daily;
    
    
    [Header("Timer")]
    public Text timerText;
    public int timeRestartAfterSeconds=21600;
    private const string timerStoredValue = "timerStoredValue";
    private const string timerDailyRewardAdButtonClicked = "DailyRewardAdButtonClicked";
    public GameObject buttonWithoutAd;
    public GameObject buttonWithAd;
    public GameObject exclamationMark;
    public FlyingDiamond flyingDiamondPrefab;
    private WaitForSecondsRealtime waitForTimer =new WaitForSecondsRealtime(1);
    private bool allowTimerDecreasing = true;
    int tempTimerValue;
    


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        CreateDailyRewardValue();
        GetdailyReward();
        EnableDailyRewardAd();
        TimerValueChange();

    }


    private void Update()
    {
        tempTimerValue = PlayerPrefs.GetInt(timerStoredValue);
        if (tempTimerValue > 0)
        {
            
            StartCoroutine(DecreasingTimerValue());
            TimerValueChange();

        }
        
    }


    private void TimerValueChange()
    {
        string time_value_string="";

        if(tempTimerValue>3600)
        {
            time_value_string +="0"+ (tempTimerValue / 3600).ToString()+ " : ";


            if((tempTimerValue % 3600) / 60>9)
            {
                time_value_string += ((tempTimerValue % 3600) / 60).ToString();
            }
            else
            {
                time_value_string +="0" +((tempTimerValue % 3600) / 60).ToString();
            }
            time_value_string += " : ";


            if (tempTimerValue % 60 > 9)
            {
                time_value_string += (tempTimerValue % 60).ToString();
            }
            else
            {
                time_value_string += "0" + (tempTimerValue % 60).ToString();
            }



        }
        else if (tempTimerValue > 60)
        {

            time_value_string +=  "00 : ";

            if ((tempTimerValue % 3600) / 60 > 9)
            {
                time_value_string += ((tempTimerValue % 3600) / 60).ToString();
            }
            else
            {
                time_value_string += "0" + ((tempTimerValue % 3600) / 60).ToString();
            }
            time_value_string += " : ";


            if (tempTimerValue % 60 > 9)
            {
                time_value_string += (tempTimerValue % 60).ToString();
            }
            else
            {
                time_value_string += "0" + (tempTimerValue % 60).ToString();
            }





        }
        else if (tempTimerValue > 0)
        {

            time_value_string = "00 : 00 : ";
            if (tempTimerValue % 60 > 9)
            {
                time_value_string += (tempTimerValue % 60).ToString();
            }
            else
            {
                time_value_string += "0" + (tempTimerValue % 60).ToString();
            }

        }
        else
        {
            time_value_string = "Get Your Reward";
        }

        timerText.text = time_value_string;

    }


    private IEnumerator DecreasingTimerValue()
    {
        allowTimerDecreasing = false;



        yield return waitForTimer;

        PlayerPrefs.SetInt(timerStoredValue, tempTimerValue - 1);
        allowTimerDecreasing = true;


    }

    private void CreateDailyRewardValue()
    {
        for (int i = 0; i < DailyRewardParent.childCount; i++)
        {
            temp_DailyReward = DailyRewardParent.GetChild(i);
           
            dailyReward_Item_Properties.Add(new DailyReward_Item_Property(temp_DailyReward.GetChild(4).gameObject, temp_DailyReward.GetChild(0).gameObject, temp_DailyReward.GetChild(3).GetComponent<Text>(), dailyRewardValues[i]));
           
        }

    }

    private void exclamatoryMarkStatus(bool status)
    {
        if (PlayerPrefs.GetString("RoomsTutorialCheck") == "Complete")
        {


            exclamationMark.SetActive(status);
        }    
        else
        {
            exclamationMark.SetActive(false);
        }
    }


    private void GetdailyReward()
    {
        
        //////////////////////       Latest Logic with Numbers                          ////////////////////
        
        
        if(PlayerPrefs.GetInt(timerStoredValue)!=0)
        {
            
            buttonWithoutAd.SetActive(false);
            buttonWithoutAd.GetComponent<Button>().interactable = true;
            exclamatoryMarkStatus(false);
        }
        else
        {
           
            exclamatoryMarkStatus(true);

        }
      
        if(PlayerPrefs.GetInt(timerDailyRewardAdButtonClicked)==0 && PlayerPrefs.GetInt(timerStoredValue) != 0)
        {
           
            buttonWithAd.SetActive(true);
            
           
        }
        else
        {
            buttonWithAd.SetActive(false);
            buttonWithAd.GetComponent<Button>().interactable = true;

        }

        
        ///////////////////////     Previous Logic By Date                 /////////////////////
        
        //if (PlayerPrefs.GetString(previousDate) == "")
        //{
        //    DateTime day = DateTime.Now;
        //    PlayerPrefs.SetString(previousDate, day.ToString());
        //    PlayerPrefs.SetString(todayDate, day.ToString());
        //    PlayerPrefs.SetInt(Days, 0);
        //}
        //else
        //{
        //    var today = DateTime.Now;
        //    var previousDay = DateTime.Parse(PlayerPrefs.GetString(previousDate).ToString());
        //    var difference = today - previousDay;

        //    if (difference.Days > 0)
        //    {
        //        PlayerPrefs.SetString(previousDate,today.ToString());
        //        PlayerPrefs.SetInt(Days, PlayerPrefs.GetInt(Days )+1);
        //    }

        //}
        



    }
    private void EnableDailyRewardAd()
    {
        int Day = PlayerPrefs.GetInt(Days);
      
        string value = PlayerPrefs.GetString("Days" + PlayerPrefs.GetInt(Days).ToString());

        for (int i = 0; i < Day; i++)
        {
            dailyReward_Item_Properties[i].notSelected.SetActive(true);
            dailyReward_Item_Properties[i].selected.SetActive(true);


        }


        if(value=="")
        {
            dailyReward_Item_Properties[Day].notSelected.SetActive(true);
          
        }
        else if(value== "selected")
        {
            dailyReward_Item_Properties[Day].notSelected.SetActive(true);
            dailyReward_Item_Properties[Day].selected.SetActive(true);
           
          
        }




    }

    public void RewardWithoutAd()
    {


        buttonWithoutAd.GetComponent<Button>().interactable = false;



        if (PlayerPrefs.GetInt(timerStoredValue) == 0)
        {

            PlayerPrefs.SetInt(timerStoredValue, timeRestartAfterSeconds);

        }
        else if (PlayerPrefs.GetInt(timerDailyRewardAdButtonClicked) == 0)
        {
            PlayerPrefs.SetInt(timerDailyRewardAdButtonClicked, 1);
        }

        GetdailyReward();


        int Day = PlayerPrefs.GetInt(Days);
        string value = PlayerPrefs.GetString("Days" + PlayerPrefs.GetInt(Days).ToString());

        if (value == "")
        {
            dailyReward_Item_Properties[Day].notSelected.SetActive(true);
            dailyReward_Item_Properties[Day].selected.SetActive(true);
            PlayerPrefs.SetString("Days" + PlayerPrefs.GetInt(Days).ToString(), "selected");
            PlayerPrefs.SetInt(Days, Day + 1);
            FlyingDiamond cashTemp = DailyRewardManager.Instance.flyingDiamondPrefab;
            int money = PlayerPrefs.GetInt("Cash");
            money += Int32.Parse(dailyReward_Item_Properties[Day].text_Value.text.ToString());
            cashTemp.MoveToTarget(target.transform, money, delegate {


              
             


            });






            //PlayerPrefs.SetInt("Cash", money);
        }



    }
    public void RewardWithAd()
    {
        GD.Controller.Instance.RewardedVideo(result => {

            if (result)
            {
                buttonWithAd.GetComponent<Button>().interactable = false;



                if (PlayerPrefs.GetInt(timerStoredValue) == 0)
                {

                    PlayerPrefs.SetInt(timerStoredValue, timeRestartAfterSeconds);

                }
                else if (PlayerPrefs.GetInt(timerDailyRewardAdButtonClicked) == 0)
                {
                    PlayerPrefs.SetInt(timerDailyRewardAdButtonClicked, 1);
                }

                GetdailyReward();


                int Day = PlayerPrefs.GetInt(Days);
                string value = PlayerPrefs.GetString("Days" + PlayerPrefs.GetInt(Days).ToString());

                if (value == "")
                {
                    dailyReward_Item_Properties[Day].notSelected.SetActive(true);
                    dailyReward_Item_Properties[Day].selected.SetActive(true);
                    PlayerPrefs.SetString("Days" + PlayerPrefs.GetInt(Days).ToString(), "selected");
                    PlayerPrefs.SetInt(Days, Day + 1);


                    FlyingDiamond cashTemp = DailyRewardManager.Instance.flyingDiamondPrefab;
                    int money = PlayerPrefs.GetInt("Cash");
                    money += Int32.Parse(dailyReward_Item_Properties[Day].text_Value.text.ToString());
                    cashTemp.MoveToTarget(target.transform, money, delegate {

                   


                    });






                    //PlayerPrefs.SetInt("Cash", money);
                }


            }


        });





    }

    public void RewardCollection()
    {





        if (PlayerPrefs.GetInt(timerStoredValue) == 0)
        {

            PlayerPrefs.SetInt(timerStoredValue, timeRestartAfterSeconds);
  
        }
        else if (PlayerPrefs.GetInt(timerDailyRewardAdButtonClicked) == 0)
        {
            PlayerPrefs.SetInt(timerDailyRewardAdButtonClicked, 1);
        }
        GetdailyReward();


        int Day = PlayerPrefs.GetInt(Days);
        string value = PlayerPrefs.GetString("Days" + PlayerPrefs.GetInt(Days).ToString());
       
        if (value == "")
        {
            dailyReward_Item_Properties[ Day].notSelected.SetActive(true);
            dailyReward_Item_Properties[ Day].selected.SetActive(true);
            PlayerPrefs.SetString("Days" + PlayerPrefs.GetInt(Days).ToString(), "selected");
            PlayerPrefs.SetInt(Days, Day + 1);
            int money = PlayerPrefs.GetInt("Cash");
            money +=Int32.Parse( dailyReward_Item_Properties[Day].text_Value.text.ToString());
            PlayerPrefs.SetInt("Cash", money);
        }
       

    }

}
[Serializable]
public class DailyReward_Item_Property
{
   
    public GameObject selected;
    public GameObject notSelected;
    public Text text_Value;
    public int value;

    public  DailyReward_Item_Property(GameObject selected = null, GameObject notSelected = null, Text text_Value = null, int value=0)
    {
        this.selected = selected;
        this.notSelected = notSelected;
        this.text_Value = text_Value;
        this.value = value;
        this.text_Value.text = this.value.ToString();
    }



}

