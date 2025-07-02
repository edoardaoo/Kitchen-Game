using UnityEngine;
using System.Collections.Generic;

namespace KitchenGame.Inventory
{
    public class RestrictedSlot : SlotBase
    {
        [Header("Allowed Types")]
        [SerializeField]
        private List<ItemType> allowedTypes = new();

        public override void SetSlot(SlotItem newItem)
        {
            if (!IsAllowed(newItem))
            {
                Debug.LogWarning($"Item '{newItem.itemInfo.ItemName}' not allowed in this slot. Allowed: {string.Join(", ", allowedTypes)}");
                return;
            }

            base.SetSlot(newItem);
        }

        private bool IsAllowed(SlotItem item)
        {
            return allowedTypes.Contains(item.itemInfo.Type);
        }

        public void SetAllowedTypes(params ItemType[] types)
        {
            allowedTypes = new List<ItemType>(types);
        }

        public List<ItemType> GetAllowedTypes()
        {
            return allowedTypes;
        }
    }
}