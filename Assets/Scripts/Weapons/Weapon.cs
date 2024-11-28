using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.Assertions;

public class Weapon : MonoBehaviour
{
    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f; 

    [Header("Bullets")]
    public Bullet bullet; 
    [SerializeField] private Transform bulletSpawnPoint; 

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool; 

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;
    public Transform parentTransform;

    private void Awake()
    {
        Assert.IsNotNull(bulletSpawnPoint); 

        objectPool = new ObjectPool<Bullet>(CreateBullet, OnGetFromPool, OnReleaseToPool, OnDestroyPooledObject, collectionCheck, defaultCapacity, maxSize);
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootIntervalInSeconds)
        {
            timer = 0f; 
            Shoot(); 
        }
    }

    private void Shoot()
    {
        Bullet bulletObj = objectPool.Get();

        bulletObj.transform.SetPositionAndRotation(bulletSpawnPoint.position, bulletSpawnPoint.rotation);

        bulletObj.SetPool(objectPool);
    }

    private Bullet CreateBullet()
    {
        Bullet instance = Instantiate(bullet);
        instance.objectPool = objectPool;
        instance.transform.parent = transform; 

        return instance;
    }

    private void OnGetFromPool(Bullet obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReleaseToPool(Bullet obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyPooledObject(Bullet obj)
    {
        Destroy(obj.gameObject);
    }
}
