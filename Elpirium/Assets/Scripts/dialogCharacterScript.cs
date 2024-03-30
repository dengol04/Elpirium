using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dialogCharacterScript : MonoBehaviour
{
    [SerializeField] private List<int> _numsOfSentencesToAppear;
    [SerializeField] private List<int> _numsOfSentencesToDisappear;

    private GameObject[] _characters;

    public void updateActiveOfCharacter(int numOfCurrentSentece)
    {
        if (_numsOfSentencesToAppear.Contains(numOfCurrentSentece))
            this.gameObject.SetActive(true);
        else if (_numsOfSentencesToDisappear.Contains(numOfCurrentSentece))
            this.gameObject.SetActive(false);
    }

}
