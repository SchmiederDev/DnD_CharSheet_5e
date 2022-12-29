using System;
using System.Collections.Generic;
namespace DnD_CharSheet_5e
{
    [Serializable]
    public class SlotPanelData
    {
        public int SlotsTotal { set; get; }
        public int SlotsExpended { set; get; }
        public List<string> SlotPanelItems { set; get; }
        public List<bool> ItemsPrepared { set; get; }

        public SlotPanelData(int slotsTotal, int slotsExpended, List<string> slotPanelItems, List<bool> itemsPrepared)
        {
            SlotsTotal = slotsTotal;
            SlotsExpended = slotsExpended;
            SlotPanelItems = slotPanelItems;
            ItemsPrepared = itemsPrepared;
        }
    }
}
