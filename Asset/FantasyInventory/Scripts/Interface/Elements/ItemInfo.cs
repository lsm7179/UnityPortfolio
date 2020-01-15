using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Assets.FantasyInventory.Scripts.Data;
using Assets.FantasyInventory.Scripts.Enums;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.FantasyInventory.Scripts.Interface.Elements
{
    /// <summary>
    /// Represents item when it was selected. Displays icon, name, price and properties.
    /// </summary>
    public class ItemInfo : MonoBehaviour
    {
        public Text Name;
        public Text Description;
        public Text Price;
        public Image Icon;

        public void Reset()
        {
            Name.text = Description.text = Price.text = null;
            Icon.sprite = ImageCollection.Instance.DefaultItemIcon;
        }

        public void Initialize(ItemId itemId, ItemParams itemParams, bool shop = false)
        {
            Icon.sprite = ImageCollection.Instance.GetIcon(itemId);
            Name.text = SplitName(itemId.ToString());
            Description.text = string.Format("Here will be {0} description soon...", itemId);

            if (itemParams.Tags.Contains(ItemTag.NotForSale))
            {
                Price.text = null;
            }
            else if (shop)
            {
                Price.text = string.Format("Buy price: {0}G{1}Sell price: {2}G", itemParams.Price, Environment.NewLine, itemParams.Price / Shop.SellRatio);
            }
            else
            {
                Price.text = string.Format("Sell price: {0}G", itemParams.Price / Shop.SellRatio);
            }

            var description = new List<string> { string.Format("Type: {0}", itemParams.Type) };

            if (itemParams.Tags.Any())
            {
                description[description.Count - 1] += string.Format(" <color=grey>[{0}]</color>", string.Join(", ", itemParams.Tags.Select(i => string.Format("{0}", i)).ToArray()));
            }

            foreach (var attribute in itemParams.Properties)
            {
                description.Add(string.Format("{0}: {1}", SplitName(attribute.Id.ToString()), attribute.Value));
            }

            Description.text = string.Join(Environment.NewLine, description.ToArray());
        }
        
        public static string SplitName(string name)
        {
            return Regex.Replace(Regex.Replace(name, "[A-Z]", " $0"), "([a-z])([1-9])", "$1 $2").Trim();
        }
    }
}