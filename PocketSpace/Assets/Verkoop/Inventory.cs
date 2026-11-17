// oskar, inventory + autosave

using UnityEngine;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Collections;


public class Inventory : MonoBehaviour
{
    public int money = 0;

    public Dictionary<string, int> items = new Dictionary<string, int>
    {
        {"Zuurstof", 0},
        {"Brandstof", 0},
        {"Maansteen", 0},
        {"Sterrenstof", 0}
    };

    public Dictionary<string, int> upgrades = new Dictionary<string, int>
    {
        {"Zuurstof", 1},
        {"Brandstof", 1},
        {"Maansteen", 1},
        {"Sterrenstof", 1}
    };

    public Dictionary<string, int> upgrade_cost = new Dictionary<string, int>
    {
        {"Zuurstof", 10},
        {"Brandstof", 10},
        {"Maansteen", 10},
        {"Sterrenstof", 10}
    };

    void Awake()
    {
        Read();
        StartCoroutine(Saving());
    }

    IEnumerator Saving()
    {
        while(true)
        {
            Save();
            yield return new WaitForSeconds(1);
        }
    }

    void Save()
    {
        var inventory_component = GetComponent<Inventory>();
        string json = JsonConvert.SerializeObject(new { 
            money = inventory_component.money, 
            items = inventory_component.items,
            upgrades = inventory_component.upgrades,
            upgrade_cost = inventory_component.upgrade_cost
        });
        string Path = Application.persistentDataPath + "/save.json";
        File.WriteAllText(Path, json);
    }

    void Read()
    {
        string Path = Application.persistentDataPath + "/save.json";
        if (!File.Exists(Path)) return;

        string data = File.ReadAllText(Path);
        var template = new { money = 0, items = new Dictionary<string, int>(), upgrades = new Dictionary<string, int>(), upgrade_cost = new Dictionary<string, int>() };

        var lolventory = JsonConvert.DeserializeAnonymousType(data, template);

        var inventory_component = GetComponent<Inventory>();
        inventory_component.money = lolventory.money;
        inventory_component.items = lolventory.items;
        inventory_component.upgrades = lolventory.upgrades;
        inventory_component.upgrade_cost = lolventory.upgrade_cost;
    }

}
