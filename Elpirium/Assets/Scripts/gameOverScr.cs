using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class gameOverScr : MonoBehaviour
{
    public void loadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void loadNextLevel()
    {
        SceneManager.LoadScene("dialog" + PlayerPrefs.GetInt("currentLevel"));
    }

    private void Start()
    {
        Debug.Log("Current level: " + PlayerPrefs.GetInt("currentLevel"));
        Debug.Log("Levels Amount: " + LevelManager.levelsAmount);
        Debug.Log("Current level <= Levels Amount: " + (PlayerPrefs.GetInt("currentLevel") <= LevelManager.levelsAmount));
        if (PlayerPrefs.GetInt("currentLevel") > LevelManager.levelsAmount)
        {
            GameObject.Find("nextLevelButton").GetComponent<Button>().enabled = false;
            int currLev = PlayerPrefs.GetInt("currentLevel");
            PlayerPrefs.SetInt("currentLevel", --currLev);
        }
        else
            GameObject.Find("nextLevelButton").GetComponent<Button>().enabled = true;
    }

    public void loadPrevLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
