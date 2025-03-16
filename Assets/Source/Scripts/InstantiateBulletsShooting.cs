using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class InstantiateBulletsShooting : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private float _repeatShooting;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private float _timeOfShootingProcess = 0.0f;
    [SerializeField] private int _poolCapacity = 20;
    [SerializeField] private int _poolMaxSize = 30;

    private ObjectPool<GameObject> _pool;
    private Vector3 _direction;

    private void Awake()
    {
        _pool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ReleaseObject(obj),
            actionOnDestroy: (obj) => Destroy(obj),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    void Start()
    {
       InvokeRepeating(nameof(GetBullet), _timeOfShootingProcess, _repeatShooting);
    }

    private void GetBullet()
    {
        _pool.Get();
    }
    
    private void ActionOnGet(GameObject bullet)
    {
        _direction = (_target.position - transform.position).normalized;
        
        bullet.transform.position = transform.position + _direction;
        bullet.GetComponent<Rigidbody>().transform.up = _direction;
        bullet.GetComponent<Rigidbody>().velocity = _direction * _bulletSpeed;
        
        bullet.SetActive(true);
    }

    private void ReleaseObject(GameObject bullet)
    {
        _pool.Release(bullet);
        bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
        
        bullet.SetActive(false);
    }
}