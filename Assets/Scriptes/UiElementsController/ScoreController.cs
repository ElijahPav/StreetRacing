using TMPro;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    private const string _playerPrefsKey = "BestScore";

    [SerializeField] private TextMeshProUGUI _scoreUI;

    public int _bestScore { get; private set; }
    public float _currenScore { get; private set; }

    private bool _isScoreActive;

    private void Awake()
    {
        _bestScore = PlayerPrefs.GetInt(_playerPrefsKey);   
    }
    private void Update()
    {
        if (_isScoreActive)
        {
            _currenScore += Time.deltaTime * 10;
            _scoreUI.text = ((int)_currenScore).ToString();
        }
    }
    public void StopScore()
    {
        _isScoreActive = false;
        if (_currenScore > _bestScore)
        {
            _bestScore = (int)_currenScore;
            PlayerPrefs.SetInt(_playerPrefsKey,_bestScore);
        }
    }
    public void StartScore()
    {
        _isScoreActive = true;
        _currenScore = 0;
    }
}
