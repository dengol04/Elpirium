using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class LevelManager : MonoBehaviour
{
    //public static int currentLevel = 1;

    private int Level;
    public void LoadLevel(int id)
    {
        Level = id;
        PlayerPrefs.SetInt("currentLevel", id);
        SceneManager.LoadScene("dialog" + $"{Level}");
    }
    public void LoadCurrent()
    {
        Level = PlayerPrefs.GetInt("currentLevel");
        SceneManager.LoadScene("dialog" + $"{Level}");
    }


    static int staticLevelsAmount = 3; // количество уровней!!!!!

    static public int levelsAmount => staticLevelsAmount;

    void Start()
    {
        if (PlayerPrefs.GetInt("Level", -1) == -1)
            PlayerPrefs.SetInt("Level", 1);

        Debug.Log("PlayerPrefs: " + PlayerPrefs.GetInt("Level", -1));

        for (int i = 1; i <= staticLevelsAmount; ++i)
        {
            GameObject levelButton = transform.GetChild(i).gameObject;
            Debug.Log("levelButton name: " + levelButton.gameObject.name);
            levelButton.GetComponent<Button>().interactable = int.Parse(levelButton.gameObject.name[levelButton.gameObject.name.Length - 1].ToString()) <= PlayerPrefs.GetInt("Level");
        }
    }
}
