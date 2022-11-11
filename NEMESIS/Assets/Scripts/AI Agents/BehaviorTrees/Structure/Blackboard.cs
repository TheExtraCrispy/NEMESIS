using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Blackboard
{
    public GameObject targetObject = null;
    public Transform targetTransform = null;
    public Vector2 desiredPosition;
    public Vector2 desiredVelocity;
}