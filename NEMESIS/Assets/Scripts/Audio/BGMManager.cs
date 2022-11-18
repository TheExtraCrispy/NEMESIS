using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    AudioSource intro;
    AudioSource loopLoud;
    AudioSource loopSoft;
    AudioSource outro;

    public MusicTrack[] tracks;
    public MusicTrack activeTrack;
    public bool PlayOnStart;

    public void Awake()
    {
        intro = gameObject.AddComponent<AudioSource>();
        loopLoud = gameObject.AddComponent<AudioSource>();
        loopSoft = gameObject.AddComponent<AudioSource>();
        outro = gameObject.AddComponent<AudioSource>();

        loopLoud.loop = true;
        loopSoft.loop = true;
        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        if (PlayOnStart)
        {
            LoadTrack(activeTrack);
            StartPlaying();
        }
    }

    public void LoadTrack(MusicTrack track)
    {
        intro.clip = track.intro;
        loopLoud.clip = track.loopLoud;
        loopSoft.clip = track.loopSoft;
        outro.clip = track.outro;
    }

    public void StartPlaying()
    {
        intro.Play();
        double trackLength = (double)intro.clip.samples / intro.clip.frequency;
        loopLoud.PlayScheduled(AudioSettings.dspTime + trackLength);
    }

    public void StopPlaying()
    {
        intro.Stop();
        loopLoud.Stop();
        loopSoft.Stop();
        outro.Stop();
    }
    public void StartOutro()
    {
        StopPlaying();

        outro.Play();
    }
}
