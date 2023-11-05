using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

// UnityWebRequest.Get example

// Access a website and use UnityWebRequest.Get to download a page.
// Also try to download a non-existing page. Display the error.

public class getweb : MonoBehaviour
{
    void Start()
    {
        // A correct website page.
        //StartCoroutine(GetRequest("http://localhost/ejemplosclase/login.php"));
        // StartCoroutine(Login("admin", "admin"));
        StartCoroutine(CrearUsuario("alex", "123", "1"));
        // A non-existing page.
     //   StartCoroutine(GetRequest("https://error.html"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            switch (webRequest.result)
            {
                case UnityWebRequest.Result.ConnectionError:
                case UnityWebRequest.Result.DataProcessingError:
                    Debug.LogError(pages[page] + ": Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.ProtocolError:
                    Debug.LogError(pages[page] + ": HTTP Error: " + webRequest.error);
                    break;
                case UnityWebRequest.Result.Success:
                    Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                    break;
            }
        }
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
        }
    }

    IEnumerator CrearUsuario(string user, string pass,string admin)
    {
        WWWForm form = new WWWForm();
        form.AddField("crearUsuario", user);
        form.AddField("crearPass", pass);
        form.AddField("crearAdmin", admin);

        UnityWebRequest www = UnityWebRequest.Post("http://localhost/ejemplosClase/crearUsuario.php", form);
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