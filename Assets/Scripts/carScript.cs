using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carScript : MonoBehaviour
{
    float Speed = 8f;
    public bool isMiddle,movingLeft,movingRight;
    public GameObject EndScreen,headlights;
    public levelManager level;
    bool night;
    private Vector2 touchStart;

    // Start is called before the first frame update
    void Start()
    {
        isMiddle = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchStart = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                Vector2 swipeDelta = touch.position - touchStart;

                if (Mathf.Abs(swipeDelta.x) > Mathf.Abs(swipeDelta.y))
                {
                    if (swipeDelta.x < 0)
                    {
                        if (!movingLeft && !movingRight)
                        {
                            movingLeft = true;
                        }
                    }
                    else if (swipeDelta.x > 0)
                    {
                        if (!movingLeft && !movingRight)
                        {
                            movingRight = true;
                        }
                    }
                }
            }
        }
        if (movingLeft)
        {
            if (isMiddle)
            {
                if (transform.position.x > -1.15f)
                {
                    transform.Translate(-Speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    isMiddle = false;
                    movingLeft = false;
                }
            }
            else
            {
                if (transform.position.x > 0f)
                {
                    transform.Translate(-Speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    isMiddle = true;
                    movingLeft = false;
                }
            }
        }
        if (movingRight)
        {
            if (isMiddle)
            {
                if (transform.position.x < 1.15f)
                {
                    transform.Translate(Speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    isMiddle = false;
                    movingRight = false;
                }
            }
            else
            {
                if (transform.position.x < 0f)
                {
                    transform.Translate(Speed * Time.deltaTime, 0, 0);
                }
                else
                {
                    isMiddle = true;
                    movingRight = false;
                }
            }
        }

        if (Input.GetKeyDown("left"))
        {
            if (!movingLeft && !movingRight)
            {
                movingLeft = true;
            }
        }
        if (Input.GetKeyDown("right"))
        {
            if (!movingLeft && !movingRight)
            {
                movingRight = true;
            }
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


    void OnCollisionEnter2D(Collision2D col)
    {
        gameObject.GetComponent<AudioSource>().Play();
        Time.timeScale = 0;
        level.Crashed();
        EndScreen.SetActive(true);
    }



}
