using KitchenGame.Runtime;

public class GrillStation : StationBase
{
    public override void Interact(PlayerController player)
    {
        if (isInUse)
            return;

        isInUse = true;

        player.StationsUIs.OpenStationUI(this);
        player.Status.SetInteracting(true);
    }

    public override void ExitStation(PlayerController player)
    {
        isInUse = false;

        player.Status.SetInteracting(false);
    }
}