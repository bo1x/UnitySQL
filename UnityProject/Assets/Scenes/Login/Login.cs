using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Login : MonoBehaviour
{
    [SerializeField] private GameObject _LoginUI;
    [SerializeField] private GameObject _RegisterUI;

    public void register()
    {
        _LoginUI.SetActive(false);
        _RegisterUI.SetActive(true);
    }

    public void login()
    {
        _RegisterUI.SetActive(false);
        _LoginUI.SetActive(true);
    }
}
