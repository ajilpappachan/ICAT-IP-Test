using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SerializeField]
[CreateAssetMenu(fileName ="Plant", menuName ="Scriptables/Plant", order =1)]
public class PlantObject : ScriptableObject
{
    public GameObject Dirt;
    public GameObject Plant;
    public GameObject Fruit;
    public GameObject Inventory;
}
