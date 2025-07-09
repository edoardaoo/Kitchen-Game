using System.Collections.Generic;

namespace KitchenGame.Runtime
{
    public interface IItemContainer
    {
        List<SlotBase> Slots { get; }
    }
}