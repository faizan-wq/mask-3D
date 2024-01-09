using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomList : MonoBehaviour
{
    public void BackBtn()
    {
        PlayerPrefs.SetString("Scene", "");
        SceneManager.LoadScene(0);
    }
}
