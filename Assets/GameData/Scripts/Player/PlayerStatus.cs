using UnityEngine;
using System;

namespace KitchenGame.Runtime
{
    public class PlayerStatus : MonoBehaviour
    {
        // Flags
        [field: SerializeField] public bool IsInteracting { get; private set; }
        [field: SerializeField] public bool IsPaused { get; private set; }
        [field: SerializeField] public bool IsInInventory { get; private set; }

        // Status
        public bool IsInputBlocked => IsPaused || IsInInventory || IsInteracting;
        public bool ShouldShowCursor => IsPaused || IsInInventory || IsInteracting;

        public Action OnStatusChanged;

        // References
        PlayerController controller;

        private void Start()
        {
            // Get references
            controller = GetComponent<PlayerController>();

            // Initial values
            UpdateCursorVisibility();
        }

        public void SetInteracting(bool value)
        {
            IsInteracting = value;
            TriggerStatusChange();
        }

        public void SetPaused(bool value)
        {
            IsPaused = value;
            TriggerStatusChange();
        }

        public void SetInventoryOpen(bool value)
        {
            IsInInventory = value;
            TriggerStatusChange();
        }

        private void TriggerStatusChange()
        {
            UpdateCursorVisibility();
            UpdatePlayerMovement();
            OnStatusChanged?.Invoke();
        }

        private void UpdateCursorVisibility()
        {
            Cursor.visible = ShouldShowCursor;
            Cursor.lockState = ShouldShowCursor ? CursorLockMode.None : CursorLockMode.Locked;
        }

        private void UpdatePlayerMovement()
        {
            controller.Movement.playerCanMove = !IsInputBlocked;
            controller.Movement.cameraCanMove = !IsInputBlocked;
            controller.Movement.CrosshairObject.enabled = !IsInputBlocked;
        }
    }
}