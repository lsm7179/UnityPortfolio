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
    /// High-level shop inverface.
    /// </summary>
    public class Shop : ItemWorkspace
    {
        public ScrollInventory Trader;
        public ScrollInventory Bag;
        public Button BuyButton;
        public Button SellButton;
        public AudioSource AudioSource;
        public AudioClip TradeSound;
        public AudioClip NoMoney;

        public const int SellRatio = 2;

        /// <summary>
        /// Initialize owned items and trader items (just for example).
        /// </summary>
        protected void Awake()
        {
            var inventory = new List<Item>
            {
                new Item(ItemId.Gold, 10000)
            };

            var shop = new List<Item>
            {
                new Item(ItemId.FireballScroll, 10),
                new Item(ItemId.Gold, 5000),
                new Item(ItemId.HealthPotion, 10),
                new Item(ItemId.IronSword, 1),
                new Item(ItemId.IvyBow, 1),
                new Item(ItemId.LeatherArmor, 1),
                new Item(ItemId.LeatherHelmet, 1),
                new Item(ItemId.ManaPotion, 10),
                new Item(ItemId.RoundShield, 1),
                new Item(ItemId.SilverRing, 2),
                new Item(ItemId.Spear, 1),
                new Item(ItemId.StoneAmulet, 1),
                new Item(ItemId.TwoHandedSword, 1),
                new Item(ItemId.WoodcutterAxe, 10)
            };

            Trader.Initialize(ref shop);
            Bag.Initialize(ref inventory);
        }

        protected void Start()
        {
            Reset();
            BuyButton.interactable = SellButton.interactable = false;

            // TODO: Assigning static callbacks. Don't forget to set null values when UI will be closed. You can also use events instead.
            InventoryItem.OnItemSelected = SelectItem;
            InventoryItem.OnDragStarted = SelectItem;
            InventoryItem.OnDragCompleted = InventoryItem.OnDoubleClick = item => { if (Trader.Items.Contains(item)) Buy(); else Sell(); };
        }

        public void SelectItem(Item item)
        {
            SelectItem(item.Id);
        }

        public void SelectItem(ItemId itemId)
        {
            SelectedItem = itemId;
            SelectedItemParams = Items.Params[itemId];
            ItemInfo.Initialize(SelectedItem, SelectedItemParams, true);
            Refresh();
        }

        public void Buy()
        {
            if (GetCurrency(Bag, ItemId.Gold) < SelectedItemParams.Price)
            {
                AudioSource.PlayOneShot(NoMoney);
                Debug.LogWarning("You haven't enought gold!");
                return;
            }

            AddMoney(Bag, -SelectedItemParams.Price, ItemId.Gold);
            AddMoney(Trader, SelectedItemParams.Price, ItemId.Gold);
            MoveItem(SelectedItem, Trader, Bag);
            AudioSource.PlayOneShot(TradeSound);
        }

        public void Sell()
        {
            if (GetCurrency(Trader, ItemId.Gold) < SelectedItemParams.Price / SellRatio)
            {
                AudioSource.PlayOneShot(NoMoney);
                Debug.LogWarning("Trader hasn't enought gold!");
                return;
            }

            AddMoney(Bag, SelectedItemParams.Price / SellRatio, ItemId.Gold);
            AddMoney(Trader, -SelectedItemParams.Price / SellRatio, ItemId.Gold);
            MoveItem(SelectedItem, Bag, Trader);
            AudioSource.PlayOneShot(TradeSound);
        }

        public override void Refresh()
        {
            if (SelectedItem == ItemId.Undefined)
            {
                ItemInfo.Reset();
                BuyButton.interactable = SellButton.interactable = false;
            }
            else
            {
                var item = Items.Params[SelectedItem];

                if (!item.Tags.Contains(ItemTag.NotForSale))
                {
                    BuyButton.interactable = Trader.Items.Any(i => i.Id == SelectedItem) && GetCurrency(Bag, ItemId.Gold) >= item.Price;
                    SellButton.interactable = Bag.Items.Any(i => i.Id == SelectedItem) && GetCurrency(Trader, ItemId.Gold) >= item.Price / SellRatio;
                }
                else
                {
                    ItemInfo.Price.text = null;
                    BuyButton.interactable = SellButton.interactable = false;
                }
            }
        }

        private static long GetCurrency(ItemContainer bag, ItemId currencyId)
        {
            var currency = bag.Items.SingleOrDefault(i => i.Id == currencyId);

            return currency == null ? 0 : currency.Count;
        }

        private static void AddMoney(ItemContainer inventory, int value, ItemId currencyId)
        {
            var currency = inventory.Items.SingleOrDefault(i => i.Id == currencyId);

            if (currency == null)
            {
                inventory.Items.Insert(0, new Item(currencyId, value));
            }
            else
            {
                currency.Count += value;

                if (currency.Count == 0)
                {
                    inventory.Items.Remove(currency);
                }
            }
        }
    }
}