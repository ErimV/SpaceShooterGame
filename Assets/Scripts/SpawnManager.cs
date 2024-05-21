using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField] List<Spawner> _spawners;
    [SerializeField] float _spawnCooldown;
    float _timer = 0;
    int _random;

    void Start()
    {
        var list = FindObjectsByType<Spawner>(FindObjectsSortMode.InstanceID);

        foreach (var obj in list)
        {
            _spawners.Add(obj);
        }
    }

    void Update()
    {
        if (_timer >= _spawnCooldown)
        {
            _random = Random.Range(0, 100);

            if (_random < 50) _spawners[0].Spawn();    //%50 Chance Enemy

            else if (_random >= 50 && _random < 80) _spawners[1].Spawn(); //%30 Chance Enemy2

            else if (_random >= 80 && _random < 100) _spawners[2].Spawn(); //%20 Chance Asteroid

            _timer = 0;
        }
        _timer += Time.deltaTime;
    }
}