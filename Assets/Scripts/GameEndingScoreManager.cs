using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameEndingScoreManager : MonoBehaviour
{
    public GameObject CoinPrefab;
    public Transform CoinParent;
    
    public void PlayCoins()
    {
        GameObject coin=(Instantiate(CoinPrefab, new Vector3(0, 0, 0), Quaternion.identity));
        coin.transform.SetParent(CoinParent.transform);
        coin.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = "+20";
        PlayerPrefs.SetInt("Day", PlayerPrefs.GetInt("Day")+1);
    }



}
