using System.Collections.Generic;
using Assets.FantasyInventory.Scripts.Data;
using Assets.FantasyInventory.Scripts.Enums;

namespace Assets.FantasyInventory.Scripts.GameData
{
    /// <summary>
    /// Item params are stored here. If you want to store them in any kind of database, please refer to dictionary serialization:
    /// https://docs.unity3d.com/ScriptReference/ISerializationCallbackReceiver.html
    /// </summary>
    public class Items
    {
        public static readonly Dictionary<ItemId, ItemParams> Params = new Dictionary<ItemId, ItemParams>
        {
            {
                ItemId.FireballScroll,
                new ItemParams
                {
                    Type = ItemType.Scroll,
                    Properties = new List<Property> { new Property(PropertyId.MagicDamage, 100) },
                    Price = 1000
                }
            },
            {
                ItemId.Flute,
                new ItemParams
                {
                    Type = ItemType.Loot,
                    Price = 10
                }
            },
            {
                ItemId.Gold,
                new ItemParams
                {
                    Type = ItemType.Currency,
                    Tags = new List<ItemTag> { ItemTag.NotForSale }
                }
            },
            {
                ItemId.HealthPotion,
                new ItemParams
                {
                    Type = ItemType.Potion,
                    Properties = new List<Property> { new Property(PropertyId.RestoreHealth, 50) },
                    Price = 200
                }
            },
            {
                ItemId.IronSword,
                new ItemParams
                {
                    Type = ItemType.Weapon,
                    Tags = new List<ItemTag> { ItemTag.Sword },
                    Properties = new List<Property> { new Property(PropertyId.PhysicDamage, 10) },
                    Price = 1000
                }
            },
            {
                ItemId.IvyBow,
                new ItemParams
                {
                    Type = ItemType.Weapon,
                    Tags = new List<ItemTag> { ItemTag.Bow, ItemTag.TwoHanded },
                    Properties = new List<Property> { new Property(PropertyId.PhysicDamage, 15) },
                    Price = 2000
                }
            },
            {
                ItemId.LeatherArmor,
                new ItemParams
                {
                    Type = ItemType.Armor,
                    Properties = new List<Property> { new Property(PropertyId.PhysicDefense, 10) },
                    Price = 1000
                }
            },
            {
                ItemId.LeatherHelmet,
                new ItemParams
                {
                    Type = ItemType.Helmet,
                    Properties = new List<Property> { new Property(PropertyId.PhysicDamage, 5) },
                    Price = 500
                }
            },
            {
                ItemId.ManaPotion,
                new ItemParams
                {
                    Type = ItemType.Potion,
                    Properties = new List<Property> { new Property(PropertyId.RestoreMana, 50) },
                    Price = 200
                }
            },
            {
                ItemId.RoundShield,
                new ItemParams
                {
                    Type = ItemType.Shield,
                    Properties = new List<Property> { new Property(PropertyId.PhysicDamage, 10) },
                    Price = 1000
                }
            },
            {
                ItemId.SilverRing,
                new ItemParams
                {
                    Type = ItemType.Ring,
                    Properties = new List<Property> { new Property(PropertyId.MagicDefense, 5) },
                    Price = 500
                }
            },
            {
                ItemId.Spear,
                new ItemParams
                {
                    Type = ItemType.Weapon,
                    Tags = new List<ItemTag> { ItemTag.Spear, ItemTag.TwoHanded },
                    Properties = new List<Property> { new Property(PropertyId.PhysicDamage, 15) },
                    Price = 2500
                }
            },
            {
                ItemId.StoneAmulet,
                new ItemParams
                {
                    Type = ItemType.Necklace,
                    Properties = new List<Property> { new Property(PropertyId.MagicDefense, 10) },
                    Price = 1000
                }
            },
            {
                ItemId.TwoHandedSword,
                new ItemParams
                {
                    Type = ItemType.Weapon,
                    Tags = new List<ItemTag> { ItemTag.Sword, ItemTag.TwoHanded },
                    Properties = new List<Property> { new Property(PropertyId.PhysicDamage, 20) },
                    Price = 5000
                }
            },
            {
                ItemId.WoodcutterAxe,
                new ItemParams
                {
                    Type = ItemType.Weapon,
                    Tags = new List<ItemTag> { ItemTag.Axe },
                    Properties = new List<Property> { new Property(PropertyId.PhysicDamage, 5) },
                    Price = 100
                }
            }
        };
    }
}