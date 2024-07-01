using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


public class levelManager : MonoBehaviour
{

    public float Speed = 5f,temp,maxSpeed=15f;
    public TMP_Text scoreText,endScoreText,highScoreText;
    public float score = 0;
    public int displayScore,highScore,scoreMultiplier = 15;
    public GameObject pauseMenu,carCrashed,smoke,pauseButton;
    bool paused, speedSpecial;
    public UnityEngine.Rendering.Universal.Light2D sun;
    public bool night;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1;
        StartCoroutine(NightCycle());
        paused = false;
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        score += scoreMultiplier * Time.deltaTime;
        displayScore = (int)score;
        if(displayScore > 999)
        {
            scoreText.text = displayScore.ToString();
        }
        else if(displayScore > 99) 
        {
            scoreText.text = "0" + displayScore.ToString();
        }
        else if (displayScore > 9) 
        {
            scoreText.text = "00" + displayScore.ToString();
        }
        else
        {
            scoreText.text = "000"+displayScore.ToString();
        }

        
        if (Speed < maxSpeed)
        {
            if (!speedSpecial)
            {
                Speed += Time.deltaTime * 0.1f;
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
        if (night)
        {
            if (sun.intensity > 0.4f)
            {
                sun.intensity-=Time.deltaTime;
            }
        }
        else
        {
            if (sun.intensity < 1f)
            {
                sun.intensity += Time.deltaTime;
            }
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game");
        
    }

    public void Pause()
    {
        if (!paused)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0;
            paused = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1;
            paused = false;
        }
    }
    public void Home()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Crashed()
    {
        pauseButton.SetActive(false);
        smoke.SetActive(true);
        carCrashed.SetActive(true);
        scoreText.gameObject.SetActive(false);
        endScoreText.text=scoreText.text;
        highScore = PlayerPrefs.GetInt("highScore");
        if (highScore < displayScore)
        {
            highScore = displayScore;
            PlayerPrefs.SetInt("highScore",highScore);
        }
        highScoreText.text=highScore.ToString();
    }
    public void Multiply()
    {
        StartCoroutine(MultiplyScore());
    }
    IEnumerator MultiplyScore()
    {
        scoreMultiplier = 30;
        yield return new WaitForSeconds(10f);
        scoreMultiplier = 15;
    }
    public void SpeedUp()
    {
        StartCoroutine(SpeedingUp());   
    }
    IEnumerator SpeedingUp()
    {
        speedSpecial = true;
        temp = Speed;
        Speed *= 2f;
        if (Speed > maxSpeed)
        {
            Speed = maxSpeed + 2;
        }
        yield return new WaitForSeconds(7.5f);
        Speed = temp;
        speedSpecial= false;
    }
    public void SpeedDown()
    {
        StartCoroutine(SpeedingDown());
    }
    IEnumerator SpeedingDown()
    {
        speedSpecial = true;
        Speed *= 0.5f;
        if (Speed < 5)
        {
            Speed = 4f;
        }
        yield return new WaitForSeconds(7.5f);
        speedSpecial = false;
    }
    IEnumerator NightCycle()
    {
        yield return new WaitForSeconds(30f);
        night = true;
        yield return new WaitForSeconds(30f);
        night = false;
        StartCoroutine(NightCycle());
    }
}
