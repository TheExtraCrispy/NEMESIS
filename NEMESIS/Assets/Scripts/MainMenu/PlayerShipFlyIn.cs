using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShipFlyIn : MonoBehaviour
{
    [SerializeField] Vector2 desiredPosition;
    [SerializeField] float smoothingFactor;
    [SerializeField] float delay;
    [SerializeField] float shakiness;
    private float startTime;

    private void Awake()
    {
        MenuEvents.ModeChosen += OnModeChosen;
    }

    void OnModeChosen(object sender, ModeArgs args)
    {
        desiredPosition = new Vector2(desiredPosition.x, desiredPosition.y * 12);
    }

    private void Start()
    {
        startTime = Time.time + delay;
    }

    private void Update()
    {
        if(Time.time > startTime)
        {
            transform.localPosition = Vector2.Lerp(transform.localPosition, desiredPosition+ Random.insideUnitCircle * shakiness, Time.deltaTime * smoothingFactor);
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 1);

        }
    }

    private void OnDisable()
    {
        MenuEvents.ModeChosen -= OnModeChosen;
    }
}
