using UnityEngine;
using KitchenGame.Runtime;

public class GrillStation : StationBase
{
    [SerializeField] GameObject uiPanel;

    public override void Interact(PlayerController player)
    {
        if (isInUse)
            return;

        isInUse = true;
        uiPanel.SetActive(true);
        player.Status.SetInteracting(true);
    }

    public override void ExitStation(PlayerController player)
    {
        base.ExitStation(player);
        uiPanel.SetActive(false);
    }
}