using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Slider Slider;
    [SerializeField] private GameObject BestText;
    public bool RecordSound = false;

    private void Start()
    {
        GameManager.Instance.setTimer(this);
        GameManager.Instance.setSlider(Slider);
    }

    private void OnEnable()
    {
        GameManager.Instance.setTimer(this);
        GameManager.Instance.setSlider(Slider);
    }

    private void OnDisable()
    {
        GameManager.Instance.setTimer(null);
        GameManager.Instance.setSlider(null);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (GameManager.Instance.StopGame())
        {
            RecordSound = false;
            BestText.SetActive(false);
            return;
        }

        GameManager.Instance.AddLife((-0.025f - Mathf.Clamp(0.001f * GameManager.Instance.GetTimeGame(), 0, 0.175f)) * Time.deltaTime);
        GameManager.Instance.UpdateScore();

        if(GameManager.Instance.GetPoints() > GameManager.Instance.GetDataUser().BestScore && !RecordSound)
        {
            RecordSound = true;
            BestText.SetActive(true);
            AudioManager.Instance.Play("Notify");
        }
    }

}
