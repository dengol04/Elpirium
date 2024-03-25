using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class dialogController : MonoBehaviour
{
    [SerializeField] private List<string> _sentences;

    private Text _currentText;
    private int _sentencesCount;

    private void Awake()
    {
        _currentText = this.GetComponentInChildren<Text>();
        _sentencesCount = -1;
    }

    private void Start()
    {
        _currentText.text = "123";
    }

    private void turnNextSentence()
    {
        _currentText.text = _sentences[++_sentencesCount];
    }

    private void OnMouseDown()
    {
        if (_sentencesCount >= _sentences.Count - 1)
            SceneManager.LoadScene("Level1");
        turnNextSentence();
    }

}
