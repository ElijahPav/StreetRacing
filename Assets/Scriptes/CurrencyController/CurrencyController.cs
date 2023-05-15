using UnityEngine;

public class CurrencyController : MonoBehaviour
{
    private string playerPrefsKey = "Currency";
    private const int _coinCurrencyEquvivalent = 10;

    private int _currencyValue;


    private void Start()
    {
        _currencyValue = PlayerPrefs.GetInt(playerPrefsKey);
    }
    public void AddCoins(int amoutn = 1)
    {
        _currencyValue += amoutn * _coinCurrencyEquvivalent;
    }
    private void OnDestroy()
    {
        PlayerPrefs.SetInt(playerPrefsKey, _currencyValue);
    }
}
