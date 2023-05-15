using UnityEngine;

public abstract class RoadObjectsFactory
{
    protected ObjectPooler _objectPooler;
    protected readonly int _maxCountOfActiveObjects;
    protected Transform _lastObjectTransform;

    public RoadObjectsFactory(ObjectPooler objectPooler, int maxCountOfActiveObjects)
    {
        _objectPooler = objectPooler;
        _maxCountOfActiveObjects = maxCountOfActiveObjects;
    }

    public GameObject[] GenetrateObjectsBeforeStart()
    {
        _lastObjectTransform = null;
        var objects = new GameObject[_maxCountOfActiveObjects];
        for (int i = 0; i < _maxCountOfActiveObjects; i++)
        {
            objects[i] = SetNewObject();
        }

        return objects;
    }

    public abstract GameObject SetNewObject();


}
