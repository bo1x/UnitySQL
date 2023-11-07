using System.Collections;
using System.Collections.Generic;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserData
{
    public int id { get; private set; }
    public string user { get; private set; }
    public int BestScore { get; private set; }

    public UserData(int idV, string userV, int scoreV)
    {
        id = idV;
        user = userV;
        BestScore = scoreV;
    }
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    UserData DataUser = null;

    Timer _Timer;

    LoseCanvas _losecanvas;

    [SerializeField] private TMP_Text _ScoreText;

    private float Life;
    private int TimeGame;
    private float StartTime;

    private bool _StopGame = true;

    [SerializeField] private Slider _Slider;

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }

    public void ResetGame()
    {
        StartTime = Time.time;
        Life = 0.5f;
        TimeGame = 0;
        _Slider.value = 0.5f;
        _StopGame = true;
        UpdateScore();
    }

    public void setTimer(Timer value)
    {
        _Timer = value;
        _ScoreText = _Timer.GetComponent<TMP_Text>();
    }


    public void setSlider(Slider value)
    {
        _Slider = value;
    }

    public void GameOver()
    {
        _StopGame = true;
        AudioManager.Instance.Play("GameOver");
        _losecanvas.gameover();
    }

    public int GetPoints()
    {
        return TimeGame;
    }

    public void AddLife(float value)
    {
        Life += value;

        _Slider.value = Mathf.Clamp(Life, _Slider.minValue, _Slider.maxValue);

        if (Life > _Slider.minValue && Life < _Slider.maxValue)
            return;

        GameOver();

    }

    public void UpdateScore()
    {
        TimeGame = (int)((Time.time - StartTime) * 3);
        _ScoreText.text = "Points: " + TimeGame.ToString();
    }

    public float GetTimeGame()
    {
        return Time.time - StartTime;
    }

    public void SetStopGame(bool Value)
    {
        _StopGame = Value;
        return;
    }

    public void SetLoseCanvas(LoseCanvas value)
    {
        _losecanvas = value;
        return;
    }

    public bool StopGame()
    {
        return _StopGame;
    }

    public void SetDataUser(UserData value)
    {
        DataUser = value;
        return;
    }

    public UserData GetDataUser()
    {
        return DataUser;
    }
}
