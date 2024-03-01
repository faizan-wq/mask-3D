using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHider : MonoBehaviour
{

    public float wait;
    private Canvas canvas;

    // Start is called before the first frame update
    void Start()
    {
        canvas = GetComponent<Canvas>();
    }

    IEnumerator waitAndHide()
    {
        yield return new WaitForSeconds(wait);
        canvas.enabled = false;

    }
}
