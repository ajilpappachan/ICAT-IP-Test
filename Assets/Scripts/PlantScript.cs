using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantScript : MonoBehaviour
{
    GameObject fruit;
    GameControllerScript gameController;

    public void SetFruit(GameObject fruitObject)
    {
        fruit = fruitObject;
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameControllerScript>();
    }

    public void PickUp()
    {
        gameController.AddInventoryItem(fruit);
        Destroy(gameObject);
    }

}
