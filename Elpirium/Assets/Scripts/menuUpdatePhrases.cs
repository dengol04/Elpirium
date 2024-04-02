using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class menuUpdatePhrases : MonoBehaviour
{
    [SerializeField] private List<string> _phrases;


    void Start()
    {
        GameObject.Find("phrase").GetComponent<Text>().text = _phrases[Random.Range(0, _phrases.Count - 1)];
    }

}
