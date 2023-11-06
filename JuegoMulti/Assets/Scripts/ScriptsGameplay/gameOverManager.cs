using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class gameOverManager : MonoBehaviour
{
    private string usuario, score;
    private string bestUsuarioScore, bestUsuarioName;
    [SerializeField] GameObject usuario1score;
    [SerializeField] GameObject usuario1name;
    [SerializeField] GameObject BestScore;
    [SerializeField] GameObject BestName;
    // Start is called before the first frame update
    void Start()
    {
        usuario = PlayerPrefs.GetString("Usuario");
        score = PlayerPrefs.GetString("Puntuacion");
        usuario1score.GetComponent<TextMeshProUGUI>().text = score;
        usuario1name.GetComponent<TextMeshProUGUI>().text=usuario;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
