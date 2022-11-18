using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuScrollimg : MonoBehaviour
{
    [SerializeField] float scrollSpeed;
    [SerializeField] float delay;
    private float startTime;
    private void Start()
    {
        startTime = Time.time + delay;
    }
    private void Update()
    {
        if(Time.time > startTime)
        {
            transform.Translate(Vector2.up * scrollSpeed * Time.deltaTime);
        }
    }
}
