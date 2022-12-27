using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItems : MonoBehaviour
{
    public List<GameObject> itemPrefabs = new ();
    public List<GameObject> itemSlots = new (4);
    public GameObject itemSlotPrefab;

    private void Start()
    {
        AddItemsToShop();
        var itemSlot = itemSlots[Random.Range(0, itemSlots.Count)].GetComponent<ItemSlot>();
        itemSlot.BuyItem(false);
    }

    public void AddItemsToShop()
    {
        for (int i = 0; i < itemSlots.Capacity; i++)
        {
            CreateItemSlot();
        }
    }

    public void CreateItemSlot()
    {
        GameObject newSlotGO = Instantiate (itemSlotPrefab);
        newSlotGO.transform.SetParent(transform, false);

        var weaponGO = itemPrefabs[Random.Range(0, itemPrefabs.Count)];
        var itemSlot = newSlotGO.GetComponent<ItemSlot>();
        itemSlot.CreateItem(weaponGO);
       
        itemSlots.Add(newSlotGO);
    }
}
