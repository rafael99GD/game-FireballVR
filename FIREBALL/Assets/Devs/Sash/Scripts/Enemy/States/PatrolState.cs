using UnityEngine;
using UnityEngine.AI;

public class PatrolState : IEnemyState
{
    private float waitTimer;

    public void Enter(EnemyAi enemy)
    {
        enemy.Agent.speed = enemy.patrolSpeed;
        MoveToRandomPoint(enemy);
    }

    public void Update(EnemyAi enemy)
    {
        if (enemy.DistanceToPlayer() < enemy.detectionRadius){
            enemy.ChangeState(new ChaseState());
            return;
        }

        if (!enemy.Agent.pathPending && enemy.Agent.remainingDistance < 0.5f) {
            waitTimer += Time.deltaTime;
            if (waitTimer > 2.0f) {
                MoveToRandomPoint(enemy);
                waitTimer = 0;
            }
        }
    }

    public void Exit(EnemyAi enemy) { }

    private void MoveToRandomPoint(EnemyAi enemy) {
        Vector3 randomDir = Random.insideUnitSphere * 15f;
        randomDir += enemy.transform.position;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomDir, out hit, 15f, 1)) enemy.Agent.SetDestination(hit.position);
    }
}