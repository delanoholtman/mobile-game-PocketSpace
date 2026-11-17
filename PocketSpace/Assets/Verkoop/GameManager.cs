using TMPro;
using UnityEngine;
using System.Collections;


public class GameManager : MonoBehaviour
{
    public GameObject money_ui;
    public GameObject items_ui;
    public GameObject type;
    public GameObject item;
    public GameObject amount;
    public GameObject reward;
    public GameObject astronaut;
    public GameObject researcher;
    public GameObject type1;
    public GameObject item1;
    public GameObject amount1;
    public GameObject reward1;
    public GameObject astronaut1;
    public GameObject researcher1;
    public GameObject meteor;
    Dealing.Deal deal;
    Dealing.Deal deal1;
    public static GameManager instance;
    public Inventory inventory;

    private float timer;

    void Awake()
    {
        instance = this;
        if (inventory == null) 
        {
            inventory = GetComponent<Inventory>();
        }
    }

    void Start()
    {
        Application.targetFrameRate = 90;
        QualitySettings.vSyncCount = 0;

        astronaut.SetActive(false);
        researcher.SetActive(false);
        astronaut1.SetActive(false);
        researcher1.SetActive(false);
        deal = Dealing.generate_deal(1 * ((inventory.upgrades["Zuurstof"] + inventory.upgrades["Brandstof"] + inventory.upgrades["Maansteen"] + inventory.upgrades["Sterrenstof"])/4));
        deal1 = Dealing.generate_deal(1 * ((inventory.upgrades["Zuurstof"] + inventory.upgrades["Brandstof"] + inventory.upgrades["Maansteen"] + inventory.upgrades["Sterrenstof"])/4));
    }


    void Update()
    {
        string money_text = "" + inventory.money;
        money_ui.GetComponent<TMP_Text>().text = money_text;
        items_ui.GetComponent<TMP_Text>().text = $"Z{inventory.items["Zuurstof"]}, B{inventory.items["Brandstof"]}, M{inventory.items["Maansteen"]}, S{inventory.items["Sterrenstof"]}";

        if (deal.type == "Astronaut")
        {
            astronaut.SetActive(true);
            researcher.SetActive(false);
        } else 
        {
            astronaut.SetActive(false);
            researcher.SetActive(true);
        }
        type.GetComponent<TMP_Text>().text = deal.type;
        item.GetComponent<TMP_Text>().text = deal.item;
        amount.GetComponent<TMP_Text>().text = $"wilt: {deal.amount}";
        reward.GetComponent<TMP_Text>().text = $"reward: {deal.reward}";

        if (deal1.type == "Astronaut")
        {
            astronaut1.SetActive(true);
            researcher1.SetActive(false);
        } else 
        {
            astronaut1.SetActive(false);
            researcher1.SetActive(true);
        }
        type1.GetComponent<TMP_Text>().text = deal1.type;
        item1.GetComponent<TMP_Text>().text = deal1.item;
        amount1.GetComponent<TMP_Text>().text = $"wilt: {deal1.amount}";
        reward1.GetComponent<TMP_Text>().text = $"reward: {deal1.reward}";
    }

    public void AstroidHit(GameObject hit)
    {
        if (hit == null) return;
        if (hit.tag == "Oxygen") 
        { inventory.items["Zuurstof"] += inventory.upgrades["Zuurstof"]; 
            meteor.GetComponent<TMP_Text>().text = $"Zuurstof :{inventory.items["Zuurstof"]}";}
        else if (hit.tag == "Fuel") { inventory.items["Brandstof"] += inventory.upgrades["Brandstof"]; 
            meteor.GetComponent<TMP_Text>().text = $"Brandstof :{inventory.items["Brandstof"]}";}
        else if (hit.tag == "Mineral") { inventory.items["Maansteen"] += inventory.upgrades["Maansteen"]; 
            meteor.GetComponent<TMP_Text>().text = $"Maansteen :{inventory.items["Maansteen"]}";}
        else if (hit.tag == "Stardust") { inventory.items["Sterrenstof"] += inventory.upgrades["Sterrenstof"]; 
            meteor.GetComponent<TMP_Text>().text = $"Sterrenstof :{inventory.items["Sterrenstof"]}";}
        Destroy(hit);
    }


    public void BuyPressed(int deal_number)
    {
        timer += Time.deltaTime;
        if(timer >= 0.175f)
        {
            if (inventory.items[deal.item] >= deal.amount && deal_number == 1)
            {
                inventory.items[deal.item] -= deal.amount;
                inventory.money += deal.reward;
                deal = Dealing.generate_deal(1 * inventory.upgrades[deal.item]);
            }
            else if (inventory.items[deal1.item] >= deal1.amount && deal_number == 2)
            {
                inventory.items[deal1.item] -= deal1.amount;
                inventory.money += deal1.reward;
                deal1 = Dealing.generate_deal(1 * inventory.upgrades[deal1.item]);
            }
            timer = 0f;
        }
    }

    public void RejectPressed(int deal_number)
    {
        timer += Time.deltaTime;

        if (timer >= 0.175f)
        {
            if( deal_number == 1)
            {
                deal = Dealing.generate_deal(1 * inventory.upgrades[deal.item]);
            }
            else
            {
                deal1 = Dealing.generate_deal(1 * inventory.upgrades[deal1.item]);
            }
            timer = 0f;
        }
    }
}
