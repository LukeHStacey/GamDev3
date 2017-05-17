using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour {

    public static ItemManager Manager { get; private set; }
    [SerializeField]
    private Item[] Items;

    void Awake() {
        ItemManager.Manager = this;
    }

    public Item GetItem() {
        return Items[Random.Range(0, Items.Length)];
    }
}
