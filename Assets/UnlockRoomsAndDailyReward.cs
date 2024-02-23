using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UnlockRoomsAndDailyReward : MonoBehaviour
{
    public Button room;
    public Button dailyReward;
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerPrefs.GetInt("Days")>5)
        {
            room.interactable = true;
            room.transform.GetChild(0).gameObject.SetActive(false);
            dailyReward.interactable = true;
            dailyReward.transform.GetChild(0).gameObject.SetActive(false);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
