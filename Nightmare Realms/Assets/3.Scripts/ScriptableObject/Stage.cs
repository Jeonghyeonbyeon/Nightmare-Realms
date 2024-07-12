using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Stage", menuName = "ScriptableObject/StageData")]
public class Stage : ScriptableObject
{
    public string stageName;
    public Vector3 spawnPos;
    public int monsterCount;
}