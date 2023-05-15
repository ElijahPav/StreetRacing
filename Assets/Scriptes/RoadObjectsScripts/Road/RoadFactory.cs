using UnityEngine;

public class RoadFactory : RoadObjectsFactory
{
    private readonly Vector3 _defaultRoadPosition = Vector3.zero;
    private readonly Vector3 _distanceBetweenRoads = new Vector3(0, 10, 0);
    protected int _objectIndex;

    private RoadModel _roadModel;

    public RoadFactory(ObjectPooler objectPooler, RoadModel roadModel) : base(objectPooler, 3) 
    {
        _roadModel = roadModel;
        _objectIndex = _objectPooler.AddObject(roadModel._roadPrefab, _maxCountOfActiveObjects, false);
    }

    public override GameObject SetNewObject()
    {
        var newRoad = _objectPooler.GetPooledObject(_objectIndex);
        if (newRoad == null)
            return null;

        if (_lastObjectTransform == null)
        {
            newRoad.transform.position = _defaultRoadPosition;
        }
        else
        {
            newRoad.transform.position = _lastObjectTransform.position + _distanceBetweenRoads;
        }

        _lastObjectTransform = newRoad.transform;
        newRoad.SetActive(true);
        return newRoad;
    }
}
