using UnityEngine;

public class InteractableRoadObjectsFactory : RoadObjectsFactory
{
    private const float _minVerticalDistanceBetweenObgects = 4;
    private const float _maxVerticalDistanceBetweenObjects = 6;
    private const float _horizontalBorder = 1;

    private const int _boosterProbability = 15;
    private const int _obstacleProbability = 60;
    private const int _coinProbability = 25;
    private int _summOfProbabilitys => _boosterProbability + _obstacleProbability + _coinProbability;
    private readonly Vector3 _defaultObjectPosition = new Vector3(0, 10, 0);

    protected readonly int _obstaclesIndex;
    protected readonly int _boostersIndex;
    protected readonly int _coinIndex;

    private ObstacleModel _obstacleModel;
    private BoosterModel _boosterModel;
    private CoinModel _coinModel;

    public InteractableRoadObjectsFactory(ObjectPooler objectPooler,
                                 ObstacleModel obstacleModel,
                                 BoosterModel boosterModel,
                                 CoinModel coinModel)
                                : base(objectPooler, 5)
    {
        _obstacleModel = obstacleModel;
        _boosterModel = boosterModel;
        _coinModel = coinModel;

        _obstaclesIndex = _objectPooler.AddObject(_obstacleModel.ObstaclePrefab.gameObject,
                                                  _maxCountOfActiveObjects, false);

        _boostersIndex = _objectPooler.AddObject(_boosterModel.BoosterPrefab.gameObject,
                                                  _maxCountOfActiveObjects, false);

        _coinIndex = _objectPooler.AddObject(_coinModel.CoinPrefab.gameObject,
                                                  _maxCountOfActiveObjects, false);
    }

    public override GameObject SetNewObject()
    {
        GameObject newObject;
        var randomValue = Random.Range(0, _summOfProbabilitys + 1);

        if (randomValue <= _boosterProbability)
        {
            newObject = GetNewCausingEffectObject<BoosterTypes>(_boostersIndex, _boosterModel);
        }
        else if (randomValue <= _boosterProbability + _coinProbability)
        {
            newObject = GetNewCoin();
        }
        else
        {
            newObject = GetNewCausingEffectObject<ObstacleTypes>(_obstaclesIndex, _obstacleModel);
        }

        if (newObject == null)
            return null;

        newObject.transform.position = GenerateNewObjectPosition();

        _lastObjectTransform = newObject.transform;
        newObject.SetActive(true);

        return newObject;
    }

    private GameObject GetNewCausingEffectObject<T>(int index, CausingEffectRoadObjectModel<T> model) where T : System.Enum
    {
        var newBooster = _objectPooler.GetPooledObject(index);

        if (newBooster == null)
        {
            return null;
        }

        SetDataToObject<T>(newBooster.GetComponent<RoadObjectView<T>>(), model);
        return newBooster;
    }

    private GameObject GetNewCoin()
    {
        var newCoin = _objectPooler.GetPooledObject(_coinIndex);
        return newCoin;
    }
    private Vector3 GenerateNewObjectPosition()
    {
        var newPositionX = Random.Range(_horizontalBorder, -_horizontalBorder);
        var newPositionY = Random.Range(_minVerticalDistanceBetweenObgects, _maxVerticalDistanceBetweenObjects);

        if (_lastObjectTransform == null)
        {
            newPositionY += _defaultObjectPosition.y;
        }
        else
        {
            newPositionY += _lastObjectTransform.position.y;
        }

        return new Vector3(newPositionX, newPositionY, 0);
    }

    private void SetDataToObject<T>(RoadObjectView<T> objectView,
                                    CausingEffectRoadObjectModel<T> objectModel) where T : System.Enum
    {
        var objectType = objectModel.GetRandomType();
        var objectSprite = objectModel.GetSpriteByType(objectType);
        objectView.SetObjectData(objectType, objectSprite);
    }
}
