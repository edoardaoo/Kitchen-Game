using System;

namespace KitchenGame.Runtime
{
    [Serializable]
    public class SlotItem
    {
        public SlotItem_SO itemInfo;

        // Stack values
        public int currentStacks;

        public SlotItem(SlotItem_SO displayInfo)
        {
            itemInfo = displayInfo;
        }
    }
}