using UnityEngine;

namespace KitchenGame.Runtime
{
    public abstract class StationBase : MonoBehaviour, IInteractable
    {
        [Header("Station Info")]
        [SerializeField] private string interactionLabel = "Use Station";

        protected bool isInUse = false;

        public abstract void Interact(PlayerController player);
        public abstract void ExitStation(PlayerController player);

        public string GetInteractionLabel() => interactionLabel;
    }
}