using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaIntro : MonoBehaviour
{
    [SerializeField] Material shader;
    private float timeElapsed;

    private void Start()
    {
        shader = gameObject.GetComponent<Renderer>().material;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        shader.SetFloat("_timeElapsed", timeElapsed);
    }
}
