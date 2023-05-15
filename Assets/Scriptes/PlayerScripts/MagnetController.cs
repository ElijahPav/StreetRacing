using System.Collections.Generic;
using UnityEngine;

public class MagnetController : MonoBehaviour
{
    private const string _coinTag = "Coin";
    private const int _coinsMoveSpeed = 5;

    private List<GameObject> _coinsList = new List<GameObject>();


    private void Update()
    {
        var deactivatedCoins = new List<GameObject>();
        foreach (var coin in _coinsList)
        {
            coin.transform.Translate(transform.position * _coinsMoveSpeed * Time.deltaTime);
            if (!coin.activeSelf)
            {
                deactivatedCoins.Add(coin);
            }
        }

        if (deactivatedCoins.Count > 0)
        {
            foreach (var coin in deactivatedCoins)
            {
                _coinsList.Remove(coin);
            }
        }
    }
    private void OnEnable()
    {
        if (_coinsList.Count > 0)
        {
            _coinsList.Clear();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(_coinTag))
        {
            _coinsList.Add(collision.gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(_coinTag) && _coinsList.Contains(collision.gameObject))
        {
            _coinsList.Remove(collision.gameObject);
        }
    }
}
