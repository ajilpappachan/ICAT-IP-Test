using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoScript : MonoBehaviour
{
    public Transform center;
    public float pullStrength = 500.0f;
    public float refreshRate = 2f;
    public float moveSpeed = 10.0f;

    Vector3 destination;

    public void Start()
    {
        float x, z;
        if(transform.position.x < 0)
        {
            x = Random.Range(200.0f, 300.0f);
        }
        else
        {
            x = Random.Range(-300.0f, -200.0f);
        }

        if(transform.position.z < 0)
        {
            z = Random.Range(200.0f, 300.0f);
        }
        else
        {
            z = Random.Range(-300.0f, -200.0f);
        }

        destination = new Vector3(x, 0.0f, z);
    }

    public void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Pullable")
        {
            StartCoroutine(pullObject(other, true));
        }
        
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pullable")
        {
            StartCoroutine(pullObject(other, false));
        }
    }

    IEnumerator pullObject(Collider other, bool shouldPull)
    {
        if(shouldPull)
        {
            Vector3 forceDir = center.position - other.transform.position;
            other.GetComponent<Rigidbody>().AddForce(forceDir.normalized * pullStrength * Time.deltaTime);
            yield return refreshRate;
            StartCoroutine(pullObject(other, shouldPull));
        }
    }
}
