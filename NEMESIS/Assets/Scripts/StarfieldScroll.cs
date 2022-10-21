using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarfieldScroll : MonoBehaviour
{
    [SerializeField] float _parallaxFactor;
    private MeshRenderer _mesh;
    private Material _mat;
    private void Start()
    {
        _mesh = gameObject.GetComponent<MeshRenderer>();
        _mat = _mesh.material;
    }
    void Update()
    {
        Vector2 offset = _mat.mainTextureOffset;

        offset.x = transform.position.x / _parallaxFactor;
        offset.y = transform.position.y / _parallaxFactor;

        _mat.mainTextureOffset = offset;
    }

}
