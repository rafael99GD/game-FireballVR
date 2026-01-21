public class ChaseState : IEnemyState
{
    public void Enter(EnemyAi enemy) {
        enemy.Agent.speed = enemy.chaseSpeed;
    }

    public void Update(EnemyAi enemy) {

        if(enemy.playerTarget != null) enemy.Agent.SetDestination(enemy.playerTarget.position);

        float distance = enemy.DistanceToPlayer();

        if (distance <= enemy.attackRange) enemy.ChangeState(new AttackState());
        else if (distance > enemy.detectionRadius * 1.5f) enemy.ChangeState(new PatrolState());
    }

    public void Exit(EnemyAi enemy) { }
}