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
    private const string previousDate="PreviousDate";
    private const string todayDate = "todayDate";

    private const string Days = "DaysReward";
    



    public List<DailyReward_Item_Property> dailyReward_Item_Properties;
    private Transform temp_DailyReward=null;
    private DailyReward_Item_Property temp_Daily = new DailyReward_Item_Property();

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
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreateDailyRewardValue()
    {
        for (int i = 0; i < DailyRewardParent.childCount; i++)
        {
            temp_DailyReward = DailyRewardParent.GetChild(i);
            
            temp_Daily.notSelected = temp_DailyReward.GetChild(0).gameObject;
            temp_Daily.text_Value = temp_DailyReward.GetChild(3).GetComponent<Text>();
            temp_Daily.selected = temp_DailyReward.GetChild(4).gameObject;
            temp_Daily.text_Value.text = dailyRewardValues[i].ToString();
            dailyReward_Item_Properties.Add(temp_Daily);
        }

    }
    private void GetdailyReward()
    {
        if (PlayerPrefs.GetString(previousDate) == "")
        {
            DateTime day = DateTime.Now;
            PlayerPrefs.SetString(previousDate, day.ToString());
            PlayerPrefs.SetString(todayDate, day.ToString());
            PlayerPrefs.SetInt(Days, 0);
        }
        else
        {
            var today = DateTime.Now;
            var previousDay = DateTime.Parse(PlayerPrefs.GetString(previousDate).ToString());
            var difference = today - previousDay;

            if (difference.Days > 0)
            {
                PlayerPrefs.SetString(previousDate,today.ToString());
                PlayerPrefs.SetInt(Days, PlayerPrefs.GetInt(Days )+1);
            }

        }
        



    }
    private void EnableDailyRewardAd()
    {
        int Day = PlayerPrefs.GetInt(Days);
      

        int setday = dailyReward_Item_Properties.Count - 1;

        string value = PlayerPrefs.GetString("Days" + PlayerPrefs.GetInt(Days).ToString());


        for (int i = 0; i < Day; i++)
        {
            dailyReward_Item_Properties[setday-i].notSelected.SetActive(true);
            dailyReward_Item_Properties[setday - i].selected.SetActive(true);


        }


        if(value=="")
        {
            dailyReward_Item_Properties[setday - Day].notSelected.SetActive(true);
            Debug.Log("setday - Day" + setday.ToString() + Day.ToString());
        }
        else if(value== "selected")
        {
            dailyReward_Item_Properties[setday - Day].notSelected.SetActive(true);
            dailyReward_Item_Properties[setday - Day].selected.SetActive(true);
           
          
        }




    }
    public void RewardCollection()
    {
        int Day = PlayerPrefs.GetInt(Days);
        string value = PlayerPrefs.GetString("Days" + PlayerPrefs.GetInt(Days).ToString());
        int setday = dailyReward_Item_Properties.Count - 1;
        if (value == "")
        {
            dailyReward_Item_Properties[setday - Day].notSelected.SetActive(true);
            dailyReward_Item_Properties[setday - Day].selected.SetActive(true);
            PlayerPrefs.SetString("Days" + PlayerPrefs.GetInt(Days).ToString(), "selected");
            int money = PlayerPrefs.GetInt("Cash");
            money +=Int32.Parse( dailyReward_Item_Properties[setday - Day].text_Value.text.ToString());
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

}

