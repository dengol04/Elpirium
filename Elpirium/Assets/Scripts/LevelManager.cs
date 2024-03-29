using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

public class LevelManager : MonoBehaviour
{
    public static int currentLevel = 1;

    private int Level;
    public void LoadLevel(int id)
    {
        Level = id;
        currentLevel = id;
        SceneManager.LoadScene("Level" + $"{Level}");
    }
    public void LoadCurrent()
    {
        Level = currentLevel;
        SceneManager.LoadScene("Level" + $"{Level}");
    }

    [SerializeField] private int _levelsAmount = 3;
 /*   
#if UNITY_EDITOR
    private void Awake()
    {
        PlayerPrefs.DeleteAll();
    }
#endif*/

    void Start()
    {
        if (PlayerPrefs.GetInt("Level", -1) == -1)
            PlayerPrefs.SetInt("Level", 1);

        Debug.Log("PlayerPrefs: " + PlayerPrefs.GetInt("Level", -1));

        for (int i = 1; i <= _levelsAmount; ++i)
        {
            GameObject levelButton = transform.GetChild(i).gameObject;
            levelButton.GetComponent<Button>().interactable = int.Parse(levelButton.gameObject.name[levelButton.gameObject.name.Length - 1].ToString()) <= PlayerPrefs.GetInt("Level");
        }
    }
}
