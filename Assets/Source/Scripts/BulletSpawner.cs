using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Bullet _prefab;

    private WaitForSeconds _shootingRepeatRate;
    private Bullet _newBullet;
    private Vector3 _direction;
    private bool _isTriggerPulled;
    private bool _isShooting;

    private void Awake()
    {
        _shootingRepeatRate = new WaitForSeconds(_prefab.RepeatShottingRate);
    }

    private void OnMouseDown()
    {
        if (_isTriggerPulled)
        {
            _isTriggerPulled = false;
            StopShooting();
        }
        else if (_isTriggerPulled == false)
        {
            _isTriggerPulled = true;
            StartShooting();
        }
        else
        {
            _isTriggerPulled = true;
            StartShooting();
        }
    }

    private void StartShooting()
    {
        _isShooting = true;
        StartCoroutine(Shooting());
    }

    private void StopShooting()
    {
        _isShooting = false;
        StopCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        while (_isShooting)
        {
            _direction = (_target.position - transform.position).normalized;

            _newBullet = Instantiate(_prefab, transform.position + _direction, Quaternion.identity);
            
            _newBullet.transform.up = _direction;
            _newBullet.GetComponent<Rigidbody>().velocity = _direction * _prefab.BulletSpeed;
            
            yield return _shootingRepeatRate;
        }
    }
}