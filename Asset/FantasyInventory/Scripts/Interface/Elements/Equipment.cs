using System.Collections.Generic;
using System.Linq;
using Assets.FantasyInventory.Scripts.Data;
using Assets.FantasyInventory.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.FantasyInventory.Scripts.Interface.Elements
{
    /// <summary>
    /// Represents hero (player) equipment. Based on equipment slots.
    /// </summary>
    public class Equipment : ItemContainer
    {
        /// <summary>
        /// Defines what kinds of items can be equipped.
        /// </summary>
        public List<ItemSlot> Slots;

        /// <summary>
        /// Used as parent object for new item instances.
        /// </summary>
        public Transform Grid;

        /// <summary>
        /// Equipped items will be instantiated in front of equipment slots.
        /// </summary>
        public GameObject ItemPrefab;

        private readonly List<InventoryItem> _inventoryItems = new List<InventoryItem>(); 

        public void OnValidate()
        {
            Slots = GetComponentsInChildren<ItemSlot>().ToList();
        }

        public override void Refresh()
        {
            Reset();

            foreach (var slot in Slots)
            {
                var item = FindItem(slot);

                slot.gameObject.SetActive(item == null);

                if (item != null)
                {
                    var inventoryItem = Instantiate(ItemPrefab, Grid).GetComponent<InventoryItem>();

                    inventoryItem.Item = item;
                    inventoryItem.Count.text = null;
                    inventoryItem.transform.position = slot.transform.position;
                    inventoryItem.transform.SetSiblingIndex(slot.transform.GetSiblingIndex());
                    _inventoryItems.Add(inventoryItem);
                    CopyDragReceiver(inventoryItem, slot);
                }
            }
        }

        private void Reset()
        {
            foreach (var inventoryItem in _inventoryItems)
            {
                Destroy(inventoryItem.gameObject);
            }

            _inventoryItems.Clear();
        }

        private Item FindItem(ItemSlot slot)
        {
            if (slot.ItemType == ItemType.Shield)
            {
                var twoHandedWeapon = Items.SingleOrDefault(i => i.Params.Type == ItemType.Weapon && i.Params.Tags.Contains(ItemTag.TwoHanded));

                if (twoHandedWeapon != null)
                {
                    return twoHandedWeapon;
                }
            }

            var index = Slots.Where(i => i.ItemType == slot.ItemType).ToList().IndexOf(slot);
            var items = Items.Where(i => i.Params.Type == slot.ItemType).ToList();

            return index < items.Count ? items[index] : null;
        }

        private static void CopyDragReceiver(InventoryItem inventoryItem, ItemSlot slot)
        {
            var copy = inventoryItem.gameObject.AddComponent<DragReceiver>();
            var sample = slot.GetComponent<DragReceiver>();

            copy.TweenTargets = new List<Image> { inventoryItem.Icon, inventoryItem.Frame };
            copy.ColorDropAllowed = sample.ColorDropAllowed;
            copy.ColorDropDenied = sample.ColorDropDenied;
            copy.ItemTypes = sample.ItemTypes;
            copy.ItemTags = sample.ItemTags;
            copy.Group = sample.Group;
        }
    }
}