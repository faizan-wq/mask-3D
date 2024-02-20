using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAdsLoadPannel : MonoBehaviour {


    private void OnEnable()
    {
        Destroy(this.gameObject, 0.3f);
    }

}
