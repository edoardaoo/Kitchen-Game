using UnityEngine;

namespace KitchenGame.Inventory
{
    [CreateAssetMenu(fileName = "ItemDisplay", menuName = "Items/Item Display")]
    public class SlotItem_SO : ScriptableObject
    {
        public string ItemName;
        public Sprite ItemIcon;
        [TextArea(1, 3)] public string ItemDescription;
        public GameObject ItemPrefab;
        [Header("Inventory Values")]
        [Range(1, 20)] public int MaxStackAmount;
        [field: SerializeField] public bool CanStack { get; private set; }
        [Header("Classification")]
        public ItemType Type;

        // Internal values
        [SerializeField, HideInInspector] private int itemID;
        public int ItemID => itemID;

#if UNITY_EDITOR
        public void SetIDEditorOnly(int newID)
        {
            itemID = newID;
        }
#endif
    }

    public enum ItemType
    {
        Bread,
        Meat,
        Vegetable,
        Cheese,
        Sauce,
        Drink,
        Tool,
        Other
    }
}