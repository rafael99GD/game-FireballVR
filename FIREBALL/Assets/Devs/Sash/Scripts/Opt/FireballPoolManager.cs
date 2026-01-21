using UnityEngine;
using System.Collections.Generic;

public class FireballPoolManager : MonoBehaviour
{
    public static FireballPoolManager Instance { get; private set; }

    [Header("Pool Settings")]
    public GameObject fireballPrefab;
    public int initialPoolSize = 10;
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Awake() {
        // Singleton pattern
        if (Instance != null && Instance != this) Destroy(this.gameObject);        
        else Instance = this;

        InitializePool();
    }

    private void InitializePool() {
        for (int i = 0; i < initialPoolSize; i++) pool.Enqueue(CreateNewFireball());
    }

    private GameObject CreateNewFireball() {
        GameObject ball = Instantiate(fireballPrefab);
        ball.SetActive(false);
        ball.transform.SetParent(this.transform); 
        return ball;
    }

    public GameObject GetFireball() {
        GameObject ball;
        if (pool.Count > 0) ball = pool.Dequeue();
        else ball = CreateNewFireball();
        
        ball.SetActive(true);
        return ball;
    }

    public void ReturnFireball(GameObject ball) {
        IProjectileBehavior behavior = ball.GetComponent<IProjectileBehavior>();
        if (behavior != null) Destroy(behavior as Component);
    
        BaseProjectile baseProjectile = ball.GetComponent<BaseProjectile>();
        if (baseProjectile != null) baseProjectile.ResetProjectile();

        ball.SetActive(false);
        pool.Enqueue(ball);
    }
}