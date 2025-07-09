using UnityEngine;

namespace KitchenGame.Runtime
{
    public abstract class StationBase : MonoBehaviour, IInteractable
    {
        [Header("Station Info")]
        [SerializeField] private string interactionLabel = "Use Station";

        protected bool isInUse = false;

        public abstract void Interact(PlayerController player);

        public string GetInteractionLabel()
        {
            return interactionLabel;
        }

        public virtual void ExitStation(PlayerController player)
        {
            isInUse = false;
            player.Status.SetInteracting(false);
        }
    }
}