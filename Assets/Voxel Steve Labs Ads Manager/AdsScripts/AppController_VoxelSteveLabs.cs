using UnityEngine;
using System.Collections;

public class AppController_VoxelSteveLabs : MonoBehaviour {
	public static bool adShowing = false;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public static float getMWidth(float pWidth )  {
		float w_  = ((pWidth * 100f) / 1280);
		return ((w_ /100.00f) *Screen.width);
	}
	
	public static float getMHeight(float pHeight )  {
		float h_  = ((pHeight * 100f) / 720);
		return ((h_ /100.0f) * Screen.height);
	}
	
	public static float getRWidth(float pWidth ) {
		float w_  = ((pWidth * 100f) / Screen.width);
		return ((w_ /100.0f) * 1280);
		
	}
	
	public static float getRHeight(float pHeight ) {
		float h_  = ((pHeight * 100f) / Screen.height);
		return ((h_ /100.0f) * 720);
	}




	public static void ButtonsOff(GameObject[] offButton , bool trueOrFalse)
	{
		for (int a=0; a<offButton.Length; a++) {
			offButton[a].SetActive(trueOrFalse);		
		}
	}
	
	
	public static void CollidersOff(GameObject[] offCollider , bool trueOrFalse)
	{
		for (int a=0; a<offCollider.Length; a++) {
			offCollider[a].GetComponent<Collider2D>().enabled = trueOrFalse;
		}
	}
}
