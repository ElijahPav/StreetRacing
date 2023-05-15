using System;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    [SerializeField] ResultWindowController _resultWindowController;
    [SerializeField] ScoreController _scoreController;
    public bool IsGameActive { get; private set; }

    public event Action PlayerDeath;
    public event Action GameStart;
    private void Start()
    {
        _resultWindowController._restartButton.onClick.AddListener(GameStartInvoke);
        _resultWindowController.ShowWindow(_scoreController._bestScore, (int)_scoreController._currenScore);
    }

    public void PlayerDeathInvoke()
    {
        IsGameActive = false;
        _scoreController.StopScore();
        _resultWindowController.ShowWindow(_scoreController._bestScore, (int)_scoreController._currenScore);
        PlayerDeath?.Invoke();
    }

    public void GameStartInvoke()
    {
        IsGameActive = true;
        _resultWindowController.HideWindow();
        _scoreController.StartScore();
        GameStart?.Invoke();
    }

}
