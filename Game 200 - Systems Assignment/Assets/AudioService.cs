using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioService : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource bath;
    public AudioSource play;

    public AudioSource food;
    public AudioSource gameOver;

    private AudioSource source;


    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    public void PlayBath()
    {
        bath.enabled = true;
        bath.Play();
    }

    public void PlayFood()
    {
        food.enabled = true;
        food.Play();
    }

    public void PlayGameOver()
    {
        gameOver.enabled = true;
        gameOver.Play();
    }

    public void PlayPlay()
    {
        play.enabled = true;
        play.Play();
    }

}
