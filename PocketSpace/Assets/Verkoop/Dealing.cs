using System.Collections.Generic;
using UnityEngine;

public class Dealing : MonoBehaviour
{
    public struct Deal
    {
        public string type;
        public string item;
        public int amount;
        public int reward;
    }

    public static Deal generate_deal(int items_per_second)
    {
        Dictionary<string, int> values = new Dictionary<string, int>()
        {
            {"Zuurstof", 4},
            {"Brandstof", 6},
            {"Maansteen", 5},
            {"Sterrenstof", 7}
        };
        
        Deal deal = new Deal();
        List<string> items;
        
        if(Random.Range(0,2) == 0) 
        {
            deal.type = "Astronaut";
            items = new List<string>{"Zuurstof", "Brandstof"};
        } else 
        {
            deal.type = "Researcher";
            items = new List<string>{"Maansteen", "Sterrenstof"};
        }
        deal.item = items[Random.Range(0,2)];

        int amount = items_per_second * Random.Range(3,6);
        int worth;
        values.TryGetValue(deal.item, out worth);

        deal.amount = amount;
        deal.reward = amount * worth;
        return deal;
    }
}