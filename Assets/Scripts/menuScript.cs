using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class menuScript : MonoBehaviour
{

    public GameObject loading, play;
    public TMP_Text high;
    public AudioSource car;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        Time.timeScale = 1.0f;
        StartCoroutine(MenuAnim());
        high.text = PlayerPrefs.GetInt("highScore").ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator MenuAnim()
    {
        yield return new WaitForSeconds(0.39f);
        loading.SetActive(true);
        play.SetActive(true);
        high.gameObject.SetActive(true);
        Time.timeScale = 0;

    }
    public void Play()
    {
        StartCoroutine(LoadScene());
    }
    IEnumerator LoadScene()
    {
        Time.timeScale = 1;
        car.Play();
        play.SetActive(false);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene("Game");
    }
}
