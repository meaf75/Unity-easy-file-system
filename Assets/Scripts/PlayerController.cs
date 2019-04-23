using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Boundary {
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour {

    [Header("Player info")]
    public int life = 100;
    public int coins = 0;
    public float moveSpeed = 10f;
    public float turnSpeed = 50f;

    public List<int> Inventory = new List<int>();

    [Header("UI References")]
    public Text txt_life;
    public Text txt_coins;

    [Header("Objects references")]
    public GameObject inventoryPanel;



    private void Start() {
        txt_life.text = "Vida: " + life;
        txt_coins.text = "Monedas: " + coins;
    }


    void Update() {
        
        if (Input.GetKey(KeyCode.UpArrow))
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.DownArrow))
            transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftArrow)) {
            //objeto.accion(no se, velocidad)
            transform.Rotate(Vector3.up, -turnSpeed * 0.16f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
            transform.Rotate(Vector3.up, turnSpeed * 0.16f);

        if (Input.GetKeyDown(KeyCode.KeypadMinus)) {
            setLife(life -= 10);            
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus)) {            
            setLife(life += 10);
        }

        if (Input.GetKeyDown(KeyCode.I)) {
            inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        }

    }

    public void setLife(int lifeTo) {
        life = lifeTo;
        txt_life.text = "Vida: " + lifeTo;
    }

    public void setCoin(int coinNumber) {
        coins = coinNumber;
        txt_coins.text = "Monedas: " + coins;
    }

    public void addCoin() {
        coins++;
        txt_coins.text = "Monedas: " + coins;
    }

}