using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Waypoints _waypoints;
    [SerializeField] private float _moveSpeed;

    private int _currentWaypoint;
    
    private void Update()
    {
        Move();
    }

    private void Move()
    {
        if (transform.position == _waypoints.WaypointsArray[_currentWaypoint].position)
        {
            _currentWaypoint = ++_currentWaypoint % _waypoints.WaypointsArray.Length;
        }
        
        transform.position = Vector3.MoveTowards(transform.position, _waypoints.WaypointsArray[_currentWaypoint].position,
            _moveSpeed * Time.deltaTime);
    }
}