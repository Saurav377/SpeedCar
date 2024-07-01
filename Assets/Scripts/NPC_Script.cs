using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Script : MonoBehaviour
{
    public float Speed;
    levelManager level;
    bool night;
    public GameObject headlights;
    // Start is called before the first frame update
    void Start()
    {
        GameObject levelObject = GameObject.FindGameObjectWithTag("LevelManager");
        level = levelObject.GetComponent<levelManager>();
        

    }

    // Update is called once per frame
    void Update()
    {
        Speed = level.Speed;
        transform.Translate(0, -Speed * Time.deltaTime, 0);
        if (transform.position.y < -16f)
        {
            Destroy(this.gameObject);
        }
        night = level.night;
        if (night)
        {
            headlights.SetActive(true);
        }
        else
        {
            headlights.SetActive(false);
        }
    }
}
