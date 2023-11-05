using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 dirr;
    public GameObject enemigo;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = dirr;
        if (enemigo)
        {
            transform.LookAt(enemigo.transform);
        }
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag== "Zombie")
        {
            Destroy(other.gameObject.transform.parent.gameObject);
            Destroy(transform.gameObject);
        }
    }
}
