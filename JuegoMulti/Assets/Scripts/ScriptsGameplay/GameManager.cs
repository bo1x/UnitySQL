using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] int vidas;
    [SerializeField] int puntos;
    
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(time());
    }

    // Update is called once per frame
    void Update()
    {
     
    }
    IEnumerator time()
    {
        while (true)
        {
            timeCount();
            yield return new WaitForSeconds(1);
        }
    }

    void timeCount()
    {
        puntos += 1;
        Debug.Log(puntos);
    }




}
