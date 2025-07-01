using System.Collections.Generic;

namespace KitchenGame.Inventory
{
    public interface IItemContainer
    {
        List<SlotBase> Slots { get; }
    }
}