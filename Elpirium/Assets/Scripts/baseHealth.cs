using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class baseHealth : MonoBehaviour
{
    [SerializeField]
    private float _levelHealth;

    public void getDamage(float damage)
    {
        _levelHealth -= damage;

        if (_levelHealth <= 0)
            gameOver();
    }


    public void gameOver()
    {
        Debug.Log("Game Over");

        //SceneManager.LoadScene("");
    }
}
