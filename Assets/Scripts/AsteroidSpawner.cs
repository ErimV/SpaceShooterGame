using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : Spawner
{
    [SerializeField] GameObject _asteroid;
    [SerializeField] GameObject _player;
    [SerializeField] Transform _topSpawnPoint;
    [SerializeField] Transform _bottomSpawnPoint;
    [SerializeField] float _asteroidSpeedModifier;
    float _minY;
    float _maxY;
    float x;
    float y;
    public override void Spawn()
    {
        _minY = _bottomSpawnPoint.position.y;
        _maxY = _topSpawnPoint.position.y;
        x = _bottomSpawnPoint.position.x;
        y = Random.Range(_minY, _maxY);
        var asteroid = Instantiate(_asteroid);
        asteroid.transform.position = new Vector3(x, y, 0);

        Vector3 speed = _player.transform.position - asteroid.transform.position;
        asteroid.GetComponent<Rigidbody2D>().velocity = speed.normalized * _asteroidSpeedModifier;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
