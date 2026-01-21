public interface IEnemyState
{
    void Enter(EnemyAi enemy);
    void Update(EnemyAi enemy);
    void Exit(EnemyAi enemy);
}