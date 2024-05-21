using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    int _totalHealth;
    int _healthLeft;
    float _scale;
    [SerializeField] GameObject _healthBarGreen;
    [SerializeField] float _barFadeDuration;
    float _timer = 0;

    public int TotalHealth 
    {  
        get { return _totalHealth; } 
        set { _totalHealth = value;
              _healthLeft = value; }
    }

    public int HealthLeft
    {
        get { return _healthLeft; }
    }
    void Start()
    {
        gameObject.SetActive(false);
    }

    void Update()
    {
        BarFade();
    }

    public void DecreaseHealth(Damages damage)
    {
        gameObject.SetActive(true);
        _healthLeft -= (int)damage;
        if (_healthLeft < 0) _healthLeft = 0;
        _scale = (float)_healthLeft / (float)_totalHealth;

        var scaleVector = _healthBarGreen.transform.localScale;
        scaleVector.x = _scale;
        _healthBarGreen.transform.localScale = scaleVector;
    }

    void BarFade()
    {
        if (_timer >= _barFadeDuration)
        {
            gameObject.SetActive(false);
            _timer = 0;
        }
        if (gameObject.activeInHierarchy) _timer += Time.deltaTime;
    }
}
