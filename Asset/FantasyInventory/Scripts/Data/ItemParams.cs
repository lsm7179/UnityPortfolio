using System;
using System.Collections.Generic;
using Assets.FantasyInventory.Scripts.Enums;

namespace Assets.FantasyInventory.Scripts.Data
{
    /// <summary>
    /// Represents generic item params (common for all items).
    /// </summary>
    [Serializable]
    public class ItemParams
    {
        public ItemType Type;
        public List<ItemTag> Tags = new List<ItemTag>();
        public List<Property> Properties = new List<Property>();
        public int Price;
    }
}