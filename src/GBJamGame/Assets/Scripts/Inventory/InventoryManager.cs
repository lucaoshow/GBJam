using Root.Interactions;
using System.Collections.Generic;

namespace Root.Inventory
{
    public class InventoryManager
    {
        private static InventoryManager instance;
        public static InventoryManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new InventoryManager();
                }

                return instance;
            }
        }

        public List<Interactable> items = new List<Interactable>();

        public void AddItem(Interactable item)
        {
            this.items.Add(item);
        }

        public void RemoveItem(Interactable item)
        {
            this.items.Remove(item);
        }

        public bool ItemInInventory(Interactable item)
        {
            return this.items.Contains(item);
        }

        public bool AnyItemsStored()
        {
            return this.items.Count > 0;
        }

        public void Clear()
        {
            this.items.Clear();
        }
    }
}
