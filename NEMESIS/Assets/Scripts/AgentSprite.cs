using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentSprite : MonoBehaviour
{
    [SerializeField] GameObject agent;
    private void Start()
    {
        //agent = transform.parent.gameObject;
    }
    private void Update()
    {
        float desiredZRotation = agent.transform.eulerAngles.y;
        transform.rotation = Quaternion.Euler(0, 0, desiredZRotation);
        transform.position = agent.transform.position;
    }
}
