using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] float _rotation;
    [SerializeField] HealthBar _healthBar;
    [SerializeField] Transform _sprite;
    [SerializeField] GameObject _smallAsteroid;
    [SerializeField] float _smallAsteroidSpeed;
    GameManager _gameManager;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _healthBar.TotalHealth = 4;
    }

    void Update()
    {
        _sprite.Rotate(0, 0, _rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            _healthBar.DecreaseHealth(Damages.BulletHit);

            if (_healthBar.HealthLeft == 0)
            {
                AsteroidDestroyed();
            }
        }

        if (collision.CompareTag("Player"))
        {
            AsteroidDestroyed();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftBorder"))
        {
            Destroy(gameObject);
        }
    }

    void AsteroidDestroyed()
    {
        Destroy(gameObject);
        VFXManager.CreateExplosion(gameObject.transform.position);
        SFXManager.PlayAudio(SFXManager.AudioTypes.Explosion);
        _gameManager.IncreaseScore(Points.AsteroidDestroyed);
        AsteroidSplit();
    }

    void AsteroidSplit()
    {
        var smallAsteroid1 = Instantiate(_smallAsteroid);
        var smallAsteroid2 = Instantiate(_smallAsteroid);

        smallAsteroid1.transform.position = transform.position;
        smallAsteroid2.transform.position = transform.position;

        smallAsteroid1.GetComponent<Rigidbody2D>().velocity = new Vector2(-_smallAsteroidSpeed, _smallAsteroidSpeed / 2);
        smallAsteroid2.GetComponent<Rigidbody2D>().velocity = new Vector2(-_smallAsteroidSpeed, -_smallAsteroidSpeed / 2);
    }
}