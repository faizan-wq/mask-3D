using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Header("Audio Clips")]
    public List<Sound> sounds;
    
    
    [Header("Audio Sources")]
    [SerializeField]private AudioSource completeSound;
    [SerializeField] private AudioSource quickSound;
    [SerializeField] private AudioSource BGSound;

    [Header("Buttons")]
    public Setting_Buttons musicBtn;
    public Setting_Buttons soundBtn;
    public Setting_Buttons vibrationBtn;

    [Header("Old Audio Sources")]
    public List<AudioSource> subGameSounds;

    private VibrationTest vibrationTest;

    private const string Music = "Music";
    private const string Sound = "Sound";
    private const string Vibrations = "Vibration";




    private void Start()
    {
        Vibration.Init();
        vibrationTest = GetComponent<VibrationTest>();
        MusicEffectResult();
        SoundEffectResult();
        VibrationEffectResult();
    }

    public void MusicEffect()
    {

        SetPlayerPrefValue(Music);

        MusicEffectResult();

    }
    private void MusicEffectResult()
    {

     

        if (!GetPlayerPrefValue(Music))
        {
            musicBtn.Button_Off.SetActive(true);
            musicBtn.Button_On.SetActive(false);
            BGSound.Stop();

        }
        else
        {
            musicBtn.Button_Off.SetActive(false);
            musicBtn.Button_On.SetActive(true);
            BGSound.Play();
        }

    }


    public void SoundEffect()
    {
        SetPlayerPrefValue(Sound);
        SoundEffectResult();

    }
    private void SoundEffectResult()
    {
       
        if (!GetPlayerPrefValue(Sound))
        {
            soundBtn.Button_Off.SetActive(true);
            soundBtn.Button_On.SetActive(false);
            foreach (var item in subGameSounds)
            {
                item.volume = 0;
            }
            completeSound.Stop();
            quickSound.Stop();
        }
        else
        {

            foreach (var item in subGameSounds)
            {
                item.volume = 1;
            }

            soundBtn.Button_Off.SetActive(false);
            soundBtn.Button_On.SetActive(true);
        }

    }


    public void VibrationEffect()
    {
        SetPlayerPrefValue(Vibrations);

        VibrationEffectResult();
    }


    private void VibrationEffectResult()
    {
       

        if (!GetPlayerPrefValue(Vibrations))
        {
            vibrationBtn.Button_Off.SetActive(true);
            vibrationBtn.Button_On.SetActive(false);
        }
        else
        {
            vibrationBtn.Button_Off.SetActive(false);
            vibrationBtn.Button_On.SetActive(true);
        }
    }


    public void PlayCompleteSoundClip(string name, bool check)
    {

        if (!GetPlayerPrefValue(Sound))
        {
           
            return;
        }

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
        {
            return;

        }

        AudioClip clip = GetAudioClip(name);

        if (clip == null)
            return;

        quickSound.clip = clip;
        quickSound.Play();


    }

    public void PlayVibration(string value)
    {
        if (GetPlayerPrefValue(Vibrations))
        {
            vibrationTest.inputValue = value;
            vibrationTest.TapVibratePattern();
        }
       


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

    public void SetPlayerPrefValue(string name)
    {
        if (PlayerPrefs.GetInt(name) == 0)

            PlayerPrefs.SetInt(name,1);
        else
            PlayerPrefs.SetInt(name, 0);
        
    }



}
[Serializable]
public struct Sound
{
    public string name;
    public AudioClip clip;
}

[Serializable]
public struct Setting_Buttons
{

    public GameObject Button_On;
    public GameObject Button_Off;



}

