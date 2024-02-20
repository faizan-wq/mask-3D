using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GamePlayTest_VoxelSteveLabs : MonoBehaviour {
    public GameObject pasuePannel;
    public GameObject gameOverPannel;
    public GameObject gameSuccessPannel;
    public GameObject gameOn;
    public GameObject lodingPannel;
	// Use this for initialization
	void Start () { 
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void btnPause() {
        pasuePannel.SetActive(true);
        gameOn.SetActive(false);


    }
    public void btnGameOver()
    {
        gameOverPannel.SetActive(true);
        gameOn.SetActive(false);

    }
    public void btnGameSuccess()
    {
        gameOverPannel.SetActive(true);
        gameOn.SetActive(false);

    }
    public void btnBack()
    {
        gameOverPannel.SetActive(false);
        gameSuccessPannel.SetActive(false);
        pasuePannel.SetActive(false);
        gameOn.SetActive(true);

    }
    public void btnBackMainMenu()
    {
        gameOverPannel.SetActive(false);
        gameSuccessPannel.SetActive(false);
        pasuePannel.SetActive(false);
        SceneManager.LoadScene(1);



    }
}
