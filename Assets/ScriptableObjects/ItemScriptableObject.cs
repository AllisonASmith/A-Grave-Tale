using UnityEngine;

// Create subclasses of this to make new items
public abstract class ItemScriptableObject : ScriptableObject
{
    public int id;
    public string itemName;
    public bool canDrop;
}