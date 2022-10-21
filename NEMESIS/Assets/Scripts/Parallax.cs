using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    [SerializeField] Transform[] _backgroundElements;
    private float[] _parallaxScales;
    [SerializeField] float _smoothing = 1;

    private Transform _camera;

    private Vector3 _previousCamPosition;
    // Start is called before the first frame update
    private void Awake()
    {
        _camera = Camera.main.transform;
    }
    void Start()
    {
        _previousCamPosition = _camera.position;
        _parallaxScales = new float[_backgroundElements.Length];

        for (int i = 0; i < _backgroundElements.Length; i++)
        {
            _parallaxScales[i] = _backgroundElements[i].position.z*-1;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for (int i = 0; i < _backgroundElements.Length; i++)
        {
            float parallaxX = (_previousCamPosition.x - _camera.position.x) * _parallaxScales[i];
            float parallaxY = (_previousCamPosition.y - _camera.position.y) * _parallaxScales[i];

            float elementTargetPosX = _backgroundElements[i].position.x + parallaxX;
            float elementTargetPosY = _backgroundElements[i].position.y + parallaxY;

            Vector3 elementTargetPos = new Vector3(elementTargetPosX, elementTargetPosY, _backgroundElements[i].position.z);

            _backgroundElements[i].position = Vector3.Lerp(_backgroundElements[i].position, elementTargetPos, _smoothing * Time.deltaTime);
        }
        _previousCamPosition = _camera.position;
    }


}
