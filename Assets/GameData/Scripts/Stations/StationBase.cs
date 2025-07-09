using UnityEngine;

namespace KitchenGame.Runtime
{
    public abstract class StationBase : MonoBehaviour, IInteractable
    {
        [Header("Station Info")]
        [SerializeField] private string interactionLabel = "Use Station";

        protected bool isInUse = false;

        public abstract void Interact(PlayerInventoryManager player);

        public string GetInteractionLabel()
        {
            return interactionLabel;
        }

        public virtual void ExitStation(PlayerInventoryManager player)
        {
            isInUse = false;
            if (player.TryGetComponent(out PlayerStatus status))
                status.SetInteracting(false);
        }
    }
}