using UnityEngine;
using KitchenGame.Runtime;

namespace KitchenGame.Cooking
{
    public class ItemInteraction : MonoBehaviour, IInteractable
    {
        [Header("Item Infos")]
        [SerializeField] SlotItem_SO itemInfo;
        [Header("Item Status")]
        [SerializeField] bool canPick;
        [SerializeField] int stacks = 1;

        // Internal values
        private SlotItem iItem = new(null);

        private void Awake()
        {
            iItem = new(itemInfo);
        }

        public void Interact(PlayerInventoryManager player)
        {
            if (!canPick)
                return;

            IItemContainer container = player.GetComponent<IItemContainer>();

            (bool wasAdded, int remainingStacks) = ItemContainerUtils.AddItem(container, iItem, stacks);
            stacks = remainingStacks;

            if (wasAdded && stacks <= 0)
                Destroy(gameObject);
        }

        public string GetInteractionLabel()
        {
            return $"Pegar {itemInfo.ItemName} ({stacks})";
        }

        public string GetItemName() => itemInfo.ItemName;
        public int GetItemStacks() => stacks;
        public bool CanPickItem() => canPick;
    }
}