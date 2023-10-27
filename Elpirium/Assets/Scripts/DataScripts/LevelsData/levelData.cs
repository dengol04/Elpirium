using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "Data/New LevelData")]
public class levelData : ScriptableObject
{
    [SerializeField]
    private string[] _way;
    [SerializeField]
    private Sprite[] _groundSprites;

    public string[] Way => _way;
    public Sprite[] GroundSprites => _groundSprites;
}

