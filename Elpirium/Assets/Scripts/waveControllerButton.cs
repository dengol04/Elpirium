using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.UI;

public class waveControllerButton : MonoBehaviour
{
    [SerializeField]
    private Text _buttonText;
    [SerializeField]
    private GameObject _wavesControllerPanel;

    private void Start()
    {
        _buttonText.text = "Start first wave";
    }


    private bool _isPressed;
    public bool isPressed => _isPressed;

    private void Awake()
    {
        _isPressed = false;
    }

    public void pressButton()
    {
        _isPressed = true;
        _wavesControllerPanel.SetActive(false);
        _buttonText.text = "Next wave";
    }

    public void unpressButton()
    {
        _isPressed = false;
        _wavesControllerPanel.SetActive(true);
    }
}
