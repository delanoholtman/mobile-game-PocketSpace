using TMPro;
using Unity.Mathematics;
using UnityEngine;


public class UpgradeScript : MonoBehaviour
{
    // om te testen heb ik de MoneyScript vervangen met 'money' dus mischien kan het zo zijn dat je die ook moet aanpassen in de andere functies
    public Inventory inventory; //script die regelt hoeveel items een asteroid heeft
    public GameObject FuelText; // Text waar de upgrade cost word displayed
    public GameObject OxygenText;
    public GameObject MineralsText;
    public GameObject StardustText;

    public GameObject money;

    float price_multiplier = 1.1f;
    float upgrade_multiplier = 1.02f;

    void UpdateUi()
    {
        money.GetComponent<TextMeshProUGUI>().text = $"Money: {inventory.money}";
        FuelText.GetComponent<TextMeshProUGUI>().text = $"Upgrade Cost: {inventory.upgrade_cost["Brandstof"]}";
        OxygenText.GetComponent<TextMeshProUGUI>().text = $"Upgrade Cost: {inventory.upgrade_cost["Zuurstof"]}";
        MineralsText.GetComponent<TextMeshProUGUI>().text = $"Upgrade Cost: {inventory.upgrade_cost["Maansteen"]}";
        StardustText.GetComponent<TextMeshProUGUI>().text = $"Upgrade Cost: {inventory.upgrade_cost["Sterrenstof"]}";
    }

    void Start()
    {
        UpdateUi();  
    }
    public void FuelUpgradeButton() // player upgrades fuel
    {
        if (inventory.money >= inventory.upgrade_cost["Brandstof"])
        {
            inventory.upgrades["Brandstof"] = (int)math.ceil((float)inventory.upgrades["Brandstof"] * upgrade_multiplier); //+1 item per asteroid
            inventory.money -= inventory.upgrade_cost["Brandstof"]; //haalt geld af van de player
            inventory.upgrade_cost["Brandstof"] = (int)math.ceil((float)inventory.upgrade_cost["Brandstof"] * price_multiplier); //verhoogt de kosten van de upgrade
            UpdateUi();
        }
    }

    public void OxygenUpgradeButton() // player upgrades oxygen
    {
        if (inventory.money >= inventory.upgrade_cost["Zuurstof"])
        {
            inventory.upgrades["Zuurstof"] = (int)math.ceil((float)inventory.upgrades["Zuurstof"] * upgrade_multiplier); //+1 item per asteroid
            inventory.money -= inventory.upgrade_cost["Zuurstof"]; //haalt geld af van de player
            inventory.upgrade_cost["Zuurstof"] = (int)math.ceil((float)inventory.upgrade_cost["Zuurstof"] * price_multiplier); //verhoogt de kosten van de upgrade
            UpdateUi();
        }
    }

    public void MineralsUpgradeButton() // player upgrades minerals
    {
        if (inventory.money >= inventory.upgrade_cost["Maansteen"])
        {
            inventory.upgrades["Maansteen"] = (int)math.ceil((float)inventory.upgrades["Maansteen"] * upgrade_multiplier); //+1 item per asteroid
            inventory.money -= inventory.upgrade_cost["Maansteen"]; //haalt geld af van de player
            inventory.upgrade_cost["Maansteen"] = (int)math.ceil((float)inventory.upgrade_cost["Maansteen"] * price_multiplier); //verhoogt de kosten van de upgrade
            UpdateUi();
        }
    }

    public void StardustUpgradeButton() // player upgrades stardust
    {
        if (inventory.money >= inventory.upgrade_cost["Sterrenstof"])
        {
            inventory.upgrades["Sterrenstof"] = (int)math.ceil((float)inventory.upgrades["Sterrenstof"] * upgrade_multiplier); //+1 item per asteroid
            inventory.money -= inventory.upgrade_cost["Sterrenstof"]; //haalt geld af van de player
            inventory.upgrade_cost["Sterrenstof"] = (int)math.ceil((float)inventory.upgrade_cost["Sterrenstof"] * price_multiplier); //verhoogt de kosten van de upgrade
            UpdateUi();
        }
    }
}
