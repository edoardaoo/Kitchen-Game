using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using TMPro;

namespace KitchenGame.Inventory
{
    public class SlotBase : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
    {
        [Header("UI")]
        [SerializeField] protected Image slotGraphic;
        [SerializeField] protected Image iconImg;
        [SerializeField] protected TMP_Text stackAmountText;

        // Internal values
        public SlotItem CurrentItem { get; private set; }
        protected RectTransform originalRect;
        protected Selectable selectable;
        List<RaycastResult> mouseRayResults = new();

        // Flags
        protected bool hasItem;

        // Events
        public UnityEvent<SlotBase> OnSlotSet { get; private set; } = new();
        public static UnityEvent<SlotBase> OnSlotClear { get; private set; } = new();

        private void Awake()
        {
            // Get references
            originalRect = GetComponent<RectTransform>();
            selectable = GetComponent<Selectable>();
        }

        private void Start()
        {
            // Initial values
            ClearSlot();
        }

        public void SetSlot(SlotItem newItem)
        {
            CurrentItem = newItem;
            hasItem = true;
            SetSlotUI();

            OnSlotSet?.Invoke(this);
        }

        public void ClearSlot()
        {
            CurrentItem = null;
            hasItem = false;
            ClearSlotUI();

            OnSlotClear?.Invoke(this);
        }

        private void SetSlotUI()
        {
            selectable.interactable = true;
            UpdateStackAmountText();

            iconImg.sprite = CurrentItem.itemInfo.ItemIcon;
        }

        private void ClearSlotUI()
        {
            hasItem = false;
            selectable.interactable = false;
            stackAmountText.text = "";

            // TODO: add default image for every sprite?
            iconImg.sprite = null;
        }

        public SlotItem GetItem() => CurrentItem;
        public bool IsEmpty() => !hasItem;
        public bool IsSameItem(SlotItem toCompare)
        {
            if (!hasItem)
                return false;

            return CurrentItem.itemInfo.ItemID == toCompare.itemInfo.ItemID;
        }
        public void UpdateStackAmountText()
        {
            if (CurrentItem == null)
                return;

            stackAmountText.text = CurrentItem.currentStacks.ToString();
        }

        #region Highlight/UnHighlight
        public void Highlight(Color newColor)
            => slotGraphic.color = newColor;

        public void UnHighlight(Color newColor)
            => slotGraphic.color = newColor;
        #endregion

        #region Drag Events
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!hasItem)
                return;

            Vector2 originalSize = originalRect.sizeDelta;
            // Set as Canvas child
            slotGraphic.transform.SetParent(transform.parent.parent.parent, false);
            // Center the anchor
            slotGraphic.rectTransform.anchorMin = new(0.5f, 0.5f);
            slotGraphic.rectTransform.anchorMax = new(0.5f, 0.5f);
            // Reset to the original size and set as last sibling (for rendering in front of other elements)
            slotGraphic.rectTransform.sizeDelta = originalSize;
            slotGraphic.transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!hasItem)
                return;

            slotGraphic.rectTransform.position = new(eventData.position.x, eventData.position.y, 0);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!hasItem)
                return;

            EventSystem.current.RaycastAll(eventData, mouseRayResults);
            for (int i = 0; i < mouseRayResults.Count; i++)
            {
                if (mouseRayResults[i].gameObject.transform.parent.TryGetComponent(out SlotBase hoveredSlot))
                {
                    if (hoveredSlot == this)
                        continue;

                    if (hoveredSlot.IsEmpty())
                    {
                        hoveredSlot.SetSlot(CurrentItem);
                        ClearSlot();
                        break;
                    }
                    else
                    {
                        // Switch slots
                        SlotItem otherSlotItem = hoveredSlot.CurrentItem;
                        hoveredSlot.SetSlot(CurrentItem);
                        SetSlot(otherSlotItem);
                    }
                }
            }

            slotGraphic.transform.SetParent(transform, false);
            slotGraphic.rectTransform.anchoredPosition = Vector3.zero;
        }
        #endregion
    }
}