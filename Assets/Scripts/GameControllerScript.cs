using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControllerScript : MonoBehaviour
{
    public GameObject PlantSelector;
    public GameObject Inventory;
    public GameObject InventoryList;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            PlantSelector.SetActive(!PlantSelector.activeSelf);
        }
        if(Input.GetKeyDown(KeyCode.I))
        {
            Inventory.SetActive(!Inventory.activeSelf);
        }
    }

    public void AddInventoryItem(GameObject item)
    {
        Instantiate(item, InventoryList.transform);
    }
}
