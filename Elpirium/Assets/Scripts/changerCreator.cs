using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.Aseprite;
//
using Unity.VisualScripting;
//
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.VFX;

public class changerCreator: MonoBehaviour
{
    // ��������� ���������
    [SerializeField]
    private levelData _dataLevel;
    //����� ��� �� �������� ���� dengol04
    [SerializeField]
    private GameObject _changerPref;
    //[SerializeField]
    //private GameObject _parent;
    //��� ������� ���� ����� ����������� ��, ��� � ��������� ����

    public GameObject changerPref => _changerPref;

    public void Spawn()
    {
        GameObject newChanger = Instantiate(_changerPref);
    }

    /*public void OnMouseDown()
    {
        GameObject newChanger = Instantiate(_changerPref);
        newChanger.transform.SetParent(_parent.transform, false);

        newChanger.GetComponent<SpriteRenderer>().sprite = _dataLevel.Changer;

        float changerSizeX = newChanger.GetComponent<SpriteRenderer>().bounds.size.x;
        float changerSizeY = newChanger.GetComponent<SpriteRenderer>().bounds.size.y;

        newChanger.transform.position = new Vector2(changerSizeX, changerSizeY);
    }*/


}
