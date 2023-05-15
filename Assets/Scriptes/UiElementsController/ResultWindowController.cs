using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultWindowController : MonoBehaviour
{
    [SerializeField] private GameObject _resultWindow;
    [SerializeField] private TextMeshProUGUI _bestScoreText;
    [SerializeField] private TextMeshProUGUI _currentScoreText;

    public Button _restartButton;

    public void ShowWindow(int bestScore, int currentScore)
    {
        _bestScoreText.text = bestScore.ToString();
        _currentScoreText.text = currentScore.ToString();
        _resultWindow.SetActive(true);
    }
    public void HideWindow()
    {
        _resultWindow.SetActive(false);
    }




}
