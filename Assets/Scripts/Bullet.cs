using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    [SerializeField] float _speedMultiplier;
    GameManager _gameManager;
    Directions _direction;
    [SerializeField] GameObject _tipOfBullet;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

    }

    public void SetDirection(Directions newDirection)
    {
        _direction = newDirection;
        switch (_direction) 
        {
            case Directions.Right:
                _rigidBody.velocity = new Vector3(_speedMultiplier, 0, 0);
                gameObject.tag = "PlayerBullet";
                break;
            case Directions.Left:
                _rigidBody.velocity = new Vector3(-_speedMultiplier, 0, 0);
                transform.Rotate(0.0f, 0.0f, 180.0f);
                gameObject.tag = "EnemyBullet";
                break;
            case Directions.Top:
                _rigidBody.velocity = new Vector3(0, _speedMultiplier, 0);
                transform.Rotate(0.0f, 0.0f, 90.0f);
                gameObject.tag = "EnemyBullet";
                break;
            case Directions.Bottom:
                _rigidBody.velocity = new Vector3(0, -_speedMultiplier, 0);
                transform.Rotate(0.0f, 0.0f, -90.0f);
                gameObject.tag = "EnemyBullet";
                break;
            case Directions.TopLeft:
                _rigidBody.velocity = new Vector3(-_speedMultiplier, _speedMultiplier, 0);
                transform.Rotate(0.0f, 0.0f, 150.0f);
                gameObject.tag = "EnemyBullet";
                break;
            case Directions.TopRight:
                _rigidBody.velocity = new Vector3(_speedMultiplier, _speedMultiplier, 0);
                transform.Rotate(0.0f, 0.0f, 30.0f);
                gameObject.tag = "EnemyBullet";
                break;
            case Directions.BottomLeft:
                _rigidBody.velocity = new Vector3(-_speedMultiplier, -_speedMultiplier, 0);
                transform.Rotate(0.0f, 0.0f, -150.0f);
                gameObject.tag = "EnemyBullet";
                break;
            case Directions.BottomRight:
                _rigidBody.velocity = new Vector3(_speedMultiplier, -_speedMultiplier, 0);
                transform.Rotate(0.0f, 0.0f, -30.0f);
                gameObject.tag = "EnemyBullet";
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(gameObject.CompareTag("PlayerBullet") && collision.CompareTag("EnemyBullet"))
        {
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("EnemyBullet") && collision.CompareTag("PlayerBullet"))
        {
            Destroy(gameObject);
        }

        if (gameObject.CompareTag("PlayerBullet") && collision.CompareTag("Asteroid"))
        {
            BulletHit();
        }

        if (gameObject.CompareTag("PlayerBullet") && collision.CompareTag("SmallAsteroid"))
        {
            BulletHit();
        }

        if (gameObject.CompareTag("PlayerBullet") && collision.CompareTag("Enemy"))
        {
            BulletHit();
        }

        if (gameObject.CompareTag("PlayerBullet") && collision.CompareTag("Enemy2"))
        {
            BulletHit();
        }

        if (gameObject.CompareTag("EnemyBullet") && collision.CompareTag("Player"))
        {
            BulletHit();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("TopBorder") || collision.CompareTag("BottomBorder") || collision.CompareTag("RightBorder") || collision.CompareTag("LeftBorder"))
        {
            Destroy(gameObject);
        }
    }

    void BulletHit()
    {
        Destroy(gameObject);
        VFXManager.CreateHit(_tipOfBullet.transform.position);
        SFXManager.PlayAudio(SFXManager.AudioTypes.Hit);
    }
}
