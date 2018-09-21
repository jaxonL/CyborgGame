using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour {

    private Sprite sprite;

    private void Awake()
    {
        sprite = this.GetComponent<SpriteRenderer>().sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") {
            // Call Inventory Script
            InventoryController.inventoryController.AddInventoryItem(
                this.name, sprite
            );
            Destroy(gameObject);
        }
    }
}
