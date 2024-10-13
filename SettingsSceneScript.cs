using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SettingsSceneScript : MonoBehaviour
{

    public TMP_InputField nameInputField;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetString("Name") != "")
        {
            nameInputField.text = PlayerPrefs.GetString("Name");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToHomeScene()
    {
        SceneManager.LoadScene("HomeScene");
    }

    public void ResetStats()
    {
        PlayerPrefs.SetInt("Number of Games Played", 0);
        PlayerPrefs.SetFloat("Best Run Time", 0);
    }

    public void SaveName()
    {
        PlayerPrefs.SetString("Name", nameInputField.text);
    }

}
