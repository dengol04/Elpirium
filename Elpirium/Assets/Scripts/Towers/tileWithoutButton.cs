using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileWithoutButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Store store = GameObject.Find("Main Camera").GetComponent<Store>();
        if (store.IsTriggered)
        {
            store.SetIsTrieggeredToFalse();
            store.SetDefaultColorToTowersTiles();
        }
    }
}
