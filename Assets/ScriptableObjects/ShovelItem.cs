using UnityEngine;

namespace ScriptableObjects.InventoryItems
{
    [CreateAssetMenu(fileName = "Shovel", menuName = "ScriptableObjects/Items/Shovel", order = 0)]
    public class ShovelItem : ItemScriptableObject
    {
        public int damage;
    }
}