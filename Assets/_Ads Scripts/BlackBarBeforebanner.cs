using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBarBeforebanner : MonoBehaviour
{
    public static BlackBarBeforebanner Instance;
    private void Awake()
    {
        if(Instance==null)
        {
            Instance = this;
        }
        DontDestroyOnLoad(this);
        HideBlackBar();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public static void ShowBlackBar()
    {
        Instance?.transform.GetChild(0).gameObject.SetActive(true);
        Instance?.gameObject.SetActive(true);
    }
    public static void HideBlackBar()
    {
        Instance?.gameObject.SetActive(false);
    }
}
