using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Singleton
    public static Inventory instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public delegate void ItemChange();
    public ItemChange itemChange;
    public int slotAmount = 11;
    public List<Item> items = new List<Item>();
    public void AddItem(Item _item)
    {
        if (items.Count < slotAmount)
        {
            items.Add(_item);
            if (itemChange!=null)
                itemChange.Invoke();
        }
    }
    public void RemoveItem(int num)
    {
        items.RemoveAt(num);
        itemChange.Invoke();
    }
}
