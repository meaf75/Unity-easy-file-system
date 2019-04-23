using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour {

    [Header("Gameobjects references")]
    public GameObject PlayerInvPanel;
    public GameObject ChestInvPanel;
    public GameObject btnItemPrefab;
    public GameObject TradePanel;

    [Header("Inventory info")]
    public List<int> ChestInventory;
    public List<int> PlayerInventory;

    ItemsList itemsList;

    private void Awake() {
        ChestInventory = FindObjectOfType<Chest>().Inventory;
        PlayerInventory = FindObjectOfType<PlayerController>().Inventory;
        itemsList = GetComponent<ItemsList>();
    }

    public void OpenChest() {

        TradePanel.SetActive(true);

        foreach (Transform item in PlayerInvPanel.transform) {
            Destroy(item.gameObject);
        }

        foreach (Transform item in ChestInvPanel.transform) {
            Destroy(item.gameObject);
        }

        // Set player objects
        foreach (int itemId in PlayerInventory) {
            ItemsList.Item item = itemsList.items.Find(x => x.id == itemId);

            if (item == null) {
                print("Item no encontrado");
            } else {
                GameObject gameOb = Instantiate(btnItemPrefab, PlayerInvPanel.transform);
                Image Btnimage = gameOb.GetComponent<Image>();
                gameOb.name = item.name;
                Btnimage.sprite = item.image;

                Button btn = gameOb.GetComponent<Button>();
                btn.onClick.AddListener(() => moveItemToChest(item.id, gameOb));
            }
        }

        // set chest objects
        foreach (int itemId in ChestInventory) {
            ItemsList.Item item = itemsList.items.Find(x => x.id == itemId);

            if (item == null) {
                print("Item no encontrado");
            } else {
                GameObject gameOb = Instantiate(btnItemPrefab, ChestInvPanel.transform);
                Image Btnimage = gameOb.GetComponent<Image>();
                gameOb.name = item.name;
                Btnimage.sprite = item.image;

                Button btn = gameOb.GetComponent<Button>();
                btn.onClick.AddListener(() => moveItemToPlayer(item.id, gameOb));
            }
        }
    }

    public void CloseChest() {
        TradePanel.SetActive(false);
    }

    public void closePlaner(GameObject gameObject) {
        gameObject.SetActive(false);
    }

    public void moveItemToPlayer(int Object_id, GameObject gameObjectMove) {
        // Fix array
        ChestInventory.Remove(Object_id);
        PlayerInventory.Add(Object_id);
        // Set new listener to button
        Button btn = gameObjectMove.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => moveItemToChest(Object_id, gameObjectMove));

        // Move object to player inventory
        gameObjectMove.transform.SetParent(PlayerInvPanel.transform);
    }

    public void moveItemToChest(int Object_id, GameObject gameObjectMove) {

        // Fix array
        PlayerInventory.Remove(Object_id);
        ChestInventory.Add(Object_id);
        // Set new listener to button
        Button btn = gameObjectMove.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => moveItemToPlayer(Object_id, gameObjectMove));
        

        // Move object to chest inventory
        gameObjectMove.transform.SetParent(ChestInvPanel.transform);
    }

}
