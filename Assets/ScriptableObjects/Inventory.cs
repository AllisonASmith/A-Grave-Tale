using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class InventoryEntry
{
    public ItemScriptableObject item;
    public int amount;
}

[CreateAssetMenu(fileName = "NewInventory", menuName = "ScriptableObjects/Inventory", order = 0)]
public class Inventory : ScriptableObject
{
    public List<InventoryEntry> items;
}