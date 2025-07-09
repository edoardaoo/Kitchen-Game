using UnityEngine;
using KitchenGame.Runtime;

public class GrillStation : StationBase
{
    [SerializeField] GameObject uiPanel;

    public override void Interact(PlayerInventoryManager player)
    {
        if (isInUse)
            return;

        isInUse = true;
        uiPanel.SetActive(true);
        if (player.TryGetComponent(out PlayerStatus status))
            status.SetInteracting(true);
    }

    public override void ExitStation(PlayerInventoryManager player)
    {
        base.ExitStation(player);
        uiPanel.SetActive(false);
    }
}