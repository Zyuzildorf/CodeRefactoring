using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class BulletShooter : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Bullet _prefab;
    [SerializeField] private int _poolCapacity = 20;
    [SerializeField] private int _poolMaxSize = 30;

    private ObjectPool<Bullet> _pool;
    private Vector3 _direction;
    private bool IsShooting;
    private bool IsTriggerPulled;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => obj.gameObject.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
       StartCoroutine(Shooting());
    }

    private void OnMouseDown()
    {
        if (IsTriggerPulled)
        {
            IsTriggerPulled = false;
            StopShooting();
        }
        else if (IsTriggerPulled == false)
        {
            IsTriggerPulled = true;
            StartShooting();
        }
        else
        {
            IsTriggerPulled = true;
            StartShooting();
        }
    }

    private void StartShooting()
    {
        IsShooting = true;
        StartCoroutine(Shooting());
    }

    private void StopShooting()
    {
        IsShooting = false;
        StopCoroutine(Shooting());
    }

    private void GetBullet()
    {
        _pool.Get();
    }
    
    private void ActionOnGet(Bullet bullet)
    {
        _direction = (_target.position - transform.position).normalized;
        
        bullet.transform.position = transform.position + _direction;
        bullet.transform.up = _direction;
        bullet.GetComponent<Rigidbody>().velocity = _direction * _prefab.BulletSpeed;
        
        bullet.gameObject.SetActive(true);

        bullet.OnPreferRelease += ReleaseObject;
    }

    private void ReleaseObject(Bullet bullet)
    {
        _pool.Release(bullet);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;

        bullet.OnPreferRelease -= ReleaseObject;
    }

    private IEnumerator Shooting()
    {
        WaitForSeconds _shootingRepeatRate = new WaitForSeconds(_prefab.RepeatShottingRate);
        
        while (IsShooting)
        {
            GetBullet();
            yield return _shootingRepeatRate;
        }
    }
}