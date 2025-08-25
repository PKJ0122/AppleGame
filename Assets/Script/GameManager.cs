using System;
using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    public const float TICK_TIME = 120f;

    float _ticktime;
    public float TickTime
    {
        get
        {
            return _ticktime;
        }
        set
        {
            _ticktime = value;
            OnTickTimeChange?.Invoke(value);
        }
    }

    bool _playing;
    public bool Playing
    {
        get => _playing;
        set
        {
            _playing = value;
            OnPlayingCharge?.Invoke(value);
        }
    }

    int _score;
    public int Score
    {
        get => _score;
        set
        {
            _score = value;
            OnScoreCharge?.Invoke(value);
        }
    }

    public event Action<float> OnTickTimeChange;
    public event Action<bool> OnPlayingCharge;
    public event Action<int> OnScoreCharge;
    Coroutine _game;


    public void GameStart()
    {
        _game = StartCoroutine(C_Game());
    }

    public void GameEnd(bool timeOver)
    {
        Playing = false;
        StopCoroutine(_game);

        if (timeOver)
        {
            UIManager.Instance.Get<EndUI>().Show();
        }
    }

    public void GameReStart()
    {
        GameEnd(false);
        GameStart();
    }

    public void PlusScore(int point)
    {
        Score += point;
    }

    IEnumerator C_Game()
    {
        TickTime = TICK_TIME;
        Playing = true;
        Score = 0;

        while (TickTime > 0)
        {
            TickTime -= Time.deltaTime;
            yield return null;
        }

        TickTime = 0;
        GameEnd(true);
    }
}
