using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Rigidbody2D _rigidBody;
    [SerializeField] float _speedModifier;

    GameManager _gameManager;

    void Awake()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        Move();
    }

    private void Move()
    {
        _rigidBody.velocity = new Vector2(-_speedModifier, 0);
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
            EnemyDestroyed();
        }

        if (collision.CompareTag("Player"))
        {
            EnemyDestroyed();
        }
    }

    void EnemyDestroyed()
    {
        Destroy(gameObject);
        VFXManager.CreateExplosion(gameObject.transform.position);
        SFXManager.PlayAudio(SFXManager.AudioTypes.Explosion);
        _gameManager.IncreaseScore(Points.EnemyDestroyed);
    }
}