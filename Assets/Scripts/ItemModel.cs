using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewItem", menuName = "Game Item")]
public class ItemModel : ScriptableObject
{
    public string itemName;
    public float price;
    public Sprite sprite;
}
