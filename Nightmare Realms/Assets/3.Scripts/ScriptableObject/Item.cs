using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "ScriptableObject/ItemData")]
public class Item : ScriptableObject
{
    public Sprite sprite;
    public string itemName;
    public string Tear;
    public int damage;
    public int health;
    public int price;
}