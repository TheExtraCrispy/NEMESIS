using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FadeOpacity : MonoBehaviour
{
    [SerializeField][Range(0,1)] float startAlpha;
    [SerializeField][Range(0, 1)] float targetAlpha;
    [SerializeField] float smoothingFactor;
    public CanvasRenderer rend;
    public Color color;
    private Color startColor;

    [SerializeField] float delay;
    private float startTime;
    private void Awake()
    {
        MenuEvents.ModeChosen += OnModeChosen;
    }

    public void OnModeChosen(object sender, ModeArgs args)
    {
        if(args.name == gameObject.name)
        {
            targetAlpha = 0;
            smoothingFactor = 0.5f;
        }
        else
        {
            targetAlpha = 0;
            smoothingFactor = 2f;
        }
    }

    private void Start()
    {
        rend = gameObject.GetComponent<CanvasRenderer>();
        color = rend.GetColor();
        startColor = new Color(color.r, color.g, color.b, startAlpha);
        rend.SetColor(startColor);
        startTime = Time.time + delay;

    }
    void Update()
    {
        if(Time.time > startTime)
        {
            float currentAlpha = rend.GetColor().a;
            Color currentColor = rend.GetColor();
            rend.SetColor(new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentAlpha, targetAlpha, Time.deltaTime * smoothingFactor)));
        }
    }
    public void Clicked()
    {
        MenuEvents.InvokeModeChosen(gameObject.name);
    }

    private void OnDisable()
    {
        MenuEvents.ModeChosen -= OnModeChosen;
    }

}

