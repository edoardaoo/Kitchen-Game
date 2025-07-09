using KitchenGame.Runtime;

public interface IInteractable
{
    void Interact(PlayerInventoryManager player);
    string GetInteractionLabel(); // Ex: "Open Grill", "Use Oven"
}