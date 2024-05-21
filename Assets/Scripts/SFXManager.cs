using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    static SFXManager instance;
    [SerializeField] List<AudioClip> _audioClips;
    AudioSource _audioSource;
    public enum AudioTypes
    {
        Fire,
        Hit,
        Explosion
    }
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlayAudio(AudioTypes type)
    {
        int index = (int) type;
        instance._audioSource.clip = instance._audioClips[index];
        instance._audioSource.Play();
    }
}
