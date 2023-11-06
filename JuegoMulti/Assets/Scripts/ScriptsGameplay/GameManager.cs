using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

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
        vidasUI.GetComponent<TextMeshProUGUI>().text = vidas.ToString();

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

    public void QuitarVidaPlayer()
    {
        vidas -= 1;
        vidasUI.GetComponent<TextMeshProUGUI>().text = vidas.ToString();
        if (vidas <= 0)
        {
            PlayerPrefs.SetString("Puntuacion", puntos.ToString());
            Debug.Log("ded");
            SceneManager.LoadScene(2);
        }
        else
        {
            Debug.Log("-1");
        }
    }

}
