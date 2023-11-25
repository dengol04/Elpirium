using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class baseHealth : MonoBehaviour
{
    [SerializeField]
    private float _levelHealth;

    [SerializeField]
    private GameObject _textAboutHealth;

    private void Start()
    {
        _textAboutHealth.GetComponent<Text>().text = _levelHealth.ToString();
    }

    public void getDamage(float damage)
    {
        _levelHealth -= damage;

        _textAboutHealth.GetComponent<Text>().text = _levelHealth >= 0 ? _levelHealth.ToString() : "0";

        if (_levelHealth <= 0)
            gameOver();
    }


    public void gameOver()
    {
        //Debug.Log("Game Over");

        //SceneManager.LoadScene("GameOver");
    }
}
