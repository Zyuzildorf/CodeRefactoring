using System.Collections;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Bullet _prefab;
    [SerializeField] private float _repeatShootingRate;

    private WaitForSeconds _shootingRepeatRate;
    private Bullet _newBullet;
    private Vector3 _direction;
    private bool _isShooting;

    private void Awake()
    {
        _shootingRepeatRate = new WaitForSeconds(_repeatShootingRate);
        _isShooting = true;
    }

    private void Start()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        while (_isShooting)
        {
            _direction = (_target.position - transform.position).normalized;

            _newBullet = Instantiate(_prefab, transform.position + _direction, Quaternion.identity);
            
            _newBullet.transform.up = _direction;

            if (_newBullet.TryGetComponent(out Rigidbody rigidbody))
            {
                rigidbody.velocity = _direction * _prefab.BulletSpeed;
            }
            else
            {
                Debug.Log("Компонент Rigidbody не найден.");
            }
            
            yield return _shootingRepeatRate;
        }
    }
}