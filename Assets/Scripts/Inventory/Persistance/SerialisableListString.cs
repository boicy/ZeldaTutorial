using System.Collections.Generic;

public class SerialisableListString
{
    public struct SerialItem
    {
        public string name;
        public int count;
    }

    public List<SerialItem> inventoryItemList = new List<SerialItem>();
}
