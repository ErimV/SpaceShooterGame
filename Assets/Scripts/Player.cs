using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    Animator _animator;
    Rigidbody2D _rigidBody;
    GameManager _gameManager;
    [SerializeField] float _speedMultiplier;
    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _gun;
    [SerializeField] float _bulletCooldown;
    [SerializeField] HealthBar _healthBar;
    float _timer;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidBody = GetComponent<Rigidbody2D>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _healthBar.TotalHealth = 20;
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        bool pressedUp = false;
        bool pressedDown = false;

        if (y < 0)
        {
            pressedDown = true;
        }

        if (y > 0)
        {
            pressedUp = true;
        }

        _animator.SetBool("pressedUp",pressedUp);
        _animator.SetBool("pressedDown",pressedDown);

        _rigidBody.velocity = new Vector2(x * _speedMultiplier, y * _speedMultiplier);
    }

    void Fire()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (_timer >= _bulletCooldown)
            {
                var newBullet = Instantiate(_bullet);
                newBullet.transform.position = _gun.transform.position;
                newBullet.GetComponent<Bullet>().SetDirection(Directions.Right);
                SFXManager.PlayAudio(SFXManager.AudioTypes.Fire);
                _timer = 0;
            }
        }
        _timer += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroid"))
        {
            _healthBar.DecreaseHealth(Damages.AsteroidCollide);
            if (_healthBar.HealthLeft == 0)
            {
                PlayerDestroyed();
            }
        }

        if (collision.CompareTag("SmallAsteroid"))
        {
            _healthBar.DecreaseHealth(Damages.SmallAsteroidCollide);
            if (_healthBar.HealthLeft == 0)
            {
                PlayerDestroyed();
            }
        }

        if (collision.CompareTag("Enemy"))
        {
            _healthBar.DecreaseHealth(Damages.EnemyCollide);
            if (_healthBar.HealthLeft == 0)
            {
                PlayerDestroyed();
            }
        }

        if (collision.CompareTag("Enemy2"))
        {
            _healthBar.DecreaseHealth(Damages.Enemy2Collide);
            if (_healthBar.HealthLeft == 0)
            {
                PlayerDestroyed();
            }
        }

        if (collision.CompareTag("EnemyBullet"))
        {
            _healthBar.DecreaseHealth(Damages.BulletHit);
            if (_healthBar.HealthLeft == 0)
            {
                PlayerDestroyed();
            }
        }
    }

    void PlayerDestroyed()
    {
        Destroy(gameObject);
        VFXManager.CreateExplosion(gameObject.transform.position);
        SFXManager.PlayAudio(SFXManager.AudioTypes.Explosion);
        _gameManager.GameOver();
    }
}