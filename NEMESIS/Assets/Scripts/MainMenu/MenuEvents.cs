using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ModeArgs : EventArgs
{
    public string name;

    public ModeArgs(string nme)
    {
        name = nme;
    }
}

public static class MenuEvents
{
    public static event EventHandler<ModeArgs> ModeChosen;
   
    public static void InvokeModeChosen(string name)
    {
        Debug.Log("MODE CHOSEN");
        ModeChosen(null, new ModeArgs(name));
    }
}
