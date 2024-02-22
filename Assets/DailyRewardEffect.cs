using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class DailyRewardEffect : MonoBehaviour
{
    public List<GameObject> images;
    public float time;
   
    private void OnEnable()
    {
       
        StartCoroutine(ApplyEffect(time));
    }
    private void OnDisable()
    {
        for (int i = 0; i < images.Count; i++)
        {
            images[i].SetActive(false);
        }
    }
        // Update is called once per frame
        void Update()
    {
        
    }

    private IEnumerator ApplyEffect(float wait)
    {

        for (int i = 0; i < images.Count; i++)
        {


            images[i].transform.localScale = Vector3.zero;
            images[i].SetActive(true);
            images[i].transform.DOScale(1, 0.5f).SetEase(Ease.OutExpo);

            yield return new WaitForSeconds(wait);
        }
        yield return  null;
    }




}
