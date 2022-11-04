using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioEventArgs : EventArgs
{
    public Sound sound;
}

public static class AudioEvents
{
    public static event EventHandler<AudioEventArgs> PlayAudio;
}
