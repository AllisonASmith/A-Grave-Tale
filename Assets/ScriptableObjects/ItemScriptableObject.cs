using UnityEngine;

// Create subclasses of this to make new items
namespace ScriptableObjects
{
    public abstract class ItemScriptableObject : ScriptableObject
    {
        public int id;
        public string itemName;
    }
}