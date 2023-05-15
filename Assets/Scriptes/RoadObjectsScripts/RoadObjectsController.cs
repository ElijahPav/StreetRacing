using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RoadObjectsController : MonoBehaviour
{
    private const float _roadObjectSpeed = 6;
    private const float _roadObjectVerticalBorder = -11;

    private const int _coinsProbability = 45;

    private RoadFactory _roadFactory;
    private InteractableRoadObjectsFactory _obstacleBusterFacatory;

    private GameStateController _gameStateController;

    private GameObject[] _roads;
    private GameObject[] _interactableObjects;

    private List<GameObject> _coins = new List<GameObject>();

    private Vector3 ObjectsTranslateVector => _roadObjectSpeed * Vector3.down * Time.deltaTime;

    private bool IsMoveEnable;

    [Inject]
    private void Constructor(RoadFactory roadFactory,
                             InteractableRoadObjectsFactory obstacleBusterFactory,
                             GameStateController gameStateController)
    {
        _roadFactory = roadFactory;
        _obstacleBusterFacatory = obstacleBusterFactory;
        _gameStateController = gameStateController;
    }
    private void Start()
    {
        _gameStateController.GameStart += PrepareRoadBeforeStart;
        _gameStateController.PlayerDeath += () => { IsMoveEnable = false; };
    }
    private void OnDestroy()
    {
        _gameStateController.GameStart -= PrepareRoadBeforeStart;
        _gameStateController.PlayerDeath -= () => { IsMoveEnable = false; };
    }

    private void Update()
    {
        if (IsMoveEnable)
        {
            MoveRoadObjects(ref _roads, _roadFactory);
            var newRoadObjects = MoveRoadObjects(ref _interactableObjects, _obstacleBusterFacatory);
        }

    }


    private GameObject[] MoveRoadObjects(ref GameObject[] roadObjects, RoadObjectsFactory objectsFactory)
    {
        var newObjects = new List<GameObject>();
        var oldObjectsIndexes = new List<int>();

        for (var i = 0; i < roadObjects.Length; i++)
        {
            roadObjects[i].transform.Translate(ObjectsTranslateVector, Space.World);

            if (roadObjects[i].transform.position.y <= _roadObjectVerticalBorder)
            {
                roadObjects[i].SetActive(false);
                var newObject = objectsFactory.SetNewObject();

                newObjects.Add(newObject);
                oldObjectsIndexes.Add(i);

            }
        }

        if (newObjects.Count > 0)
        {
            for (var i = 0; i < newObjects.Count; i++)
            {
                roadObjects[oldObjectsIndexes[i]] = newObjects[i];
            }
            return newObjects.ToArray();
        }
        return null;
    }

    private void PrepareRoadBeforeStart()
    {
        if (_roads!=null && _roads.Length > 0)
        {
            foreach (var road in _roads)
            {
                road.SetActive(false);
            }
        }
        if (_interactableObjects!=null && _interactableObjects.Length > 0)
        {
            foreach (var inerObject in _interactableObjects)
            {
                inerObject.SetActive(false);
            }
        }

        _roads = _roadFactory.GenetrateObjectsBeforeStart();
        _interactableObjects = _obstacleBusterFacatory.GenetrateObjectsBeforeStart();
        IsMoveEnable = true;
    }
}
