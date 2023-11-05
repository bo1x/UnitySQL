using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{

    [SerializeField] int vidas;
    [SerializeField] int puntos;
    public GameObject vidasUI;
    public GameObject puntosUI;


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
        puntos += 10;
        puntosUI.GetComponent<TextMeshProUGUI>().text = puntos.ToString();
    }

    public void añadirPuntos(int pointswapos)
    {
        puntos += pointswapos;
        puntosUI.GetComponent<TextMeshProUGUI>().text = puntos.ToString();
    }

    public int getScore()
    {
        return puntos;
    }


}
