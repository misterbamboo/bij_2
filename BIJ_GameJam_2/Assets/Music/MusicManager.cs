using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource gameMusic;
    [SerializeField] float startTime;
    [SerializeField] float cropEnd;
    private float croptime;

    void Start()
    {
        croptime = gameMusic.clip.length - cropEnd;
        
    }

    void Update()
    {
        StartIfStopped();
        CropEndAndLoop();
    }

    private void StartIfStopped()
    {
        if (!gameMusic.isPlaying)
        {
            gameMusic.time = startTime;
            gameMusic.Play();
        }
    }

    private void CropEndAndLoop()
    {
        if (gameMusic.time > croptime)
        {
            gameMusic.time = 0;
        }
    }
}
