using System.Collections.Generic;
using Assets.FantasyInventory.Scripts.Data;
using UnityEngine;

namespace Assets.FantasyInventory.Scripts.Interface.Elements
{
    /// <summary>
    /// Abstract item container. It can be inventory bag, player equipment or trader goods.
    /// </summary>
    public abstract class ItemContainer : MonoBehaviour
    {
        /// <summary>
        /// List of items.
        /// </summary>
        public List<Item> Items { get; protected set; }

        /// <summary>
        /// Either all items are expanded (i.e. item count = 1, so two equal items will be stored as two list elements).
        /// </summary>
        public bool Expanded;

        public abstract void Refresh();

        public void Initialize(ref List<Item> items)
        {
            Items = items;
            Refresh();
        }
    }
}