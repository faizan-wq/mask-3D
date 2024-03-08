using System.Collections;
using UnityEngine;
using UnityEngine.UI;
public class loadingTextChange : MonoBehaviour
{

    public int number;
    public Text anim_Text;
    private bool runTextAnim = true;
    private WaitForSeconds wait = new WaitForSeconds(0.25f);
    public Image loadingSlider;

    private void OnEnable()
    {
        number = 0;
    }


  

    // Update is called once per frame
    void Update()
    {
        if (!runTextAnim)
            return;

       if(loadingSlider.fillAmount <=0.29f)
        {
            number = 0;
        }
       else if(loadingSlider.fillAmount > 0.33f && loadingSlider.fillAmount <= 0.66f)
        {
            number = 1;
        }
       else
        {
            number = 2;
        }


        switch (number)
        {
            case 0:
                StartCoroutine(ChangeString("Loading Texture "));
                break;
            case 1:
                StartCoroutine(ChangeString("Loading Inredients "));
               
                break;
            case 2:
                StartCoroutine(ChangeString("Loading Images "));
                break;

                runTextAnim = false; 
        }
    }

    private IEnumerator ChangeString(string text)
    {
        runTextAnim = false;
        anim_Text.text = text;
        yield return wait;
        anim_Text.text += ".";
        yield return wait;
        anim_Text.text += ".";
        yield return wait;
        anim_Text.text += ".";
        yield return wait;
        runTextAnim = true;
    }

}
