using UnityEngine;

namespace KitchenGame.Runtime
{
    [DefaultExecutionOrder(-1000)]
    public class PlayerController : MonoBehaviour
    {
        // References
        public PlayerStatus Status { get; private set; }
        public PlayerInventoryManager Inventory { get; set; }
        public FirstPersonController Movement { get; set; }

        private void Awake()
        {
            // Get references
            Status = GetComponent<PlayerStatus>();
            Inventory = transform.parent.GetComponentInChildren<PlayerInventoryManager>();
            Movement = transform.parent.GetComponent<FirstPersonController>();
        }
    }
}