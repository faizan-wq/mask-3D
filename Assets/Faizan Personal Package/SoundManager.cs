using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public List<Sound> sounds;

    [SerializeField]private AudioSource completeSound;
    [SerializeField] private AudioSource quickSound;
    [SerializeField] private AudioSource BGSound;

    private const string Music = "Music";
    private const string Sound = "Sound";
    private const string Vibrations = "Sound";



    public void PlayCompleteSoundClip(string name, bool check)
    {

        if (!GetPlayerPrefValue(Sound))
            return;

        AudioClip clip = GetAudioClip(name);

        if (clip == null)
            return;

        completeSound.clip = clip;

        if (check)
            completeSound.Play();
        else
            completeSound.Stop();

    }

    public void PlayQuickSoundClip(string name)
    {

        if (!GetPlayerPrefValue(Sound))
            return;

        AudioClip clip = GetAudioClip(name);

        if (clip == null)
            return;

        quickSound.clip = clip;
        quickSound.Play();


    }

    public void PlayVibration(string name, string value)
    {
        if (!GetPlayerPrefValue(Vibrations))
            return;




    }




    private AudioClip GetAudioClip(string name)
    {
        foreach (var item in sounds)
        {
            if (name == item.name)
                return item.clip;
        }
        return null;
    }

    private bool GetPlayerPrefValue(string name)
    {
        if (PlayerPrefs.GetInt(name) == 0)    
            return true;
        
        return false;

    }



}
[Serializable]
public struct Sound
{
    public string name;
    public AudioClip clip;
}