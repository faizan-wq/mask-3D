using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


using Random = UnityEngine.Random;
public class FlyingDiamond : MonoBehaviour
{

    public List<Transform> diamonds;
    
    public Transform startingPosition;
    
    

   public void MoveToTarget(Transform target,int value,Action action=null )
    {
        OnDiamondCollection = null;
        OnDiamondCollection = action;
        gameObject.SetActive(true);
        StartCoroutine(GiveTarget(target,value,action));
    }

    private Action OnDiamondCollection;
    private IEnumerator GiveTarget(Transform target,int value, Action action=null)
    {
        Vector2 randomPos;
        foreach (var item in diamonds)
        {
            item.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
          
            item.gameObject.SetActive (true);
            randomPos = new Vector2(item.position.x,item.position.y)+  new Vector2(Random.RandomRange(-200f, 200f), Random.RandomRange(-200f, 200f));
            ParticleManager.Instance.soundManager.PlayQuickSoundClip("coin");
            item.DOMove(randomPos, 0.125f);
        }

        yield return new WaitForSeconds(1);

        float timeDifference = 0.5f;
        int number=0;
        foreach (var item in diamonds)
        {
           
            item.DOMove(target.position, timeDifference-number*0.02f).OnComplete(()=> { item.gameObject.SetActive(false); ParticleManager.Instance.soundManager.PlayQuickSoundClip("coin"); });
            number++;
        }
        yield return new WaitForSeconds(0.5f);
        PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + value);
        gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
       
            OnDiamondCollection?.Invoke();


    }


}
