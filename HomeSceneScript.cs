using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class HomeSceneScript : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI bestScoreText;
    public TextMeshProUGUI gamesPlayedText;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = $"Welcome: {PlayerPrefs.GetString("Name")}"; 
        bestScoreText.text = $"Best Score: {PlayerPrefs.GetFloat("Best Run Time")}";
        gamesPlayedText.text = $"Games Played: {PlayerPrefs.GetInt("Number of Games Played")}";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    public void ToSettingsScene()
    {
        SceneManager.LoadScene("SettingsScene");
    }

}
