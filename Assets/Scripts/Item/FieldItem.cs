using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldItem : MonoBehaviour
{
    public Item item;
    public SpriteRenderer image;
    public Image textImage;
    public Text text;
    public bool isTrigger;

    public void SetItem(Item _item)
    {
        item.itemName = _item.itemName;
        item.itemType = _item.itemType;
        item.itemImage = _item.itemImage;
        item.efts = _item.efts;
        image.sprite = item.itemImage;
    }
    public Item GetItem()
    {
        Inventory.instance.AddItem(item);
        item.Use();
        ItemDatabase.instance.isItemGet=true;
        return item;
    }
    void Update()
    {
        if (isTrigger && Input.GetButtonDown("Item"))
        {
            GetItem();
        }
        if (ItemDatabase.instance.isItemGet)
        {
            DestroyItem();
        }
    }
    public void DestroyItem()
    {
        Destroy(gameObject);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textImage.gameObject.SetActive(true);
            text.text = item.itemName + ":" + ItemDatabase.instance.itemInfo[item.itemName];
            isTrigger = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            textImage.gameObject.SetActive(false);
            isTrigger = false;
        }
    }
}
