using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyAttackingState : EnemyBaseState
{
    NavMeshAgent navMesh;
    GameObject enemyObj;

    bool playerInRange;
    private float nextAttack;
    private float attackCD;
    public Transform attackPoint;
    private float attackRange;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemyObj = enemy.gameObject;
        navMesh = enemyObj.GetComponent<NavMeshAgent>();
        attackPoint = enemy.attackpoint;

        navMesh.speed = enemyObj.GetComponent<EnemyManager>().enemyStats.moveSpeed * 2;
        nextAttack = Time.time;

        DebugManager.DebugLogWarning(enemyObj.name + ": Attacking State", DebugType.ENEMYDEBUG);
    }

    public override void UpdateState(EnemyStateManager enemy)
    {
        if (navMesh.remainingDistance <= enemy.stopDis && playerInRange)
        {
            AttackPlayer();
        }
        else if(navMesh.remainingDistance <= enemy.stopDis && !playerInRange)
        {
            enemy.SwitchState(enemy.idleState);
        }

        Collider[] foundPlayer = Physics.OverlapSphere(enemyObj.transform.position, enemy.sightRange * 1.25f, enemy.playerMask);

        foreach (var player in foundPlayer)
        {
            MovePlayer(player.transform);
            playerInRange = true;
        }

        if (foundPlayer.Length == 0)
        {
            playerInRange = false;
        }
    }

    public override void FixedUpdateState(EnemyStateManager enemy)
    {

    }

    void MovePlayer(Transform player)
    {
        navMesh.SetDestination(new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z));
    }

    void AttackPlayer()
    {
        if(Time.time >= nextAttack)
        {
            //Set Stats
            attackRange = enemyObj.GetComponent<EnemyManager>().enemyStats.attackRange;

            attackCD = 1 / (PlayerManager.acc.basePlayerStats.baseAtkSpeed + enemyObj.GetComponent<EnemyManager>().enemyStats.attackSpeed / 100);
            nextAttack = Time.time + attackCD;

            bool playerHit = Physics.CheckSphere(attackPoint.position, attackRange, enemyObj.GetComponent<EnemyStateManager>().playerMask);

            //for (int i = 0; i < enemiesHit.Length; i++)
            //{
            //    Debug.Log("YOU HIT: " + enemiesHit[i].name);
            //    PlayerManager.acc.TakeDamage(enemyObj.GetComponent<EnemyManager>().enemyStats.attackDamage);
            //}
            if (playerHit)
            {
                DebugManager.DebugLog("YOU HIT: Player", DebugType.ENEMYDEBUG);
                PlayerManager.acc.TakeDamage(enemyObj.GetComponent<EnemyManager>().enemyStats.attackDamage);
            }
        }
    }
}
