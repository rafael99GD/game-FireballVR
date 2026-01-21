using UnityEngine;
using UnityEngine.Events;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }
    [SerializeField] private int maxLives = 5;
    [SerializeField] private float healthRegenDelay = 5.0f;
    [SerializeField] private float healthRegenInterval = 2.0f;
    public int CurrentLives { get; private set; }
    private float lastDamageTime;
    private float lifeRegenTimer;

    [SerializeField] private float maxMana = 100f;
    [SerializeField] private float manaRegenRate = 15f;
    [SerializeField] private float manaRegenDelay = 1.0f;
    
    public float CurrentMana { get; private set; }
    private float lastManaUseTime;

    public UnityEvent<int> OnLivesChanged; 
    public UnityEvent<float> OnManaChanged; 
    public UnityEvent OnPlayerDeath;

    void Awake()
    {
        if (Instance != null && Instance != this) {
            Destroy(this.gameObject);
            return;
        }

        Instance = this;
    }

    void Start()
    {
        CurrentLives = maxLives;
        CurrentMana = maxMana;
        Time.timeScale = 1f;

        if (OnLivesChanged != null) OnLivesChanged.Invoke(CurrentLives);
        if (OnManaChanged != null) OnManaChanged.Invoke(1f);
    }

    void Update()
    {
        HandleHealthRegeneration();
        HandleManaRegeneration();
    }

    public void TakeDamage(int amount)
    {
        if (CurrentLives > 0) {
            CurrentLives--;
            lastDamageTime = Time.time;
            
            Debug.Log($"takeDamage, {CurrentLives} lives");
            OnLivesChanged?.Invoke(CurrentLives);

            if (CurrentLives <= 0) Die();
        }
    }

    private void HandleHealthRegeneration()
    {
        if (CurrentLives >= maxLives || CurrentLives <= 0) return;

        if (Time.time - lastDamageTime > healthRegenDelay)
        {
            lifeRegenTimer += Time.deltaTime;
            if (lifeRegenTimer >= healthRegenInterval)
            {
                CurrentLives++;
                lifeRegenTimer = 0; 
                OnLivesChanged?.Invoke(CurrentLives);
            }
        } else lifeRegenTimer = 0;
    }

    private void Die()
    {
        OnPlayerDeath?.Invoke();
        Time.timeScale = 0f; 
    }

    public bool TrySpendMana(float cost)
    {
        if (CurrentMana >= cost)
        {
            CurrentMana -= cost;
            lastManaUseTime = Time.time;
            OnManaChanged?.Invoke(CurrentMana / maxMana);
            return true;
        }
        return false;
    }

    private void HandleManaRegeneration()
    {
        if (CurrentMana >= maxMana) return;

        if (Time.time - lastManaUseTime > manaRegenDelay)
        {
            CurrentMana += manaRegenRate * Time.deltaTime;
            if (CurrentMana > maxMana) CurrentMana = maxMana;
            OnManaChanged?.Invoke(CurrentMana / maxMana);
        }
    }
}