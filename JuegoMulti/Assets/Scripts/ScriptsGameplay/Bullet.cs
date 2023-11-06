using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    

    public Rigidbody rb;
    public Vector3 dirr;
    public GameObject enemigo;
    public float bulletSpeed;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (enemigo)
        {
            transform.LookAt(enemigo.transform);

        }
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = dirr*bulletSpeed;
        
    }

     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag== "Zombie")
        {
            other.GetComponentInParent<Zombie>().Quitarvida(2);
          //  Destroy(other.gameObject.transform.parent.gameObject);
            Destroy(gameObject);
        }
    }
}
