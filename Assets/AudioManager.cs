using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;


[RequireComponent(typeof(AudioSource))]

public class AudioManager : MonoBehaviour


{
    public AudioClip[] musicClips;
    private int currentTrack;
    private AudioSource source;

    public Text clipTitleText;
    public Text clipTimeText;

    public int fullLength;
    public int playTime;
    public int seconds;
    public int minutes;


      
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(musicClips[9]);
        //Play Music 


        PlayMusic();

        
    }

    public void PlayMusic()
    {
        if(source.isPlaying)
        {
            return;

        }
        currentTrack--;
        if (currentTrack <0)
        {
            currentTrack = musicClips.Length - 1;

        }
        StartCoroutine("WaitForMusicEnd");

    }

    IEnumerator WaitForMusicEnd()
    {
        while (source.isPlaying)
        {
            yield return null;
            playTime = (int)source.time;
            ShowPlayTime(); 
        }
        NextTitle();


    }

    public void NextTitle()
    {
        source.Stop();
        currentTrack++;
        if(currentTrack > musicClips.Length-1)
        {
            currentTrack = 0;

        }
        source.clip = musicClips[currentTrack];
        source.Play();

        StartCoroutine("WaitForMusicEnd");
        source.PlayOneShot(musicClips[1]);

        ShowCurrentTitle();



    }

    public void PreviousTitle()
    {
        source.Stop();
        currentTrack--;
        if (currentTrack < 0)
        {
            currentTrack = musicClips.Length-1;

        }
        source.clip = musicClips[currentTrack];
        source.Play();

        StartCoroutine("WaitForMusicEnd");

        ShowCurrentTitle(); 

    }

    public void StopMusic()
    {
        StopCoroutine("WaitForMusicEnd");


        source.Stop();




    }

    public void MuteMusic()
    {
        source.mute = !source.mute;

    }


    void ShowCurrentTitle()
    {
        clipTitleText.text = source.clip.name;
        fullLength = (int)source.clip.length;

    }

    void ShowPlayTime()
    {
        seconds = playTime % 60;
        minutes = (playTime / 60) % 60;
        clipTimeText.text = minutes + ':' + seconds.ToString("D2") + "/";
    }
} 
