using UnityEngine;
using Zenject;

public class PlayerController : MonoBehaviour
{
    private const string _obstacleTag = "Obstacle";
    private const string _boosterTag = "Booster";
    private const string _coinTag = "Coin";
    private const int _playerMaxHP = 100;

    [SerializeField] private PlayerMovementController _playerMovementController;
    [SerializeField] private PlayerEffectsController _playerEffectController;
    [SerializeField] private MagnetController _magnetController;
    [SerializeField] private CurrencyController _currencyController;

    [SerializeField] private BarsController _barsController;

    [Inject] GameStateController _gameStateController;

    private int _playerHpValue;
    private bool _isPlayerShieldActive;
    private Vector3 _defaultPlayerPosition = new Vector3(0, -3, 0);
    private int _playerHP
    {
        get { return _playerHpValue; }
        set
        {
            if (value <= 0)
            {
                _playerHpValue = 0;
                OnPlayerDeath();
            }
            else if (value > _playerMaxHP)
            {
                _playerHpValue = _playerMaxHP;
            }
            else
            {
                _playerHpValue = value;
            }
            _barsController.UpdateHealthBar((float)_playerHpValue / _playerMaxHP);
        }
    }

    private void Start()
    {
        _playerHpValue = _playerMaxHP;
        _gameStateController.GameStart += OnGameStart;
        Subskriptions();
    }


    private void OnDestroy()
    {
        Unsubskriptions();
    }

    private void OnGameStart()
    {
        transform.position=_defaultPlayerPosition;
        _playerHP = _playerMaxHP;
        _playerMovementController.EnableMovement();

    }

    private void OnPlayerDeath()
    {
        _playerMovementController.DithableMovement();
        _playerEffectController.DeactivateAllEffects();
        _gameStateController.PlayerDeathInvoke();
    }

    private void Subskriptions()
    {
        _playerEffectController.DamagePlayerEvent += DamagePlayer;
        _playerEffectController.SlowDownDebuffStart += SlowDownPlayer;
        _playerEffectController.SlowDownDebuffEnd += DecactivateSlowDownDebuff;

        _playerEffectController.HeartBuffActivate += HealPlayer;
        _playerEffectController.TemporraryBaffActivate += ActivateBuff;
        _playerEffectController.TemporraryBaffDeactivate += DeactivateBuff;
    }
    private void Unsubskriptions()
    {
        _playerEffectController.DamagePlayerEvent -= DamagePlayer;
        _playerEffectController.SlowDownDebuffStart -= SlowDownPlayer;

        _playerEffectController.HeartBuffActivate -= HealPlayer;
        _playerEffectController.TemporraryBaffActivate -= ActivateBuff;
        _playerEffectController.TemporraryBaffDeactivate -= DeactivateBuff;
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag(_obstacleTag))
        {
            var obstacle = collider.GetComponent<ObstacleView>();
            _playerEffectController.ActivateObstacleEffect(obstacle.ObjectType);
        }

        if (collider.gameObject.CompareTag(_boosterTag))
        {
            var booster = collider.GetComponent<BoosterView>();
            _playerEffectController.ActivateBoosterEffect(booster.ObjectType);
            collider.gameObject.SetActive(false);
        }
        if (collider.gameObject.CompareTag(_coinTag))
        {
            _currencyController.AddCoins();
            collider.gameObject.SetActive(false);
        }
    }

    private void ActivateBuff(BoosterTypes type)
    {
        switch (type)
        {
            case BoosterTypes.Nitro:
                _playerMovementController.BoostSpeed();
                break;
            case BoosterTypes.Shield:
                _isPlayerShieldActive = true;
                break;
            case BoosterTypes.Magnet:
                _magnetController.gameObject.SetActive(true);
                break;
        }
    }
    private void DeactivateBuff(BoosterTypes type)
    {
        switch (type)
        {
            case BoosterTypes.Nitro:
                _playerMovementController.SetDefaultSpeed();
                break;
            case BoosterTypes.Shield:
                _isPlayerShieldActive = false;
                break;
            case BoosterTypes.Magnet:
                _magnetController.gameObject.SetActive(false);
                break;
        }
    }
    public void HealPlayer(int value)
    {
        _playerHP += value;
    }
    public void DamagePlayer(int value)
    {
        if (!_isPlayerShieldActive)
        {
            _playerHP -= value;
        }

    }

    private void SlowDownPlayer()
    {
        _playerMovementController.DebuffSpeed();
    }
    private void DecactivateSlowDownDebuff()
    {
        _playerMovementController.SetDefaultSpeed();
    }
}
