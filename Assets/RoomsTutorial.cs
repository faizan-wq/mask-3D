using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsTutorial : MonoBehaviour
{
    private const string completeState = "Complete";
    private const string unCompleteState = "UnComplete";
    private const string RoomsTutorialCheck = "RoomsTutorialCheck";


    private void OnEnable()
    {
        if (PlayerPrefs.GetString(RoomsTutorialCheck) == completeState || PlayerPrefs.GetInt("Days") < 4)
        {
            gameObject.SetActive(false);
        }
    }
    public void TutorialComplete()
    {
        PlayerPrefs.SetString(RoomsTutorialCheck, completeState);
    }

}
