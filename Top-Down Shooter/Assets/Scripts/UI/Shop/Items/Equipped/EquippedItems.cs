using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItems : MonoBehaviour
{
    public GameObject itemPrefab;
    public List<GameObject> items;

    private void Start()
    {
        var shop = FindObjectOfType<Shop>();
        items = new List<GameObject>(shop.maxEquippedItems);
    }

    public void AddItemToInventory(Sprite sprite)
    {
        var item = CreateItem(sprite);
        items.Add(item);
    }

    public GameObject CreateItem(Sprite sprite)
    {
        GameObject newItemGO = Instantiate(itemPrefab);
        newItemGO.transform.SetParent(transform, false);
        var item = newItemGO.GetComponent<EquippedItem>();
        item.CreateItem(sprite);

        return newItemGO;
    }
}
