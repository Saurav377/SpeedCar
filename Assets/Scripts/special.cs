using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class special : MonoBehaviour { 

    public bool multiply, speedup, speeddown, nocar;
    NPC_Spawn spawn;
    levelManager level;
    public float Speed;
    AudioSource sfx;

    // Start is called before the first frame update
    void Start()
    {
        level = GameObject.FindGameObjectWithTag("LevelManager").GetComponent<levelManager>();
        spawn = GameObject.FindGameObjectWithTag("spawnManager").GetComponent<NPC_Spawn>();
        sfx = GameObject.FindGameObjectWithTag("SpecialSFX").GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Speed = level.Speed;
        transform.Translate(0, -Speed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        sfx.Play();
        if (multiply)
        {
            level.Multiply();
        }
        else if (speedup)
        {
            level.SpeedUp();
        }
        else if (speeddown)
        {
            level.SpeedDown();
        }
        else if (nocar)
        {
            spawn.NoCar();
        }
        Destroy(this.gameObject);
    }
}
