using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
public class FPS_Target : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 90;
        DontDestroyOnLoad(this.gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
