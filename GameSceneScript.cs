using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameSceneScript : MonoBehaviour
{

    // Camera Switch Variables
    private int camNum = 0;
    public GameObject[] vcamList;

    // Game Time Variables
    public float timeRemaining = 60f;
    private bool timerIsRunning = false;
    public TextMeshProUGUI timerText;

    // Game Over variables
    public GameObject gameOverPanel;
    public TextMeshProUGUI scoreText;

    // Projectile Variables
    public GameObject projectile;
    public Transform originalPosition;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Number of Games Played", PlayerPrefs.GetInt("Number of Games Played")+1);
        StartCoroutine(DelayStartCountdown());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SetCams();
        }
        if(Input.GetKey(KeyCode.Space))
        {
            GameObject bullet = Instantiate(projectile, originalPosition.position+transform.forward * 1.5f, Quaternion.identity) as GameObject;
            bullet.transform.localScale = new Vector3(0.1773227f, 0.1773227f, 0.1773227f);
            bullet.GetComponent<Rigidbody>().AddForce(transform.forward * 500);
        }
    }

    public void ToHomeScene()
    {
        SceneManager.LoadScene("HomeScene");
    }

    void SetCams()
    {
        camNum++;
        if (camNum >= vcamList.Length)
            camNum = 0;
        foreach (GameObject vcam in vcamList)
            vcam.SetActive(false);
        vcamList[camNum].SetActive(true);
    }

    // Game Time Countdown
    private IEnumerator Countdown()
    {
        while (timeRemaining > 0 && timerIsRunning)
        {
            yield return new WaitForSeconds(1f);
            timeRemaining--;
            PlayerPrefs.SetFloat("Current Score", timeRemaining);
            timerText.text = $"Seconds Left: {timeRemaining}";
        }
        GameOver();
        Debug.Log("Time's up!");
    }

    // Coroutine to wait and call ToHomeScene
    private IEnumerator WaitAndGoToHome()
    {
        yield return new WaitForSeconds(2f);
        ToHomeScene();
    }

    // Game Over function
    public void GameOver()
    {
        timerIsRunning = false;
        scoreText.text = $"Your Score is: {timeRemaining+1}";
        gameOverPanel.SetActive(true);
        // Start a coroutine to wait for 2 seconds and then call ToHomeScene()
        StartCoroutine(WaitAndGoToHome());
    }

    // Coroutine to delay the start of the countdown by 29 seconds
    private IEnumerator DelayStartCountdown()
    {
        yield return new WaitForSeconds(29f);
        timerIsRunning = true;
        StartCoroutine(Countdown());
    }

}
