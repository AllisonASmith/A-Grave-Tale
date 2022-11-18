using System;
using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
    [Serializable]
    public class InventoryEntry
    {
        public ItemScriptableObject item;
        public int amount;
        public bool canDrop;
    }
    
    [Serializable]
    public class CoreInventoryEntry
    {
        public ItemScriptableObject item;
    }
    
    [CreateAssetMenu(fileName = "NewInventory", menuName = "ScriptableObjects/Inventory", order = 0)]
    public class Inventory : ScriptableObject
    {
        public List<CoreInventoryEntry> coreItems;
        public List<InventoryEntry> items;
    }
}