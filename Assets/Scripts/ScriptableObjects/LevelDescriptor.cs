using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelDescriptor", menuName = "ScriptableObjects/LevelDescriptor")]
public class LevelDescriptor : ScriptableObject
{
    public int CreepsToSpawn;
    public GameObject CreepPrefab;
    public float TimeBetweenSpawns;
}
