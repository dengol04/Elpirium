using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseAndReset : MonoBehaviour
{
    [SerializeField]
    private Button[] _buttonsToDeactivate;
    [SerializeField]
    private GameObject _pauseMenuPanel;

    private bool _isPaused;

    public bool isPaused => _isPaused;

    private void Awake()
    {
        _isPaused = false;
    }

    private void Start()
    {
        _pauseMenuPanel.SetActive(false);
    }

    public void resetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void resetLevelAfterDeath()
    {
        SceneManager.LoadScene("Level" + PlayerPrefs.GetInt("currentLevel"));
    }
    public void tooglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0;

            foreach (var b in _buttonsToDeactivate)
                b.interactable = false;

            _pauseMenuPanel.SetActive(true);

            Debug.Log("Поставлена пауза");
        }
        else
        {
            Time.timeScale = 1;
            foreach (var b in _buttonsToDeactivate)
                b.interactable = true;

            _pauseMenuPanel.SetActive(false);
        }
    }

    public void returnToMainMenu()
    {
        SceneManager.LoadScene("menu");
    }

}
