using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Linq;

namespace KitchenGame.Inventory
{
    public class PlayerInventoryManager : MonoBehaviour, IItemContainer
    {
        [Header("Slots")]
        [SerializeField] GameObject slotsHolder;
        List<SlotBase> slots = new();
        int selectedSlot;
        int maxSlots;

        // References
        InventoryUI ui;

        public List<SlotBase> Slots => slots;

        private void Start()
        {
            // Get references
            slots = slotsHolder.GetComponentsInChildren<SlotBase>().ToList();
            ui = GetComponent<InventoryUI>();
            maxSlots = slots.Count;

            // Subscribe events
            SlotBase.OnSlotClear.AddListener(OnSlotClear);
        }

        public void OnSelectSlot(InputAction.CallbackContext ctx)
        {
            if (ctx.started)
            {
                string keyPressed = ctx.control.name;
                if (int.TryParse(keyPressed, out int slotIndex))
                    SelectSlot(slotIndex - 1);
            }
        }

        public void OnScrollSlot(InputAction.CallbackContext ctx)
        {
            if (ctx.performed)
            {
                float scrollDirection = ctx.ReadValue<Vector2>().y;

                if (scrollDirection > 0)
                    SelectSlot((selectedSlot + 1) % maxSlots);
                else if (scrollDirection < 0)
                    if (selectedSlot == 0)
                        SelectSlot(maxSlots - 1);
                    else
                        SelectSlot(selectedSlot - 1);
            }
        }

        void SelectSlot(int slot)
        {
            // If slot is available, it means it has no item
            if (slots[slot].IsEmpty())
                return;

            selectedSlot = slot;
            ui.SelectSlot(slots[selectedSlot]);
        }

        void DeselectCurrentSlot()
        {
            ui.DeselectSlot(slots[selectedSlot]);
            selectedSlot = -1;
        }

        void OnSlotClear(SlotBase slotCleared)
        {
            if (selectedSlot == -1)
                return;

            if (slotCleared == slots[selectedSlot])
                DeselectCurrentSlot();
        }

        public void UseItem()
        {

        }

        public void RemoveItem()
        {

        }

        public SlotItem GetCurrentItem()
            => slots[selectedSlot].GetItem();
    }
}