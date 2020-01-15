using System.Collections.Generic;
using System.Linq;
using Assets.FantasyInventory.Scripts.Data;
using Assets.FantasyInventory.Scripts.Enums;
using Assets.FantasyInventory.Scripts.GameData;
using Assets.FantasyInventory.Scripts.Interface.Elements;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.FantasyInventory.Scripts.Interface
{
    /// <summary>
    /// High-level inventory inverface.
    /// </summary>
    public class Inventory : ItemWorkspace
    {
        public Equipment Equipment;
        public ScrollInventory Bag;
        public Button EquipButton;
        public Button RemoveButton;
        public AudioSource AudioSource;
        public AudioClip EquipSound;
        public AudioClip RemoveSound;

        /// <summary>
        /// Initialize owned items (just for example).
        /// </summary>
        public void Awake()
        {
            var inventory = new List<Item>
            {
                new Item(ItemId.FireballScroll, 1),
                new Item(ItemId.Flute, 2),
                new Item(ItemId.Gold, 2000),
                new Item(ItemId.HealthPotion, 10),
                new Item(ItemId.IronSword, 1),
                new Item(ItemId.IvyBow, 1),
                new Item(ItemId.LeatherArmor, 1),
                new Item(ItemId.LeatherHelmet, 1),
                new Item(ItemId.ManaPotion, 2),
                new Item(ItemId.RoundShield, 1),
                new Item(ItemId.SilverRing, 2),
                new Item(ItemId.Spear, 1),
                new Item(ItemId.StoneAmulet, 1),
                new Item(ItemId.TwoHandedSword, 1),
                new Item(ItemId.WoodcutterAxe, 2)
            };

            var equipped = new List<Item>();

            Bag.Initialize(ref inventory);
            Equipment.Initialize(ref equipped);
        }

        protected void Start()
        {
            Reset();
            EquipButton.interactable = RemoveButton.interactable = false;

            // TODO: Assigning static callbacks. Don't forget to set null values when UI will be closed. You can also use events instead.
            InventoryItem.OnItemSelected = SelectItem;
            InventoryItem.OnDragStarted = SelectItem;
            InventoryItem.OnDragCompleted = InventoryItem.OnDoubleClick = item => { if (Bag.Items.Contains(item)) Equip(); else Remove(); };
        }

        public void SelectItem(Item item)
        {
            SelectItem(item.Id);
        }

        public void SelectItem(ItemId itemId)
        {
            SelectedItem = itemId;
            SelectedItemParams = Items.Params[itemId];
            ItemInfo.Initialize(SelectedItem, SelectedItemParams);
            Refresh();
        }

        public void Equip()
        {
            var equipped = Equipment.Items.LastOrDefault(i => i.Params.Type == SelectedItemParams.Type);

            if (equipped != null)
            {
                AutoRemove(SelectedItemParams.Type, Equipment.Slots.Count(i => i.ItemType == SelectedItemParams.Type));
            }

            if (SelectedItemParams.Tags.Contains(ItemTag.TwoHanded))
            {
                var shield = Equipment.Items.SingleOrDefault(i => i.Params.Type == ItemType.Shield);

                if (shield != null)
                {
                    MoveItem(shield, Equipment, Bag);
                }
            }
            else if (SelectedItemParams.Type == ItemType.Shield)
            {
                var weapon2H = Equipment.Items.SingleOrDefault(i => i.Params.Tags.Contains(ItemTag.TwoHanded));

                if (weapon2H != null)
                {
                    MoveItem(weapon2H, Equipment, Bag);
                }
            }

            MoveItem(SelectedItem, Bag, Equipment);
            AudioSource.PlayOneShot(EquipSound);
        }

        public void Remove()
        {
            MoveItem(SelectedItem, Equipment, Bag);
            SelectItem(Equipment.Items.FirstOrDefault(i => i.Id == SelectedItem) ?? Bag.Items.Single(i => i.Id == SelectedItem));
            AudioSource.PlayOneShot(RemoveSound);
        }

        public override void Refresh()
        {
            if (SelectedItem == ItemId.Undefined)
            {
                ItemInfo.Reset();
                EquipButton.interactable = RemoveButton.interactable = false;
            }
            else
            {
                if (CanEquip())
                {
                    EquipButton.interactable = Bag.Items.Any(i => i.Id == SelectedItem)
                        && Equipment.Slots.Count(i => i.ItemType == SelectedItemParams.Type) > Equipment.Items.Count(i => i.Id == SelectedItem);
                    RemoveButton.interactable = Equipment.Items.Any(i => i.Id == SelectedItem);
                    ItemInfo.Price.enabled = !SelectedItemParams.Tags.Contains(ItemTag.NotForSale);
                }
                else
                {
                    EquipButton.interactable = RemoveButton.interactable = false;
                }
            }
        }

        private bool CanEquip()
        {
            return Equipment.Slots.Any(i => i.ItemType == SelectedItemParams.Type && i.ItemTags.All(j => SelectedItemParams.Tags.Contains(j)));
        }

        /// <summary>
        /// Automatically removes items if target slot is busy.
        /// </summary>
        private void AutoRemove(ItemType itemType, int max)
        {
            var items = Equipment.Items.Where(i => i.Params.Type == itemType).ToList();
            long sum = 0;

            foreach (var p in items)
            {
                sum += p.Count;
            }

            if (sum == max)
            {
                MoveItem(items.LastOrDefault(i => i.Id != SelectedItem) ?? items.Last(), Equipment, Bag);
            }
        }
    }
}