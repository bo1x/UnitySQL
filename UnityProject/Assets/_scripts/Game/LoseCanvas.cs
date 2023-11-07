using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class LoseCanvas : MonoBehaviour
{
    [SerializeField] private GameObject Canvas;

    [SerializeField] private TMP_Text scoretext;
    [SerializeField] private TMP_Text bestscore;
    [SerializeField] private TMP_Text worldrecord;
    [SerializeField] private TMP_Text _LoginErrorText;

    Coroutine _crconexion;

    private void Start()
    {
        GameManager.Instance.SetLoseCanvas(this);
    }

    private void OnEnable()
    {
        GameManager.Instance.SetLoseCanvas(this);
    }

    private void OnDisable()
    {
        GameManager.Instance.SetLoseCanvas(null);
    }

    public void gameover()
    {
        Canvas.SetActive(true);
        UpdatePoints();
    }



    public void UpdatePoints()
    {
        if(GameManager.Instance.GetPoints() > GameManager.Instance.GetDataUser().BestScore)
        {
            Debug.Log("Test");
            _crconexion = StartCoroutine(SetPoints());
        }

        scoretext.text = GameManager.Instance.GetPoints().ToString();
        bestscore.text = GameManager.Instance.GetDataUser().BestScore.ToString();
        StartCoroutine(GetHighscore());
    }

    public void Retry()
    {
        Canvas.SetActive(false);
        GameManager.Instance.ResetGame();
    }

    public void Logout()
    {
        SceneManagarGame.Instance.NextScene(0);
    }

    IEnumerator GetHighscore()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://localhost/VampireStats/HighScore.php");
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            StopCoroutine(_crconexion);
        }
        string[] datos = www.downloadHandler.text.Split('|');
        switch (Convert.ToInt32(datos[0]))
        {
            case 200:
                worldrecord.text = datos[1];
                break;
            case 400:
                _LoginErrorText.gameObject.SetActive(true);
                _LoginErrorText.text = "Error 400: No conexion";
                break;
            default:
                _LoginErrorText.gameObject.SetActive(true);
                _LoginErrorText.text = "Error 400: No conexion";
                break;
        }
    }

    IEnumerator SetPoints()
    {
        WWWForm form = new WWWForm();

        string user = GameManager.Instance.GetDataUser().user;
        int id = GameManager.Instance.GetDataUser().id;
        int sc = GameManager.Instance.GetPoints();

        form.AddField("uss", user);
        form.AddField("id", id);
        form.AddField("sc", sc);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/VampireStats/setpoints.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            StopCoroutine(_crconexion);
        }
        string[] datos = www.downloadHandler.text.Split('|');
        switch (Convert.ToInt32(datos[0]))
        {
            case 200:
                GameManager.Instance.SetDataUser(new UserData(GameManager.Instance.GetDataUser().id, GameManager.Instance.GetDataUser().user, GameManager.Instance.GetPoints()));
                bestscore.text = GameManager.Instance.GetDataUser().BestScore.ToString();
                StartCoroutine(GetHighscore());
                break;
            case 400:
                _LoginErrorText.gameObject.SetActive(true);
                _LoginErrorText.text = "Error 400: No conexion";
                break;
            case 401:
                _LoginErrorText.gameObject.SetActive(true);
                _LoginErrorText.text = "Error 401: Datos Incorrectos";
                break;
            default:
                _LoginErrorText.gameObject.SetActive(true);
                _LoginErrorText.text = "Error 400: No conexion";
                break;
        }
    }
}
