using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using KitchenGame.Inventory;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] LayerMask interactionLayer;
    [SerializeField] float interactionMaxDistance = 5f;

    [Header("UI")]
    [SerializeField] TMP_Text itemNameText;

    // Internal values
    Camera cam;
    ItemInteraction currInteraction;
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
            currInteraction = hit.transform.GetComponentInParent<ItemInteraction>();
            SetItemNameText(currInteraction.GetItemName(), currInteraction.GetItemStacks(), currInteraction.CanPickItem());
        }
        else
        {
            currInteraction = null;
            ClearItemNameText();
        }
    }

    /// <summary>
    /// Called by PlayerInput component, from map Player.
    /// </summary>
    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (!currInteraction)
                return;

            currInteraction.Interact(playerInventory);
        }        
    }

    void SetItemNameText(string itemName, int stacks, bool canPick)
    {
        string canPickText = canPick ? $"{InteractionInputText_Color}{InteractionInputText}" : "" ;

        itemNameText.text = $"{itemName} ({stacks}) {canPickText}";
    }

    void ClearItemNameText()
        => itemNameText.text = "";
}