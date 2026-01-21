using UnityEngine;
using System;

[RequireComponent(typeof(TrajectoryVisualizer))]
[RequireComponent(typeof(AudioSource))]
public class WandController : MonoBehaviour
{
    public GameObject fireballPrefab;
    public Transform shootPoint;
    public float launchForce = 20f;

    public AudioClip fireballThrowSound;
    private AudioSource audioSource;

    public PhysicsMaterial redBouncyMaterial;
    public string redBallTargetTag = "PuzzleTarget";
    public GameObject greenPlatformPrefab;
    public GameObject purpleBlackHolePrefab;
    public float defaultShotCost = 5f;

    private float currentSpecialManaCost = 0f;
    private System.Type defaultBehavior = typeof(OrangeHeatBehavior);
    private System.Type currentBehavior;
    private int specialShotsRemaining = 0;
    private TrajectoryVisualizer visualizer;

    public event Action<System.Type, int> onBehaviorChanged;
    public event Action onAmmoUsed;

    void Awake()
    {
        visualizer = GetComponent<TrajectoryVisualizer>();
        if (visualizer == null)
            visualizer = gameObject.AddComponent<TrajectoryVisualizer>();

        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        currentBehavior = defaultBehavior;
    }

    void Update()
    {
        if (currentBehavior == typeof(RedBounceBehavior) && specialShotsRemaining > 0)
            visualizer.DrawPath(shootPoint.position, shootPoint.forward);
        else
            visualizer.HidePath();
    }

    public void ShootFireball()
    {
        if (fireballPrefab == null || shootPoint == null || currentBehavior == null)
            return;

        if (PlayerManager.Instance != null)
        {
            float costToSpend = (specialShotsRemaining > 0)
                ? currentSpecialManaCost
                : defaultShotCost;

            if (!PlayerManager.Instance.TrySpendMana(costToSpend))
                return;
        }

        GameObject newBall = FireballPoolManager.Instance.GetFireball();
        Rigidbody ballRb = newBall.GetComponent<Rigidbody>();

        if (ballRb != null)
        {
            ballRb.isKinematic = false;
            ballRb.linearVelocity = Vector3.zero;
            ballRb.angularVelocity = Vector3.zero;
        }

        newBall.transform.position = shootPoint.position;
        newBall.transform.rotation = shootPoint.rotation;

        Component newBehaviorComponent = newBall.AddComponent(currentBehavior);
        IProjectileBehavior behaviorInterface = newBehaviorComponent as IProjectileBehavior;
        if (behaviorInterface != null)
            behaviorInterface.Initialize(this);

        BaseProjectile projectileScript = newBall.GetComponent<BaseProjectile>();
        if (projectileScript != null)
            projectileScript.Launch(shootPoint.forward, launchForce);

        if (fireballThrowSound != null)
            audioSource.PlayOneShot(fireballThrowSound);

        if (specialShotsRemaining > 0)
        {
            specialShotsRemaining--;
            onAmmoUsed?.Invoke();

            if (specialShotsRemaining == 0)
            {
                currentBehavior = defaultBehavior;
                currentSpecialManaCost = 0f;
                onBehaviorChanged?.Invoke(currentBehavior, 0);
                visualizer.HidePath();
            }
        }
    }

    public void LoadSpecialShots(IGem gem)
    {
        if (gem == null) return;

        currentBehavior = gem.GetBehaviorType();
        specialShotsRemaining = gem.GetShotCount();
        currentSpecialManaCost = gem.GetManaCost();

        onBehaviorChanged?.Invoke(currentBehavior, specialShotsRemaining);
    }
}
