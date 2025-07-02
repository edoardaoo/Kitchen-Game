using UnityEngine;
using System;

namespace KitchenGame.Player
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
        FirstPersonController playerController;

        private void Awake()
        {
            // Get references
            playerController = GetComponent<FirstPersonController>();
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
            playerController.playerCanMove = IsInputBlocked;
            playerController.cameraCanMove = IsInputBlocked;
        }
    }
}