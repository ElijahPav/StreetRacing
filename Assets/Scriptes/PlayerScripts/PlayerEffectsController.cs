using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerEffectsController : MonoBehaviour
{

    private const float _speedDownDebuffDuration = 10;
    private const float _buffDuration = 15;

    private const int _blockDamageValue = 100;
    private const int _crackDamageValue = 20;

    private const int _healValue = 25;

    [SerializeField] BarsController _barsController;

    private float _speedDownDebuffTimer;
    private bool _isSpeedDownDebuffActive;

    public event Action<int> DamagePlayerEvent;
    public event Action SlowDownDebuffStart;
    public event Action SlowDownDebuffEnd;

    public event Action<int> HeartBuffActivate;
    public event Action<BoosterTypes> TemporraryBaffActivate;
    public event Action<BoosterTypes> TemporraryBaffDeactivate;

    private Dictionary<BoosterTypes, float> _boosterTimePaier = new Dictionary<BoosterTypes, float>();
    private BoosterTypes[] _activeBoosters => _boosterTimePaier.Keys.ToArray();

    void Update()
    {
        CheckActiveBuffTime();
        CheckActiveDebuffTime();
        
    }
    private void CheckActiveBuffTime()
    {
        foreach (var boosterType in _activeBoosters)
        {
            if (_boosterTimePaier[boosterType] > 0)
            {
                _boosterTimePaier[boosterType] -= Time.deltaTime;
                _barsController.UpdateBuffBar(boosterType, _boosterTimePaier[boosterType] / _buffDuration);
            }
            else
            {
                DeactivateBuff(boosterType);
            }
        }
    }
    private void CheckActiveDebuffTime()
    {

        if (_isSpeedDownDebuffActive)
        {
            if (_speedDownDebuffTimer <= 0)
            {
                DeactivateSpeedDebuff();
            }
            else
            {
                _speedDownDebuffTimer -= Time.deltaTime;
            }
        }
    }

    public void ActivateBoosterEffect(BoosterTypes type)
    {
        switch (type)
        {
            case BoosterTypes.Heart:
                ActivateHeartBuff();
                break;
            case BoosterTypes.Shield:
                ActivateHeartBuff();
                ActivateTemporraryBuff(BoosterTypes.Shield, _buffDuration);
                break;
            case BoosterTypes.Nitro:
                _isSpeedDownDebuffActive = false;
                ActivateTemporraryBuff(BoosterTypes.Nitro, _buffDuration);
                break;
            case BoosterTypes.Magnet:
                ActivateTemporraryBuff(BoosterTypes.Magnet, _buffDuration);
                break;
        }
    }
    public void ActivateObstacleEffect(ObstacleTypes type)
    {
        switch (type)
        {
            case ObstacleTypes.Block:
                ActivateBlockDebuf();
                break;
            case ObstacleTypes.OilPuddle:
                ActivateOilDebuf();
                break;
            case ObstacleTypes.Crack:
                ActivateCrackDebuf();
                break;

        }
    }
    private void ActivateHeartBuff()
    {
        HeartBuffActivate?.Invoke(_healValue);
    }
    private void DamagePlayer(int damage)
    {
        DamagePlayerEvent?.Invoke(damage);
    }

    private void ActivateTemporraryBuff(BoosterTypes type, float time)
    {
        if (_activeBoosters.Contains(type))
        {
            _boosterTimePaier[type] = time;
        }
        else
        {
            _boosterTimePaier.Add(type, time);
            _barsController.SetBuffBarActiveState(type, true);
            TemporraryBaffActivate?.Invoke(type);
        }
       
    }

    private void ActivateOilDebuf()
    {
        SlowDownPlayer();
    }
    private void ActivateCrackDebuf()
    {
        SlowDownPlayer();
        DamagePlayer(_crackDamageValue);
    }
    private void ActivateBlockDebuf()
    {
        DamagePlayer(_blockDamageValue);
    }

    private void SlowDownPlayer()
    {
        if (_activeBoosters.Contains(BoosterTypes.Nitro))
        {
            return;
        }

        _isSpeedDownDebuffActive = true;
        _speedDownDebuffTimer = _speedDownDebuffDuration;
        SlowDownDebuffStart?.Invoke();
    }
    private void DeactivateBuff(BoosterTypes type)
    {
        _boosterTimePaier.Remove(type);
        _barsController.SetBuffBarActiveState(type, false);
        TemporraryBaffDeactivate?.Invoke(type);
    }
    
    private void DeactivateSpeedDebuff()
    {
        _isSpeedDownDebuffActive = false;
        _speedDownDebuffTimer = 0;
        SlowDownDebuffEnd?.Invoke();
    }

    public void DeactivateAllEffects()
    {
        foreach (var boosterTypes in _activeBoosters)
        {
            DeactivateBuff(boosterTypes);
        }

        if (_isSpeedDownDebuffActive)
        {
            DeactivateSpeedDebuff();
        }
    }
  
}
