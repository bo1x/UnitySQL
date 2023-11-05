using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.

public class IniciarSesion : MonoBehaviour
{
    public GameObject usuario;
    public GameObject contraseña;

    public void LoginMethod()
    {
        StartCoroutine(Login());

    }

    IEnumerator Login()
    {
        string user = usuario.GetComponent<TextMeshProUGUI>().text;
        string pass = contraseña.GetComponent<TextMeshProUGUI>().text;
        user = user.Substring(0, user.Length - 1);
        pass = pass.Substring(0, pass.Length - 1);
        
        //user = "alex";
        //pass = "123";

        

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
        }
    }
}