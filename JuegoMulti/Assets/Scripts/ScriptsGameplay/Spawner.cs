using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject zombi;
    [SerializeField] GameObject player;
    [SerializeField] public int enemigosActuales;
    [SerializeField] public int enemigosMaximos;
    [SerializeField] float VelocidadEnemigos;
    [SerializeField] int VidaEnemigos;
    [SerializeField] float distanciaminima;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        score = GameObject.Find("GameManager").GetComponent<GameManager>().getScore();

        enemigosMaximos = score / 100;
        if (enemigosMaximos > 150)
        {
            enemigosMaximos = 150;
        }
         
        VelocidadEnemigos = score / 500;
        VidaEnemigos = score / 1000;
        if (VelocidadEnemigos<1)
        {
            VelocidadEnemigos = 1;
        }
        if (VelocidadEnemigos > 4)
        {
            VelocidadEnemigos = 4;
        }
        if (VidaEnemigos < 1)
        {
            VidaEnemigos = 1;
        }
        
        if (enemigosActuales<enemigosMaximos)
        {
            for (int i = enemigosActuales; i <enemigosMaximos ; i++)
            {
                GameObject coso = Instantiate(zombi);
                coso.transform.position = RandomPosition();
                coso.GetComponent<Zombie>().vidaActual = VidaEnemigos;
                coso.GetComponent<Zombie>().speed = VelocidadEnemigos;
                enemigosActuales++;
            }
        }
    }

    public Vector3 RandomPosition()
    {
        Vector3 position = new Vector3();
        if (Random.value > 0.5)
        {
            if (Random.value<0.5)
            {
                position.x = Random.Range(player.transform.position.x - 20, player.transform.position.x + 20);
                position.z = player.transform.position.z + 20;
            }
            else
            {
                position.x = player.transform.position.x - 20;
                position.z = player.transform.position.z+ Random.Range(-10f, 10f);
            }
           
        }
        else
        {
            if(Random.value<0.5f)
            {
                position.x = player.transform.position.x + 20;
                position.z = player.transform.position.z + Random.Range(-10f,10f);
            }
            else
            {
                position.z = player.transform.position.z - 20;
                position.x = Random.Range(player.transform.position.x - 20, player.transform.position.x + 20);
            }
           
        }
        position.y = 0;


        return position;
    }
}
