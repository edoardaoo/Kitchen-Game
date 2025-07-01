using UnityEngine;
using KitchenGame.Inventory;

public class ItemInteraction : MonoBehaviour
{
    [Header("Item Infos")]
    [SerializeField] SlotItem_SO itemInfo;
    [Header("Item Status")]
    [SerializeField] bool canPick;
    [SerializeField] int stacks = 1;

    // Internal values
    SlotItem iItem = new(null);

    private void Awake()
    {
        // Initial values
        iItem = new(itemInfo);
    }

    public void Interact(IItemContainer container)
    {
        if (!canPick)
            return;

        (bool wasAdded, int remainingStacks) = ItemContainerUtils.AddItem(container, iItem, stacks);
        stacks = remainingStacks;

        if (wasAdded && stacks <= 0)
            Destroy(gameObject);
    }

    public string GetItemName() => itemInfo.ItemName;
    public int GetItemStacks() => stacks;
    public bool CanPickItem() => canPick;
}