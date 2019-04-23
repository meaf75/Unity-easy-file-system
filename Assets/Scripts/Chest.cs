using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour {

    Animator anim;
    GameMaster gm;

    public List<int> Inventory = new List<int>();

    private void Awake() {
        gm = FindObjectOfType<GameMaster>();
        anim = GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if(other.tag == "Player") {
            anim.SetBool("open", true);
            gm.OpenChest();
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.tag == "Player") {
            anim.SetBool("open", false);
            anim.SetBool("close", true);
            gm.CloseChest();
        }
    }

    void ChestClosed() {
        anim.SetBool("close", false);
    }

}
