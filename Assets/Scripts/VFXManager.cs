using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXManager : MonoBehaviour
{
    [SerializeField] GameObject _explosion;
    [SerializeField] GameObject _hit;
    static VFXManager instance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        instance = this;
    }

    public static void CreateExplosion(Vector3 position)
    {
        var explosion = Instantiate(instance._explosion);
        explosion.transform.position = position;
    }

    public static void CreateHit(Vector3 position)
    {
        var explosion = Instantiate(instance._hit);
        explosion.transform.position = position;
    }
}
