using UnityEngine;
using UnityEngine.AI;
public class FleeState : IEnemyState {
    private float timer;

    public void Enter(EnemyAi enemy) {
        enemy.Agent.speed = enemy.chaseSpeed * 1.2f;
        enemy.Agent.isStopped = false;
        
        Vector3 dirToPlayer = enemy.transform.position - enemy.playerTarget.position;
        Vector3 newPos = enemy.transform.position + dirToPlayer.normalized * 10f;
        NavMeshHit hit;

        if (NavMesh.SamplePosition(newPos, out hit, 5f, 1)) enemy.Agent.SetDestination(hit.position);    
        timer = 0;
    }

    public void Update(EnemyAi enemy) {
        timer += Time.deltaTime;
        if (timer > enemy.fleeDuration) enemy.ChangeState(new PatrolState());
    }

    public void Exit(EnemyAi enemy) { }
}