using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Node : ScriptableObject
{
    public enum State
    {
        RUNNING, 
        FAILURE,
        SUCCESS
    }

    [HideInInspector] public State state = State.RUNNING;
    [HideInInspector] public bool started = false;
    [HideInInspector] public string guid;
    [HideInInspector] public Vector2 position;
    [TextArea] public string description;
    [HideInInspector] public Blackboard blackboard;
    [HideInInspector] public Agent agent;

    public State Update()
    {
        if (!started)
        {
            OnStart();
            started = true;
        }

        state = OnUpdate();        
        if(state == State.FAILURE || state == State.SUCCESS)
        {
            OnStop();
            started = false;
        }
        return state;
    }

    public virtual Node Clone()
    {
        return Instantiate(this);
    }

    protected abstract void OnStart();
    protected abstract void OnStop();
    protected abstract State OnUpdate();
}
