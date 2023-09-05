using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ItemManager : MonoBehaviour
{
    public Transform goal;
    public GameObject fieldItemPreFab;
    public int itemCount;
    public Vector3[] pos;
    List<Item> itemDB; 
    void Start()
    {
        itemDB = ItemDatabase.instance.itemDB;
        for (int i = 0; i < 3; i++)
        {
            GameObject go = Instantiate(fieldItemPreFab, pos[i], Quaternion.identity, goal);
            go.GetComponent<FieldItem>().SetItem(itemDB[Random.Range(0, 14)]);
        }
    }
    void Update()
    {
        
    }
}
