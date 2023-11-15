using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class storeOfTowersCreator : MonoBehaviour
{
    [SerializeField]
    private GameObject _storePref;

    private bool isMenuOpen;

    private GameObject newStore;

    private void Start()
    {
        isMenuOpen = false;
        newStore = null;
    }

    private void OnMouseDown()
    {
        Debug.Log("Нажата кнопка");

        if (!isMenuOpen)
        {
            newStore = Instantiate(_storePref);
            newStore.transform.position = new Vector2(transform.position.x + gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.x / 2, 
                                           transform.position.y + gameObject.GetComponent<SpriteRenderer>().sprite.bounds.size.y / 2);
            isMenuOpen = true;
        }
        else
        {
            Destroy(newStore);
            isMenuOpen = false;
        }
    }
}
    
