using System.Collections.Generic;
using UnityEngine;
using System;
using GameAnalyticsSDK;
using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using System.Threading.Tasks;
using Firebase.Crashlytics;

namespace GD
{
    #region Custom

    [System.Serializable]
    public class MyTransform
    {
        public Vector3 Positon;
        public Vector3 Rotation;
        public Vector3 Scale;
    }

    #endregion


    public class Controller : MonoBehaviour
    {
        public static Controller Instance;
        public static bool allowFirebaseAds = false;
        public static bool stopAds = false;
        
        // Use this for initialization
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this.gameObject);
                OnFireBase();
                InitializeGameAnalytics();
            }
            else
            {
                Destroy(this.gameObject);
            }

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
        }


        #region Ads

        private Action<bool> customActionVideo;


        public void ActionVideo(bool value)
        {
            if (customActionVideo != null)
                customActionVideo.Invoke(value);

            try
            {
                VideoEvent(value ? "Completed" : "Failed");
            }
            catch (Exception e)
            {
                Debug.Log(e.Message);
            }
        }

        public bool isAdmob = false;

        public void RewardedVideo(Action<bool> result)
        {
            customActionVideo = null;
            customActionVideo = result;
           
            if (isAdmob)
            {
                isAdmob = false;
              
            }

            Debug.Log("RewardedVideo");
          
            LoadingAdScreen.instance.ShowLoadingAdScreen(delegate { AdMob_GF.ShowRewardedAdmobOrInterstitial(); });
            VideoEvent("Click");
        }

        public void RewardedAdOrInterstetial(Action<bool> result)
        {
            //AppOpenAdManager.isInterstialAdPresent = true;
            customActionVideo = null;
            customActionVideo = result;
            LoadingAdScreen.instance.ShowLoadingAdScreen(delegate { AdMob_GF.ShowRewardedAdmobOrInterstitial(); });
            Debug.Log("RewardedAdOrInterstetial");
            //ActionResult(true); //Test
            VideoEvent("Click");
        }

      
        public bool IsRemoveAds
        {
            get { return PlayerPrefs.GetInt("RemoveAds", 0) == 1; }
            set { PlayerPrefs.SetInt("RemoveAds", value ? 1 : 0); }
        }

        #endregion

        #region Events

        /// <summary>
        /// 1 =Start
        /// 2 =Win
        /// 3 =Lose
        /// 4 =Tie
        /// </summary>
        /// <param name="progressionID"></param>
        public void CustomEvent(int progressionID)
        {
            string msg = "Map_";
            //print(msg);

            GameAnalytics.NewProgressionEvent((GAProgressionStatus)progressionID, msg);

            if (firebaseInitialized)
            {
                if (progressionID == 1)
                    FirebaseAnalytics.LogEvent(msg + "Start");
                if (progressionID == 2)
                    FirebaseAnalytics.LogEvent(msg + "Win");
                if (progressionID == 3)
                    FirebaseAnalytics.LogEvent(msg + "Lose");
                if (progressionID == 4)
                    FirebaseAnalytics.LogEvent(msg + "Tie");
            }
        }

        public void DesignEvent(string message)
        {
            GameAnalytics.NewDesignEvent("Screen_" + message);

            // Dictionary<string, object> param = new Dictionary<string, object>();
            // param.Add("Screen", message);
            // AppMetrica.Instance.ReportEvent("GameFlow", param);
            if (firebaseInitialized)
            {
                FirebaseAnalytics.LogEvent("Screen_" + message);

                print("Event :" + "Screen_" + message);
            }
        }

        public void LevelEvents(int Id)
        {
            // Dictionary<string, object> param = new Dictionary<string, object>();
            // param.Add("Screen", message);
            // AppMetrica.Instance.ReportEvent("GameFlow", param);
            if (firebaseInitialized)
            {
                string msg = "";
                if (Id == 1) //levelStart
                    msg = "Game_Level_Start";
                else if (Id == 2) //levelFail
                    msg = "Game_Level_Fail";
                else if (Id == 3) //levelWin
                    msg = "Game_Level_Win";
                else if (Id == 4) //levelTie
                    msg = "Game_Level_Tie";
                else if (Id == 5) //levelRetry
                    msg = "Retry";
                FirebaseAnalytics.LogEvent(msg);
                GameAnalytics.NewDesignEvent(msg);
                print("Event :" + msg);
            }
        }

        public void VideoEvent(string message)
        {
            GameAnalytics.NewDesignEvent("Video_" + message);

            // Dictionary<string, object> param = new Dictionary<string, object>();
            // param.Add("Video", message);
            // AppMetrica.Instance.ReportEvent("GameFlow", param);
            if (firebaseInitialized)
            {
                FirebaseAnalytics.LogEvent("Video_" + message);

                print("Event :" + "Video_" + message);
            }
        }

        public void IAPEvent(string message)
        {
            GameAnalytics.NewDesignEvent("IAP_" + message);

            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("IAP", message);
            //AppMetrica.Instance.ReportEvent("GameFlow", param);
            if (firebaseInitialized)
            {
                FirebaseAnalytics.LogEvent("IAP_" + message);

                print("Event :" + "IAP_" + message);
            }
        }

        public void AdsEvent(int action, int adType, string adsdk, string placement)
        {
            GameAnalytics.NewAdEvent((GAAdAction)action, (GAAdType)adType, adsdk, placement);
        }

        #endregion

        #region GameAnanlytics

        void InitializeGameAnalytics()
        {
            GameAnalytics.Initialize();
        }

        #endregion

        #region Firebase

        DependencyStatus dependencyStatus = DependencyStatus.UnavailableOther;
        public bool firebaseInitialized = true;


        void OnFireBase()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    InitializeFirebase();
                }
                else
                {
                    Debug.LogError(
                        "Could not resolve all Firebase dependencies: " + dependencyStatus);
                }
            });
        }

        public void GetRemoteData()
        {

#if UNITY_IOS
        GlobalConstant.ActionsAdsTimeLimit = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("CrimeAppOpenCheckIOS").LongValue;
        GlobalConstant.IsRewardedAd = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("CrimeRewardedAdCheckIOS").BooleanValue;
        GlobalConstant.IsInterstitialAd = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("CrimeInterstitialAdCheckIOS").BooleanValue;
        GlobalConstant.IsBannerAd = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("CrimeBigBannerAdIOS").BooleanValue;
        GlobalConstant.IsAdaptiveBannerAd = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("CrimeAdaptiveBannerAdCheckIOS").BooleanValue;
        GlobalConstant.IsAppOpenAd = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("CrimeAppOpenCheckIOS").BooleanValue;
#elif UNITY_ANDROID
           


            GlobalConstant.ActionsAdsTimeLimit = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("InterstetialTimer").LongValue;
            GlobalConstant.IsRewardedAd = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("RewardedAdCheck").BooleanValue;
            GlobalConstant.IsInterstitialAd = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("InterstitialAdCheck").BooleanValue;
            GlobalConstant.IsInterstitialMaxAd = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("InterstitialMaxAdCheck").BooleanValue;
            GlobalConstant.IsInterstitialAdmobAd = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("InterstitialAdmobAdCheck").BooleanValue;
            GlobalConstant.IsBannerAd = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("BannerAd").BooleanValue;
            GlobalConstant.IsBigBannerFreeGoldAd = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("IsBigBannerFreeGoldAd").BooleanValue;
            GlobalConstant.IsMaxBannerAd = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("MaxBannerAd").BooleanValue;
            GlobalConstant.IsAdaptiveBannerAd = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("AdmobBannerAd").BooleanValue;
            GlobalConstant.isAppOpen = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.GetValue("AppOpenCheck").BooleanValue;

#endif
            Debug.Log(GlobalConstant.ActionsAdsTimeLimit + "InterstetialTimer");
            Debug.Log(GlobalConstant.IsRewardedAd + "RewardedAdCheck");
            Debug.Log(GlobalConstant.IsInterstitialAd + "InterstitialAdCheck");
            Debug.Log(GlobalConstant.IsInterstitialMaxAd + "InterstitialMaxAdCheck");
            Debug.Log(GlobalConstant.IsInterstitialAdmobAd + "InterstitialAdmobAdCheck");
            Debug.Log(GlobalConstant.IsBannerAd + "BannerAd");
            Debug.Log(GlobalConstant.IsBigBannerFreeGoldAd + "IsBigBannerFreeGoldAd");
            Debug.Log(GlobalConstant.IsMaxBannerAd + "MaxBannerAd");
            Debug.Log(GlobalConstant.IsAdaptiveBannerAd + "AdmobBannerAd");
            Debug.Log(GlobalConstant.isAppOpen + "AppOpenCheck");
        }

        void InitializeFirebase()
        {
            Debug.Log("Enabling data collection.");
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            Crashlytics.IsCrashlyticsCollectionEnabled = true;

            Debug.Log("Set user properties.");
            // Set the user's sign up method.
            FirebaseAnalytics.SetUserProperty(
                FirebaseAnalytics.UserPropertySignUpMethod,
                "Google");

            // Set the user ID.
            //  FirebaseAnalytics.SetUserId("uber_user_510");
            // Set default session duration values.
            //  FirebaseAnalytics.SetSessionTimeoutDuration(new TimeSpan(0, 30, 0));
            firebaseInitialized = true;
            FirebaseApp app = FirebaseApp.DefaultInstance;
            System.Collections.Generic.Dictionary<string, object> defaults =
                new System.Collections.Generic.Dictionary<string, object>();

            // These are the values that are used if we haven't fetched data from the
            // server
            // yet, or if we ask for values that the server doesn't have:

          

#if UNITY_IOS
            defaults.Add("InterstetialTimer", 40);
            defaults.Add("RewardedAdCheck", true);
            defaults.Add("InterstitialAdCheck", true);
            defaults.Add("InterstitialMaxAdCheck", true);
            defaults.Add("InterstitialAdmobAdCheck", true);
            defaults.Add("BannerAd", true);
            defaults.Add("IsBigBannerFreeGoldAd", true);
            defaults.Add("MaxBannerAd", true);
            defaults.Add("AdmobBannerAd", true);
            defaults.Add("AppOpenCheck", true);

#elif UNITY_ANDROID
            defaults.Add("InterstetialTimer", 40);
            defaults.Add("RewardedAdCheck", true);
            defaults.Add("InterstitialAdCheck", true);
            defaults.Add("InterstitialMaxAdCheck", true);
            defaults.Add("InterstitialAdmobAdCheck", true);
            defaults.Add("BannerAd", true);
            defaults.Add("IsBigBannerFreeGoldAd", true);
            defaults.Add("MaxBannerAd", true);
            defaults.Add("AdmobBannerAd", true);
            defaults.Add("AppOpenCheck", true);


#endif

            Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults)
                .ContinueWithOnMainThread(task => { FetchDataAsync(); });

            CheckInterNet();

        }
        void CheckInterNet()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                Event("Internet_Available");
            }
            else
            {
                Event("Internet_Notavailable");
            }
        }
        public void Event(string name)
        {
            if (firebaseInitialized)
            {
                FirebaseAnalytics.LogEvent(name);
            }
        }

        // fire base remote setting 

        public Task FetchDataAsync()
        {
            Debug.Log("Fetching data...");
            System.Threading.Tasks.Task fetchTask =
                Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.FetchAsync(
                    TimeSpan.Zero);
            return fetchTask.ContinueWithOnMainThread(FetchComplete);
        }


        // 
        void FetchComplete(Task fetchTask)
        {
            if (fetchTask.IsCanceled)
            {
                Debug.Log("Fetch canceled.");
            }
            else if (fetchTask.IsFaulted)
            {
                Debug.Log("Fetch encountered an error.");
            }
            else if (fetchTask.IsCompleted)
            {
                Debug.Log("Fetch completed successfully!");
            }

            var info = Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.Info;
            switch (info.LastFetchStatus)
            {
                case Firebase.RemoteConfig.LastFetchStatus.Success:
                    Firebase.RemoteConfig.FirebaseRemoteConfig.DefaultInstance.ActivateAsync()
                        .ContinueWithOnMainThread(task =>
                        {
                            Debug.Log(String.Format("Remote data loaded and ready (last fetch time {0}).",
                                info.FetchTime));
                            GetRemoteData();
                        });

                    break;
                case Firebase.RemoteConfig.LastFetchStatus.Failure:
                    switch (info.LastFetchFailureReason)
                    {
                        case Firebase.RemoteConfig.FetchFailureReason.Error:
                            Debug.Log("Fetch failed for unknown reason");
                            break;
                        case Firebase.RemoteConfig.FetchFailureReason.Throttled:
                            Debug.Log("Fetch throttled until " + info.ThrottledEndTime);
                            break;
                    }

                    break;
                case Firebase.RemoteConfig.LastFetchStatus.Pending:
                    Debug.Log("Latest Fetch call still pending.");
                    break;
            }
        }

        #endregion


        #region Common

        public string GetArrangeAmount(string amount)
        {
            char[] temp = amount.ToCharArray();
            string result = "";
            for (int i = temp.Length - 1; i >= 0; i--)
            {
                if ((i != temp.Length - 1) && (i + 1) % 3 == 0)
                    result += ",";
                result += temp[(temp.Length - 1) - i];
            }


            return result;
        }

        #endregion
    }
}