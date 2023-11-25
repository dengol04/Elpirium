using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pauseAndReset : MonoBehaviour
{
    private bool _isPaused;

    [SerializeField]
    private Button[] _buttonsToDeactivate;

    private void Awake()
    {
        _isPaused = false;
    }

    public void resetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void tooglePause()
    {
        _isPaused = !_isPaused;

        if (_isPaused)
        {
            Time.timeScale = 0;

            foreach (var b in _buttonsToDeactivate)
                b.interactable = false;

            Debug.Log("Поставлена пауза");
        }
        else
        {
            Time.timeScale = 1;
            foreach (var b in _buttonsToDeactivate)
                b.interactable = true;
        }
    }

}
