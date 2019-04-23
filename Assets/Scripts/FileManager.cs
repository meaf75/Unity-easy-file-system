using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using Newtonsoft.Json;

public class FileManager : MonoBehaviour {

    GameMaster gm;
    PlayerController player;

    string path;

    private void Awake() {
        gm = FindObjectOfType<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Start() {
        path = Path.Combine(Application.dataPath, "My_Files/MyConfigFile.json");
    }

    public void save() {

        PlayerController player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();

        GameInfo gameInfo = new GameInfo(player,gm.ChestInventory,gm.PlayerInventory);

        string texto = JsonConvert.SerializeObject(gameInfo, Formatting.Indented);

        string dir = Path.Combine(Application.dataPath, "My_Files");
        if (!Directory.Exists(dir)) {
            Directory.CreateDirectory(dir);
        }

        File.WriteAllText(path, texto);

        //Si se esta trabajando a traves de unity se actualiza el archivo
        #if UNITY_EDITOR
                UnityEditor.AssetDatabase.Refresh();    //Refresca los assets 
        #endif
    }

    public void load() {

        string dir = Path.Combine(Application.dataPath, "My_Files");
        if (!Directory.Exists(dir)) {
            Directory.CreateDirectory(dir);
        }

        if (!File.Exists(path)) {
            return;
        }

        string texto = File.ReadAllText(path);

        GameInfo infoLoad = JsonConvert.DeserializeObject<GameInfo>(texto);

        player.setLife(infoLoad.life);
        player.setCoin(infoLoad.coins);
        player.transform.position = infoLoad.pos;

        gm.ChestInventory = infoLoad.ChestInventory;
        gm.PlayerInventory = infoLoad.PlayerInventory;
    }

}

public class GameInfo {
    public int life = 100;
    public int coins = 0;
    public Vector3 pos;
    public List<int> ChestInventory;
    public List<int> PlayerInventory;

    public GameInfo() {

    }

    public GameInfo(PlayerController player, List<int> _chestInventory, List<int> _playerInventory) {
        this.life = player.life;
        this.coins = player.coins;
        this.pos = player.transform.position;
        this.ChestInventory = _chestInventory;
        this.PlayerInventory = _playerInventory;
    }
}



