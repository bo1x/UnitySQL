using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponBehaviur : MonoBehaviour
{
    public List<GameObject> enemyList;
    private float dist;
    [SerializeField] float WeaponRange;
    private float RangoMinimo;
    private GameObject enemigoMasCercano;
    [SerializeField] GameObject Bala;
    private bool disparo;
    [SerializeField] float cadencia;
    // Start is called before the first frame update
    void Start()
    {
        disparo = true;  
    }

    // Update is called once per frame
    void Update()
    {

        dist = 1000f;
        foreach (GameObject enemigo in GameObject.FindGameObjectsWithTag("Zombie"))
        {
            enemyList.Add(enemigo);
            
        }
        
        for (int i = 0; i < enemyList.Count; i++)
        {
            
            if (dist>Vector3.Distance(transform.position, enemyList[i].transform.position))
            {
                dist = Vector3.Distance(transform.position, enemyList[i].transform.position);
                if (dist< WeaponRange)
                {
                   enemigoMasCercano = enemyList[i];
                }
            }
        }
        if (enemigoMasCercano)
        {
            transform.LookAt(enemigoMasCercano.transform);
            if (disparo)
            {
                StartCoroutine("shooot", enemigoMasCercano);
            }
        }
      
        enemyList.Clear();
        enemigoMasCercano = null;
    }

    public void shoot(GameObject enemigo)
    {
       GameObject coso =  Instantiate(Bala);
        coso.transform.position = transform.position;
        coso.GetComponent<Bullet>().dirr = enemigo.transform.position - transform.position;
        coso.GetComponent<Bullet>().enemigo = enemigo;
    }

    IEnumerator shooot(GameObject enemigo)
    {
        disparo = false;
        GameObject coso = Instantiate(Bala);
        coso.transform.position = transform.position;
        coso.GetComponent<Bullet>().dirr = enemigo.transform.position - transform.position;
        coso.GetComponent<Bullet>().enemigo = enemigo;
        Destroy(coso, 10f);

        yield return new WaitForSeconds(cadencia);
        disparo = true;
    }
}
