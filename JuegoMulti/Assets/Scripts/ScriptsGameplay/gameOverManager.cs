using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using static UnityEditor.ShaderData;

public class gameOverManager : MonoBehaviour
{
    private string usuario, score;
    private string bestUsuarioScore, bestUsuarioName;
    [SerializeField] GameObject usuario1score;
    [SerializeField] GameObject usuario1name;
    [SerializeField] GameObject BestScore;
    [SerializeField] GameObject BestName;
    private string stringcheck;
    // Start is called before the first frame update
    void Start()
    {
        usuario = PlayerPrefs.GetString("Usuario");
        score = PlayerPrefs.GetString("Puntuacion");
        usuario1score.GetComponent<TextMeshProUGUI>().text = score;
        usuario1name.GetComponent<TextMeshProUGUI>().text=usuario;
        getBestScore();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void uploadScorewapardo()
    {
        StartCoroutine(UploadScore(usuario,score));
    }
    public void getBestScore()
    {
        StartCoroutine(getBestScore_());
    }

    IEnumerator getBestScore_()
    {
        
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/ejemplosClase/BajarDatosMejorScore.php");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);

        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            stringcheck = www.downloadHandler.text.ToString();
        }


        if (true)
        {
            BestScore.GetComponent<TextMeshProUGUI>().text = stringcheck;

        }
        

    }

    IEnumerator UploadScore(string user,string puntos)
    {



        WWWForm form = new WWWForm();
        form.AddField("crearUsuario", user);
        form.AddField("points", puntos);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/ejemplosClase/subirDatosScore.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            stringcheck = www.downloadHandler.text.ToString();
        }
      
        Debug.Log("hello");
    }

    public void volverLogin()
    {
        SceneManager.LoadScene(0);
    }
}
