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







}
[Serializable]
public class DailyReward_Item_Property
{
   
    public GameObject selected;
    public GameObject notSelected;
    public Text text_Value;
    public int value;

}

