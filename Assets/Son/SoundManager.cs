using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class SoundManager : MonoBehaviour
{
public static SoundManager Instance;
public Sound[] musicSounds, sfxSounds, ambSounds;
public AudioSource musicSource, sfxSource, ambSource;

private void Awake()
{   
    if(Instance == null){
        Instance= this;
        DontDestroyOnLoad(gameObject);
    }
    else{
        Destroy(gameObject);
    }
}

private void Start(){
    PlayMusic("Main_Theme");
}


private void ToggleMusic()
{
    musicSource.mute= !musicSource.mute;
}
public void ToggleSFX(){
    sfxSource.mute= !sfxSource.mute;
}

public void MusicVolume(float volume){
    musicSource.volume = volume;
}

public void SFXVolume(float volume){
    sfxSource.volume= volume;
}

public void AMBVolume(float volume){
    ambSource.volume= volume;
}


public void PlayMusic(string name)
{
    Sound s=Array.Find(musicSounds, x=> x.name == name);

    if (s == null){
        Debug.Log("Le son n'a pas été trouvé");
    }
    else {
        musicSource.clip=s.clip;
        musicSource.Play();
    }
}

public void PlayAMB(string name)
{
    Sound s=Array.Find(ambSounds, x=> x.name == name);

    if (s == null){
        Debug.Log("Le son n'a pas été trouvé");
    }
    else {
        ambSource.clip=s.clip;
        ambSource.Play();
    }
}


public void PlaySFX(string name)
{
    Sound s=Array.Find(sfxSounds, x=> x.name == name);

    if (s == null){
        Debug.Log("Le son n'a pas été trouvé");
    }
    else {
        sfxSource.PlayOneShot(s.clip);
    }

}

}