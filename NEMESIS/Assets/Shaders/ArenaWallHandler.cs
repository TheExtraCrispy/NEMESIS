using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaWallHandler : MonoBehaviour
{
    //[SerializeField] Material shaderMat;

    private void Start()
    {
        Renderer renderer = gameObject.GetComponent<Renderer>();
        Material shaderMat = renderer.material;
        renderer.material = shaderMat;
    }
}
