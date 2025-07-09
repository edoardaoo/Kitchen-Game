using UnityEngine;
using System;

namespace KitchenGame.Runtime
{
    public class PlayerStatus : MonoBehaviour
    {
        // Flags
        public bool IsInteracting { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsInInventory { get; private set; }

        // Status
        public bool IsInputBlocked => IsPaused || IsInInventory || IsInteracting;

        public Action OnStatusChanged;

        // References
        PlayerController controller;

        private void Start()
        {
            // Get references
            controller = GetComponent<PlayerController>();
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
            bool shouldShowCursor = IsPaused || IsInInventory || IsInteracting;
            Cursor.visible = shouldShowCursor;
            Cursor.lockState = shouldShowCursor ? CursorLockMode.None : CursorLockMode.Locked;
        }

        private void UpdatePlayerMovement()
        {
            controller.Movement.playerCanMove = IsInputBlocked;
            controller.Movement.cameraCanMove = IsInputBlocked;
        }
    }
}