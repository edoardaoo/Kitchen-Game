using KitchenGame.Runtime;

public interface IInteractable
{
    void Interact(PlayerController player);
    string GetInteractionLabel(); // Ex: "Open Grill", "Use Oven"
}