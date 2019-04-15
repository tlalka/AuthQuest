﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory1 : MonoBehaviour
{
    private const int SLOTS = 1;

    private List<IInventoryItem> nItems = new List<IInventoryItem>();

    public event EventHandler<InventoryEventArgs> ItemAdded;

    public void AddItem(IInventoryItem item)
    {
        // Debug.Log("AddItemIsworking"); not reaching this point of code
        if (nItems.Count < SLOTS)
        {
            Collider2D collider = (item as MonoBehaviour).GetComponent<Collider2D>();
            if (collider.enabled)
            {
                collider.enabled = false;

                nItems.Add(item);

                item.OnPickup();

                if (ItemAdded != null)
                {
                    ItemAdded(this, new InventoryEventArgs(item));
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}