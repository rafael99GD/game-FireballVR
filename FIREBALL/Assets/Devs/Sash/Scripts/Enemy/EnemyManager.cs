using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int hp = 1000;
    public int damage = 20;

    public void ApplyBurn()
    {
        hp -= damage;
        Debug.Log($"Enemigo quemado. HP actual: {hp}");

        if (hp <= 0) Die();
    }
    
    private void Die() {
        Destroy(this.gameObject);
    }
}