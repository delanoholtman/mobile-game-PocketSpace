using UnityEngine;
using TMPro;

public class inventaris : MonoBehaviour
{
    public Inventory inventory;
    public GameObject zuurstof;
    public GameObject sterrenstof;
    public GameObject maansteen;
    public GameObject brandstof;
    void Update()
    {
        zuurstof.GetComponent<TMP_Text>().text = $"{inventory.items["Zuurstof"]}";
        sterrenstof.GetComponent<TMP_Text>().text = $"{inventory.items["Sterrenstof"]}";
        maansteen.GetComponent<TMP_Text>().text = $"{inventory.items["Maansteen"]}";
        brandstof.GetComponent<TMP_Text>().text = $"{inventory.items["Brandstof"]}";
    }
}