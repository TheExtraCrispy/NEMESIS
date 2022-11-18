using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu]
public class Sound : ScriptableObject
{
    public AudioClip audioClip;

    [Range(0f, 1f)]
    public float volume = 1;
    [Range(0.1f, 3f)]
    public float pitch = 1;

    [HideInInspector] public AudioSource source;
}
