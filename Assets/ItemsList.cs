using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsList : MonoBehaviour {

    public List<Item> items = new List<Item>();

    [System.Serializable]
    public class Item{
        public string name;
        public int id;
        public Sprite image;        
    }
}
