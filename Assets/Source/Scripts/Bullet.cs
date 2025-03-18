using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _repeatShootingRate;
    [SerializeField] private float _bulletSpeed;

    public float RepeatShottingRate => _repeatShootingRate;
    public float BulletSpeed => _bulletSpeed;
  }
