using UnityEngine;
using UnityEngine.AI;
using HefimaLibrary;

[System.Serializable]
public class EnemyIdleState : EnemyBaseState
{
    NavMeshAgent navMesh;
    GameObject enemyObj;


    public float moveRadius;

    public float stopTime;
    float nextMoveTime;
    bool moving;

    bool playerInRange;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemyObj = enemy.gameObject;
        navMesh = enemyObj.GetComponent<NavMeshAgent>();
        enemy.startPos = enemyObj.transform;
        navMesh.stoppingDistance = enemy.stopDis;

        DebugManager.DebugLogWarning(enemyObj.name + ": Idle State", DebugType.ENEMYDEBUG);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    public override void FixedUpdateState(EnemyStateManager enemy)
    {
        navMesh.speed = enemyObj.GetComponent<EnemyManager>().baseStats.agility;

        if (!moving && Time.time >= nextMoveTime)
        {
            MoveEnemy(enemy);
        }
        if (moving && navMesh.remainingDistance <= enemy.stopDis)
        {
            nextMoveTime = Time.time + Random.Range(0, stopTime);
            moving = false;
        }

        playerInRange = Physics.CheckSphere(enemyObj.transform.position, enemy.sightRange, enemy.playerMask);

        if(playerInRange)
            enemy.SwitchState(enemy.attackState);
    }

    void MoveEnemy(EnemyStateManager enemy)
    {
        moving = true;
        navMesh.SetDestination(HefiMath.RandomVector3_Plane(moveRadius, enemy.startPos.position));
    }
}
