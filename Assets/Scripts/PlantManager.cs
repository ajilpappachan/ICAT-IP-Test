using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantManager : MonoBehaviour
{
    public void CreatePlant(PlantObject plantObject)
    {
        StartCoroutine(PlantLife(plantObject));
    }

    IEnumerator PlantLife(PlantObject plant)
    {
        GameObject newPlant = Instantiate(plant.Dirt, this.transform.localPosition + this.transform.forward, Quaternion.identity);
        yield return new WaitForSeconds(10);
        Transform plantTransform = newPlant.transform;
        Destroy(newPlant);
        newPlant = Instantiate(plant.Plant, plantTransform.position, plantTransform.rotation);
        newPlant.GetComponent<PlantScript>().SetFruit(plant.Inventory);
    }

}