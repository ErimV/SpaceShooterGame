using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] GameObject _background;
    [SerializeField] GameObject _backgroundCon;
    public float _flowRate;
    float distanceX;
    float minX;
    void Start()
    {
        distanceX = _backgroundCon.transform.position.x - _background.transform.position.x;
        minX = _background.transform.position.x - distanceX;
    }

    void Update()
    {
        _background.transform.position += new Vector3(-_flowRate, 0, 0);
        _backgroundCon.transform.position += new Vector3(-_flowRate, 0, 0);

        if (_background.transform.position.x < minX)
        {
            _background.transform.position = _backgroundCon.transform.position + new Vector3(distanceX, 0, 0);

            GameObject temp = _background;
            _background = _backgroundCon;
            _backgroundCon = temp;
        }
    }
}
