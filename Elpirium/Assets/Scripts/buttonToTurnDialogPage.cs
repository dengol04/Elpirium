using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ButtonToTurnPageOfDialogType
{
    FARTEHER,
    BACK,
    NONE
}

public class buttonToTurnDialogPage : MonoBehaviour
{
    [SerializeField] private dialogController _dialogController;
    [SerializeField] private ButtonToTurnPageOfDialogType _buttonType;

    private void OnMouseDown()
    {
        switch(_buttonType)
        {
            case ButtonToTurnPageOfDialogType.FARTEHER:
                _dialogController.turnNextSentence();
                break;
            case ButtonToTurnPageOfDialogType.BACK:
                _dialogController.turnPrevSentence();
                break;
            default:
                return;
        }
    }

}
