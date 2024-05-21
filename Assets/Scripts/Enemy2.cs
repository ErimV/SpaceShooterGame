using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2 : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    GameManager _gameManager;

    [SerializeField] GameObject _bullet;
    [SerializeField] GameObject _rightGun;
    [SerializeField] GameObject _leftGun;
    [SerializeField] float _bulletCooldown;
    [SerializeField] float _decisionCooldown;
    float _timer;

    [SerializeField] float _speedModifierX;
    [SerializeField] float _speedModifierY;
    [SerializeField] float yDuration;
    [SerializeField] float delayBeforeReturnToX;
    float yMovementStartTime;
    bool isMovingInYDirection = false;
    Vector2 speed;

    [SerializeField] HealthBar _healthBar;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
        yMovementStartTime = Time.time;
        speed = new Vector2(-_speedModifierX, 0);
        _rigidBody.velocity = speed;
        _healthBar.TotalHealth = 6;
    }

    void Update()
    {
        Move();
        Fire();
    }

    private void Move()
    {
        if (!isMovingInYDirection && Time.time - yMovementStartTime >= yDuration)
        {
            if (Random.Range(0, 2) == 0)
            {
                speed.x = 0;
                speed.y = _speedModifierY;
                _rigidBody.velocity = speed;
            }
            else
            {
                speed.x = 0;
                speed.y = -_speedModifierY;
                _rigidBody.velocity = speed;
            }
            isMovingInYDirection = true;
        }
        else if (isMovingInYDirection && Time.time - yMovementStartTime >= yDuration + delayBeforeReturnToX)
        {
            speed.x = -_speedModifierX;
            speed.y = 0;
            _rigidBody.velocity = speed;
            isMovingInYDirection = false;
            yMovementStartTime = Time.time;
        }

    }

    void Fire()
    {
        if (_timer >= _bulletCooldown)
        {
            var newBullet1 = Instantiate(_bullet);
            var newBullet2 = Instantiate(_bullet);
            newBullet1.transform.position = _rightGun.transform.position;
            newBullet2.transform.position = _leftGun.transform.position;
            newBullet1.GetComponent<Bullet>().SetDirection(Directions.Left);
            newBullet2.GetComponent<Bullet>().SetDirection(Directions.Left);
            _timer = 0;
        }
        _timer += Time.deltaTime;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftBorder"))
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            _healthBar.DecreaseHealth(Damages.BulletHit);

            if (_healthBar.HealthLeft == 0)
            {
                EnemyDestroyed();
            }
        }

        if (collision.CompareTag("Player"))
        {
            EnemyDestroyed();
        }

        if (collision.CompareTag("TopBorder"))
        {
            speed.y = -_speedModifierY;
            _rigidBody.velocity = speed;
        }

        if (collision.CompareTag("BottomBorder"))
        {
            speed.y = _speedModifierY;
            _rigidBody.velocity = speed;
        }
    }
    void EnemyDestroyed()
    {
        Destroy(gameObject);
        VFXManager.CreateExplosion(gameObject.transform.position);
        SFXManager.PlayAudio(SFXManager.AudioTypes.Explosion);
        _gameManager.IncreaseScore(Points.Enemy2Destroyed);
    }
}