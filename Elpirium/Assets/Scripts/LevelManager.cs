using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;
using static System.Net.Mime.MediaTypeNames;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int currentLevel;

    private int Level;
    public void LoadLevel(int id)
    {
        Level = id;
        SceneManager.LoadScene("Level" + $"{Level}");
    }
    public void LoadCurrent()
    {
        Level = PlayerPrefs.GetInt("Level");
        SceneManager.LoadScene("Level" + $"{Level}");
    }
    void Start()
    {
        if (currentLevel != 0)
            PlayerPrefs.SetInt("Level", currentLevel);
    }
}
