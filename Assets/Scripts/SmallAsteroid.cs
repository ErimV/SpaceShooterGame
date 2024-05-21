using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallAsteroid : MonoBehaviour
{
    [SerializeField] float _rotation;
    GameManager _gameManager;
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        transform.Rotate(0, 0, _rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            AsteroidDestroyed();
        }

        if (collision.CompareTag("Player"))
        {
            AsteroidDestroyed();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("LeftBorder") || collision.CompareTag("TopBorder") || collision.CompareTag("BottomBorder"))
        {
            Destroy(gameObject);
        }
    }

    void AsteroidDestroyed()
    {
        Destroy(gameObject);
        VFXManager.CreateExplosion(gameObject.transform.position);
        SFXManager.PlayAudio(SFXManager.AudioTypes.Explosion);
        _gameManager.IncreaseScore(Points.SmallAsteroidDestroyed);
    }
}