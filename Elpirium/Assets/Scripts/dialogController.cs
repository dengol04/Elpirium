using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class dialogController : MonoBehaviour
{
    [SerializeField] private List<string> _sentences;
    [SerializeField] private string _nextSceneName;
    [SerializeField] private GameObject[] _buttonsToGetControl;
    [SerializeField] private List<string> _namesOfCharacters;

    private Text _currentText;
    private int _sentencesCount;
    private Text _currentCharacter;

    private void Awake()
    {
        _currentText = this.GetComponentInChildren<Text>();
        _currentCharacter = this.transform.GetChild(this.transform.childCount - 1).GetComponent<Text>();
        _sentencesCount = -1;
        //_sentences.Add("Нажмите для продолжения");
    }

    private void Start()
    {
        _currentText.text = "Нажмите для продолжения";
        _currentCharacter.text = "";
        foreach (var b in _buttonsToGetControl)
            b.SetActive(false);
    }

    public void turnNextSentence()
    {
        //if (_sentencesCount >= _sentences.Count - 1)
        //SceneManager.LoadScene(_nextSceneName);

        ++_sentencesCount;

        if (_sentencesCount > _sentences.Count - 1)
        {
            SceneManager.LoadScene(_nextSceneName);
            return;
        }


        _currentText.text = _sentences[_sentencesCount];
        _currentCharacter.text = _namesOfCharacters[_sentencesCount];

        if (_sentencesCount == 0)
        {
            _buttonsToGetControl.First(x => x.name == "back").SetActive(false);
            _buttonsToGetControl.First(x => x.name == "farther").SetActive(true);
        }
        if (_sentencesCount == 1)
            _buttonsToGetControl.First(x => x.name == "back").SetActive(true);
        if (_sentencesCount == _sentences.Count - 1)
            _buttonsToGetControl.First(x => x.name == "farther").GetComponent<Text>().color = Color.red;
    }

    public void turnPrevSentence()
    {
        if (_sentencesCount > 0)
            _currentText.text = _sentences[--_sentencesCount];

        if (_sentencesCount == 0)
            _buttonsToGetControl.First(x => x.name == "back").SetActive(false);
        if (_sentencesCount == _sentences.Count - 2)
            _buttonsToGetControl.First(x => x.name == "farther").GetComponent<Text>().color = Color.gray;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (_sentencesCount < 0)
                turnNextSentence();
            if (_sentencesCount > _sentences.Count)
                turnNextSentence();

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            turnNextSentence();
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            turnPrevSentence();
        }
    }

    /* private void OnMouseDown()
     {
         if (_sentencesCount >= _sentences.Count - 1)
             SceneManager.LoadScene(_nextSceneName);
         turnNextSentence();
     }*/

}
