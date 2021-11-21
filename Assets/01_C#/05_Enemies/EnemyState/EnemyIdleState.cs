using UnityEngine;
using UnityEngine.AI;
using HefimaLibrary;

[System.Serializable]
public class EnemyIdleState : EnemyBaseState
{
    NavMeshAgent navMesh;
    GameObject enemyObj;

    Transform startPos;

    public float moveRadius;

    public float stopTime;
    float nextMoveTime;
    bool moving;

    bool playerInRange;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemyObj = enemy.gameObject;
        navMesh = enemyObj.GetComponent<NavMeshAgent>();
        startPos = enemyObj.transform;
        GameManager.acc.DM.DebugLogWarning(enemyObj.name + ": Idle State", DebugType.ENEMYDEBUG);

        navMesh.speed = navMesh.speed / 2;
    }

    public override void UpdateState(EnemyStateManager enemy)
    {

    }

    public override void FixedUpdateState(EnemyStateManager enemy)
    {
        if (!moving && Time.time >= nextMoveTime)
        {
            MoveEnemy();
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

    public override void OnTriggerEnter(EnemyStateManager enemy, Collider other)
    {
        /*if(other.tag == "Player")
        {
            enemy.SwitchState(enemy.attackState);
        }*/
    }

    void MoveEnemy()
    {
        moving = true;
        navMesh.SetDestination(HefiMath.RandomVector3_Plane(moveRadius, startPos.position));
    }
}
