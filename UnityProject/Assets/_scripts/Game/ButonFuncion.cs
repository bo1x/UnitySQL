using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButonFuncion : MonoBehaviour
{
    private void Awake()
    {
    }

    public void OnClick()
    {
        AudioManager.Instance.Play("Cuack");

        if(GameManager.Instance.StopGame())
        {
            GameManager.Instance.ResetGame();
            GameManager.Instance.SetStopGame(false);
        }
        GameManager.Instance.AddLife(0.1f - Mathf.Clamp(0.001f * GameManager.Instance.GetTimeGame(),0 , 0.094f));
    }
}
