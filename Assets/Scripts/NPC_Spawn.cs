using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Spawn : MonoBehaviour
{
    public Transform[] sp;
    public int spNum,sc;
    public GameObject road1,road2;
    public GameObject[] npc;
    float spTime, roadSpeed;
    public levelManager manager;
    bool nocar=false,specialUsed=false;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NextSpawn());
    }

    // Update is called once per frame
    void Update()
    {
        roadSpeed = manager.Speed * 2;
        road1.transform.Translate(0, -roadSpeed * Time.deltaTime, 0);
        road2.transform.Translate(0, -roadSpeed * Time.deltaTime, 0);
        if(road1.transform.position.y <= -10.23f)
        {
            road1.transform.position = new Vector3(0f, 10.23f, 0f);
        }
        if (road2.transform.position.y <= -10.23f)
        {
            road2.transform.position = new Vector3(0f, 10.23f, 0f);
        }
    }
    IEnumerator NextSpawn()
    {
        if (!nocar)
        {
            if (roadSpeed > 20)
            {
                spTime = Random.Range(0.4f, 0.6f);
            }
            else if (roadSpeed > 16)
            {
                spTime = Random.Range(0.4f, 0.8f);
            }
            else
            {
                spTime = Random.Range(0.7f, 1.2f);
            }
            yield return new WaitForSeconds(spTime);
            spNum = Random.Range(0, 3);
            while (true)
            {
                sc = Random.Range(0, 16);
                if (sc==12 || sc==13 || sc==14 || sc==15)
                {
                    if (!specialUsed)
                    {
                        StartCoroutine(SpecialCooldown());
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            Instantiate(npc[sc], sp[spNum].position, sp[spNum].rotation);
            StartCoroutine(NextSpawn());
        }
    }
    public void NoCar()
    {
        StartCoroutine(NoCarSpawn());   
    }
    IEnumerator NoCarSpawn()
    {
        nocar = true;
        yield return new WaitForSeconds(6f);
        nocar = false;
        StartCoroutine(NextSpawn());
    }
    IEnumerator SpecialCooldown()
    {
        specialUsed = true;
        yield return new WaitForSeconds(15f);
        specialUsed = false;
    }
}
