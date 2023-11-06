using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [SerializeField] public float vidaActual;
    [SerializeField] GameObject player;
    [SerializeField] public float speed;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PersonajePrincipal");
        Destroy(gameObject, 25f);
        StartCoroutine(morir());
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    private void Move()
    {
        transform.position = new Vector3(Vector3.MoveTowards(transform.position, player.transform.position, speed*Time.deltaTime).x,0,Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime).z);
        transform.LookAt(player.transform);
    
    }

    public void Quitarvida(int dano)
    {
        vidaActual = vidaActual - dano;
        if (vidaActual<0)
        {
            GameObject.Find("Spawner").GetComponent<Spawner>().enemigosActuales -= 1;
            GameObject.Find("GameManager").GetComponent<GameManager>().añadirPuntos(100);
            Destroy(gameObject);
        }
    }

    IEnumerator morir()
    {
        yield return new WaitForSeconds(24);
        GameObject.Find("Spawner").GetComponent<Spawner>().enemigosActuales -= 1;
    }
}
