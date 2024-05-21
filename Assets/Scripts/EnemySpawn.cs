using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemySpawn : Spawner
{
    [SerializeField] GameObject _enemy;
    [SerializeField] Transform _topSpawnPoint;
    [SerializeField] Transform _bottomSpawnPoint;
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
        Instantiate(_enemy).transform.position = new Vector3(x, y, 0);
    }
}
