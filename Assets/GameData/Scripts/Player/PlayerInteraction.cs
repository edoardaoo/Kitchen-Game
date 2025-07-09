using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using KitchenGame.Runtime;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] float interactionMaxDistance = 5f;

    [Header("UI")]
    [SerializeField] TMP_Text itemNameText;

    // Internal values
    Camera cam;
    IInteractable currInteraction;
    PlayerInventoryManager playerInventory;

    // Consts
    private const string InteractionInputText = "(E)";
    private const string InteractionInputText_Color = "<color=\"green\">";

    private void Start()
    {
        // Initial values
        ClearItemNameText();

        // Get references
        cam = Camera.main;
        playerInventory = transform.parent.GetComponentInChildren<PlayerInventoryManager>();
    }

    private void Update()
    {
        Raycast();
    }

    void Raycast()
    {
        if(Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, interactionMaxDistance, interactionLayer))
        {
            if (hit.transform.TryGetComponent(out IInteractable interactable) || hit.transform.parent.TryGetComponent(out interactable))
            {
                currInteraction = interactable;
                SetCurrentInteractionText(interactable.GetInteractionLabel(), true);
                return;
            }
        }

        currInteraction = null;
        ClearItemNameText();
    }

    /// <summary>
    /// Called by PlayerInput component, from map Player.
    /// </summary>
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (currInteraction == null)
                return;

            currInteraction.Interact(playerInventory);
        }        
    }

    void SetCurrentInteractionText(string itemName, bool canPick)
    {
        string canPickText = canPick ? $"{InteractionInputText_Color}{InteractionInputText}" : "" ;

        itemNameText.text = $"{itemName} {canPickText}";
    }

    void ClearItemNameText()
    {
        // Avoid cleaning multiple times
        if (string.IsNullOrEmpty(itemNameText.text))
            return;

        itemNameText.text = "";
    }
}