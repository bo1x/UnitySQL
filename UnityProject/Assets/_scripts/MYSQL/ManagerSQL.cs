using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.UI;


public class ManagerSQL : MonoBehaviour
{

    /*
     * 400 - No puede establecer conexion
     * 401 - No encontro datos
     * 402 - El usuario ya existe
     * 
     * 200 - Datos encontrados 
     * 201 - Usuario Registrado
     */

    [SerializeField] private TMP_InputField _LoginUserBox;
    [SerializeField] private TMP_InputField _LoginPassBox;
    [SerializeField] private TMP_Text _LoginErrorText;

    [SerializeField] private TMP_InputField _RegisterUserBox;
    [SerializeField] private TMP_InputField _RegisterPassBox;
    [SerializeField] private TMP_InputField _RegisterPassBoxAgain;
    [SerializeField] private TMP_Text _RegisterErrorText;

    [SerializeField] private RectTransform Done;
    [SerializeField] private float DoneTime;
    private float doneStart;
    private float StartTime;

    Coroutine _crconexion;
    Coroutine _crregister;

    private void Start()
    {
        doneStart = Done.localPosition.y;
    }

    public void loginbutton()
    {
        AudioManager.Instance.Play("Click");
        _crconexion = StartCoroutine(Login());
    }

    public void registerbutton()
    {
        AudioManager.Instance.Play("Click");
        _crregister = StartCoroutine(Register());
    }


    IEnumerator Login()
    {

        WWWForm form = new WWWForm();

        string user = _LoginUserBox.text;
        string pass = _LoginPassBox.text;

        form.AddField("uss", user);
        form.AddField("pss", pass);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/VampireStats/login.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            StopCoroutine(_crconexion);
        }
        string[] datos = www.downloadHandler.text.Split('|');
        switch (Convert.ToInt32(datos[0]))
        {
            case 200:
                GameManager.Instance.SetDataUser(new UserData(Convert.ToInt32(datos[1]), datos[2], Convert.ToInt32(datos[3])));
                SceneManagarGame.Instance.NextScene(1);
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

    IEnumerator Register()
    {
        if (_RegisterPassBox.text != _RegisterPassBoxAgain.text)
        {
            _RegisterErrorText.gameObject.SetActive(true);
            _RegisterErrorText.text = "No coincide la contraseña";
            StopCoroutine(_crregister);
        }

        WWWForm form = new WWWForm();

        string user = _RegisterUserBox.text;
        string pass = _RegisterPassBox.text;

        form.AddField("uss", user);
        form.AddField("pss", pass);

        UnityWebRequest www = UnityWebRequest.Post("https://carlosfern.xiscosoft.es/php/register.php", form);
        yield return www.SendWebRequest();
        string[] datos = www.downloadHandler.text.Split('|');

        if (www.result != UnityWebRequest.Result.Success)
        {
            StopCoroutine(_crconexion);
        }
        switch (Convert.ToInt32(datos[0]))
        {
            case 402:
                _RegisterErrorText.gameObject.SetActive(true);
                _RegisterErrorText.text = "Error 402: Usuario ya existente";
                break;
            case 201:
                Done.localPosition = new Vector2(Done.localPosition.x, doneStart);
                Done.gameObject.SetActive(true);
                StartTime = Time.time;
                AudioManager.Instance.Play("Notify");
                while (!AccountCreated(0, +206))
                    yield return null;

                yield return new WaitForSeconds(3);

                StartTime = Time.time;
                while (!AccountCreated(206, -206))
                    yield return null;

                Done.gameObject.SetActive(false);

                break;
        }
    }

    private bool AccountCreated(float startposition, float target)
    {
        float animTime = Time.time - StartTime;
       float newPosition = Mathf.Lerp(0, target, animTime / DoneTime);
        Debug.Log(newPosition);
        Done.localPosition = new Vector2(Done.localPosition.x, (doneStart + startposition) + newPosition);
        if (animTime >= DoneTime)
            return true;
        return false;
    }
}
