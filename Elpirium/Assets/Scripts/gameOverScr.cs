using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameOverScr : MonoBehaviour
{
    public void loadMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void loadPrevLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
