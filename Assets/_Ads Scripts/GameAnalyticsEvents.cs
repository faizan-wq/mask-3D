using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
public class GameAnalyticsEvents : MonoBehaviour
{

    public static GameAnalyticsEvents Instance;
    private int gameLevel;
    private const string name = "";



    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);

        GetLevelNumber();

        DontDestroyOnLoad(gameObject);
    }

    #region LevelProgressionCalls


    public void UpgradeLevelNumber()
    {
        PlayerPrefs.SetInt("Mission", PlayerPrefs.GetInt("Mission")+1);
    }

    public void GetLevelNumber()
    {
        if(PlayerPrefs.GetInt("Mission")==0)
        {
            UpgradeLevelNumber();
        }
        gameLevel = PlayerPrefs.GetInt("Mission");
    }


    public void OnLevelStarted()
    {
        //Game Analytics Call
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "lvl Start", gameLevel);
        //Elephant Call
      
    }
    public void OnLevelComplete()
    {
        //Game Analytics Call
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "lvl Completed", gameLevel);
        UpgradeLevelNumber();
        //Elephant Call

    }

    public void OnLevelFail()
    {
        //Game Analytics Call
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, "lvl Fail", gameLevel);
        //Elephant Call
       
    }
    public void OnLevelRestart()
    {
        //Game Analytics Call
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Restart, "Restart btn", gameLevel);
        //Elephant Call

    }



    #endregion

}
public enum Level_Status
{
    Start,
    Fail,
    Complete,
    NextLevelbtn,
    Restart



}
