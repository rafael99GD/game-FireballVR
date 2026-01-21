using UnityEngine;

public class AttackState : IEnemyState {
    private float cooldownTimer = 0;
    private bool hasAttacked = false;

    public void Enter(EnemyAi enemy) {
        enemy.Agent.isStopped = true;
        enemy.transform.LookAt(enemy.playerTarget);
        cooldownTimer = 2.0f;
        hasAttacked = false;
    }

    public void Update(EnemyAi enemy) {
        if (!hasAttacked) {
            PerformAttack(enemy);
            hasAttacked = true;
        }

        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0) enemy.ChangeState(new FleeState()); 
    }

    public void Exit(EnemyAi enemy) {
        enemy.Agent.isStopped = false;
    }

    private void PerformAttack(EnemyAi enemy) {
        if (enemy.playerTarget == null) return;
        
        PlayerManager playerStats = enemy.playerTarget.GetComponent<PlayerManager>();
        EnemyManager myStats = enemy.GetComponent<EnemyManager>();

        if (playerStats != null) {
            int damageDealt = (myStats != null) ? myStats.damage : 10;            
            playerStats.TakeDamage(damageDealt);
        }
    }
}