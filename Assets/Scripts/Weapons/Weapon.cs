using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool; 

public class Weapon : MonoBehaviour
{
    public Transform parentTransform;

    [Header("Weapon Stats")]
    [SerializeField] private float shootIntervalInSeconds = 3f; 

    [Header("Bullets")]
    public Bullet Bullet; 
    [SerializeField] private Transform BulletSpawnPoint;  

    [Header("Bullet Pool")]
    private IObjectPool<Bullet> objectPool;

    private readonly bool collectionCheck = false;
    private readonly int defaultCapacity = 30;
    private readonly int maxSize = 100;
    private float timer;  

    private void Awake()
    {
        if (Bullet == null) 
        {
            Debug.LogError("Bullet prefab is not assigned in the inspector.");
            return;
        }

        objectPool = new ObjectPool<Bullet>(
            CreateBullet,   
            OnTakeFromPool,  
            OnReturnedToPool,
            OnDestroyPoolObject, 
            collectionCheck,
            defaultCapacity,
            maxSize
        );
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= shootIntervalInSeconds)
        {
            Shoot(); 
            timer = 0f; 
        }
    }

    private void Shoot()
{
    if (Bullet == null || BulletSpawnPoint == null)
    {
        Debug.LogError("Bullet or BulletSpawnPoint is not assigned.");
        return;
    }

    Bullet newBullet = objectPool.Get(); 
    newBullet.transform.position = BulletSpawnPoint.position;
    newBullet.transform.rotation = BulletSpawnPoint.rotation;
    newBullet.SetPool(objectPool); 
}


    private Bullet CreateBullet()
    {
        Bullet newBullet = Instantiate(Bullet);
        newBullet.gameObject.SetActive(false);
        return newBullet;
    }

    private void OnReturnedToPool(Bullet Bullet)
    {
        Bullet.gameObject.SetActive(false);
    }

    private void OnTakeFromPool(Bullet Bullet)
    {
        Bullet.gameObject.SetActive(true);
    }

    private void OnDestroyPoolObject(Bullet Bullet)
    {
        Destroy(Bullet.gameObject);
    }
}
