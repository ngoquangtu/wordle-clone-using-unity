using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;
    public AudioSource correctwordSource;
    public AudioSource enterSound;
    public AudioSource invalidSound;
    public AudioSource loseSound;

    private void Start()
    {

    }
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void playSound(AudioSource audio)
    {
        audio.Play();
    }

}
