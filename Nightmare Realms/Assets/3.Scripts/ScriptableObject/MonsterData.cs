using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Monster", menuName = "ScriptableObject/MonsterData")]
public class MonsterData : ScriptableObject
{
    public int curHP;
    public int maxHP;
}
