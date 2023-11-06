using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using UnityEngine.SceneManagement;
using static UnityEngine.Rendering.DebugUI;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.

public class getweb : MonoBehaviour
{

    public GameObject usuario;
    public GameObject contraseña;
    public GameObject inputFieldError;
    string user, pass;
    string stringCheck;

    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetRequest("http://localhost/ejemplosclase/login.php"));
        // StartCoroutine(Login("admin", "admin"));
        // A non-existing page.
     //   StartCoroutine(GetRequest("https://error.html"));
    }

    public void LlamarLogin()
    {
        user = usuario.GetComponent<TMP_InputField>().text.ToString();
        pass = contraseña.GetComponent<TMP_InputField>().text.ToString();
        StartCoroutine(Login(user,pass));
        
    }
    public void LlamarCrearCuenta()
    {
        user = usuario.GetComponent<TMP_InputField>().text.ToString(); ;
        pass = contraseña.GetComponent<TMP_InputField>().text.ToString(); ;
        StartCoroutine(CrearUsuario(user, pass));
    }

    IEnumerator Login(string user,string pass)
    {

       

        WWWForm form = new WWWForm();
        form.AddField("loginUsuario", user);
        form.AddField("loginCont", pass);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/ejemplosClase/login.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
            
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            stringCheck = www.downloadHandler.text.ToString();
        }


        if (stringCheck.Contains("777"))
        {
            PlayerPrefs.SetString("Usuario", user);
            SceneManager.LoadScene(1);
        }
        if (stringCheck.Contains("666"))
        {
            inputFieldError.GetComponent<TextMeshProUGUI>().text = "Contraseña Erronea/Usuario";
        }
        if (stringCheck.Contains("555"))
        {
            inputFieldError.GetComponent<TextMeshProUGUI>().text = "Usuario Erroneo/Contraseña";
        }

    }

    IEnumerator CrearUsuario(string user, string pass)
    {

        

        WWWForm form = new WWWForm();
        form.AddField("crearUsuario", user);
        form.AddField("crearPass", pass);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/ejemplosClase/crearUsuario.php", form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log(www.downloadHandler.text);
            stringCheck = www.downloadHandler.text.ToString();
        }
        if (stringCheck.Contains("111"))
        {
         
            inputFieldError.GetComponent<TextMeshProUGUI>().text = "Usuario ya existe";
        }
        if (stringCheck.Contains("888"))
        {
            inputFieldError.GetComponent<TextMeshProUGUI>().text = "Usuario Creado";
        }
        Debug.Log("hello");
    }

    /*
     cREAR USER

111 Usuario Ya existente

888 Usuario Creado

Login 

777 lOGIN CORRECTO

666 CONTRASEÑA INCORRECTA

555 USUARIO NO EXISTE
     */
}