using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu()]
public class MusicTrack : ScriptableObject
{
    public AudioClip intro;
    public AudioClip loopLoud;
    public AudioClip loopSoft;
    public AudioClip outro;

    public bool playIntro;
    public bool playOutro;
    public bool loop;
    public bool loudAndSoft;

    [Range(0f, 1f)]
    public float volume = 1;
    [HideInInspector] public AudioSource source;
}
