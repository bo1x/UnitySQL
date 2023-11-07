using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowPassword : Switcher
{
    bool Show;
    [SerializeField]private TMP_InputField Pass;
    [SerializeField] private Sprite ShowImg;
    [SerializeField] private Sprite HideImg;

    private void OnEnable()
    {
        _Image.sprite = HideImg;
        Pass.contentType = TMP_InputField.ContentType.Password;
        ButtonOff();
    }


    // Update is called once per frame
    public void HoldButton()
    {
        Show = !Show;
        if (Show)
        {
            Pass.contentType = TMP_InputField.ContentType.Standard;
            Pass.textComponent.SetAllDirty();
            ButtonOn();
            _Image.sprite = ShowImg;
        }
        else
        {
            Pass.contentType = TMP_InputField.ContentType.Password;
            Pass.textComponent.SetAllDirty();
            ButtonOff();
            _Image.sprite = HideImg;
        }
    }
}
