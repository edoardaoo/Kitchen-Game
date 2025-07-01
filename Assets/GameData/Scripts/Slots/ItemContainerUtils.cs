using UnityEngine;

namespace KitchenGame.Inventory
{
    public static class ItemContainerUtils
    {
        public static (bool wasAdded, int remainingStacks) AddItem(IItemContainer container, SlotItem newItem, int quantityToAdd = 1)
        {
            int remainingStacks = quantityToAdd;

            // Stack into existing items first
            foreach (var slot in container.Slots)
            {
                if (!slot.IsEmpty() && slot.IsSameItem(newItem))
                {
                    (bool wasAdded, int remaining) = TryStackItem(slot, remainingStacks);
                    if (wasAdded) remainingStacks = remaining;
                    if (remainingStacks == 0) return (true, 0);
                }
            }

            // Fill into empty slots
            foreach (var slot in container.Slots)
            {
                if (slot.IsEmpty())
                {
                    newItem.currentStacks = remainingStacks;
                    slot.SetSlot(newItem);
                    return (true, 0);
                }
            }

            return (remainingStacks != quantityToAdd, remainingStacks);
        }

        public static (bool wasAdded, int remainingStacks) TryStackItem(SlotBase slotToAdd, int quantityToAdd)
        {
            SlotItem itemToModify = slotToAdd.GetItem();

            if (!itemToModify.itemInfo.CanStack || itemToModify.currentStacks == itemToModify.itemInfo.MaxStackAmount)
                return (false, quantityToAdd);

            int maxStack = itemToModify.itemInfo.MaxStackAmount;
            int spaceAvailable = maxStack - itemToModify.currentStacks;
            int stacksToAdd = Mathf.Min(spaceAvailable, quantityToAdd);

            itemToModify.currentStacks += stacksToAdd;
            int remainingStacks = quantityToAdd - stacksToAdd;

            slotToAdd.UpdateStackAmountText();
            return (true, remainingStacks);
        }
    }
}