using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomButton : MonoBehaviour
{
    public ButtonType type;
    public int cashRequiredToUnlock;
    public GameObject itemToUnlock;
    private Button button;
    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        CheckButtonStatus();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CheckButtonStatus()
    {
        GameObject obj = gameObject;
      if (PlayerPrefs.GetInt(type.ToString() + obj.name)==1)
        {
            obj.SetActive(false);
            itemToUnlock.transform.localScale = Vector3.one;
            itemToUnlock.SetActive(true);
            return;
        }
        
        button.onClick.AddListener(()=> {

            PlayerPrefs.SetInt(type.ToString() + obj.name, 1);
            itemToUnlock.SetActive(true);
            obj.SetActive(false);

        });








    }




}
public enum ButtonType
{
    Rewarded,
    Cash
}
