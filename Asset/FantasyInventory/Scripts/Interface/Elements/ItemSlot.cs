using System.Collections.Generic;
using Assets.FantasyInventory.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.FantasyInventory.Scripts.Interface.Elements
{
    /// <summary>
    /// Represents equipment slot. Inventory items can be dragged on it.
    /// </summary>
    [RequireComponent(typeof(DragReceiver))]
    public class ItemSlot : MonoBehaviour
    {
        public Image Icon;
        public ItemType ItemType;

        /// <summary>
        /// Filter compatible items by tags (for example filter weapons: sword, axe, dagger or bow).
        /// </summary>
        public List<ItemTag> ItemTags;

        public void Start()
        {
            var dragReceiver = GetComponent<DragReceiver>();

            dragReceiver.ItemTypes = new List<ItemType> { ItemType };
            dragReceiver.ItemTags = ItemTags;
        }
    }
}