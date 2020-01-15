using System.Linq;
using Assets.FantasyInventory.Scripts.Data;
using Assets.FantasyInventory.Scripts.Enums;
using UnityEngine;

namespace Assets.FantasyInventory.Scripts.Interface.Elements
{
    /// <summary>
    /// Abstract item workspace. It can be shop or player inventory. Items can be managed here (selected, moved and so on).
    /// </summary>
    public abstract class ItemWorkspace : MonoBehaviour
    {
        public ItemInfo ItemInfo;

        protected ItemId SelectedItem;
        protected ItemParams SelectedItemParams;

        public abstract void Refresh();

        protected void Reset()
        {
            SelectedItem = ItemId.Undefined;
            ItemInfo.Reset();
        }

        protected void MoveItem(Item item, ItemContainer from, ItemContainer to)
        {
            MoveItem(item.Id, from, to);
        }

        protected void MoveItem(ItemId id, ItemContainer from, ItemContainer to)
        {
            if (to.Expanded)
            {
                to.Items.Add(new Item(id, 1));
            }
            else
            {
                var target = to.Items.SingleOrDefault(i => i.Id == id);

                if (target == null)
                {
                    to.Items.Add(new Item(id, 1));
                }
                else
                {
                    target.Count++;
                }
            }

            if (from.Expanded)
            {
                from.Items.Remove(from.Items.Last(i => i.Id == id));
            }
            else
            {
                var target = from.Items.Single(i => i.Id == id);

                if (target.Count > 1)
                {
                    target.Count--;
                }
                else
                {
                    from.Items.Remove(target);
                }
            }

            Refresh();
            from.Refresh();
            to.Refresh();
        }
    }
}