using UnityEngine;

public class Waypoints : MonoBehaviour
{
    [SerializeField] private Transform[] _waypointsArray;

    private Transform[] _copiedArray;
    
    public Transform[] WaypointsArray => CopyArray();
    
    [ContextMenu("GetWaypoints")]
    private void GetWaypoints()
    {
        _waypointsArray = new Transform[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            _waypointsArray[i] = transform.GetChild(i);
        }
    }

    private Transform[] CopyArray()
    {
        _copiedArray = new Transform[_waypointsArray.Length];

        for (int i = 0; i < _waypointsArray.Length; i++)
        {
            _copiedArray[i] = _waypointsArray[i];
        }

        return _copiedArray;
    }
}