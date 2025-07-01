using UnityEngine;
using TMPro;

namespace KitchenGame.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] TMP_Text currItemText;

        [Header("Slot Highlight Colors")]
        [SerializeField] Color slot_DefaultColor;
        [SerializeField] Color slot_HighlightedColor;

        // Internal values
        SlotBase currentSlot;

        private void Start()
        {
            // Initial values
            ClearItemText();
        }

        public void SelectSlot(SlotBase slot)
        {
            if (currentSlot)
                DeselectSlot(currentSlot);

            currentSlot = slot;
            slot.Highlight(slot_HighlightedColor);
            SetItemText(slot.GetItem().itemInfo.ItemName);
        }

        public void DeselectSlot(SlotBase slot)
        {
            currentSlot = null;
            slot.UnHighlight(slot_DefaultColor);
            ClearItemText();
        }

        void SetItemText(string newText)
            => currItemText.text = newText;

        void ClearItemText()
            => currItemText.text = "";
    }
}