using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManagerHomeLoop : MonoBehaviour
{
    [Header("Animation Controller")]
    public Animator CharacterModel;

    [Header("Strings Controller")]
    public List<string> animationNames = new List<string>();
    void Start()
    {
       // ListAnimationNames();
    }
    void Update()
    {
        CheckStatAnimation();
    }
    void ListAnimationNames()
    {
        RuntimeAnimatorController controller = CharacterModel.runtimeAnimatorController;

        if (controller != null)
        {
            foreach (AnimationClip clip in controller.animationClips)
            {
                string clipName = clip.name;
                animationNames.Add(clipName);
            }
        }
    }
    void CheckStatAnimation()
    {
        AnimatorStateInfo stateInfo = CharacterModel.GetCurrentAnimatorStateInfo(0);

        if (stateInfo.normalizedTime < 1)
        {
           
        }else 
        { 
            //CharacterModel.Play(animationNames[Random.Range(0, animationNames.Count)]);             
        }
    }
}
