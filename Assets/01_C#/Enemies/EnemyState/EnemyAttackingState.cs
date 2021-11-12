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
        Debug.LogWarning(enemyObj.name + ": Attacking State");

        navMesh.speed = navMesh.speed * 2;

        nextAttack = Time.time;
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
    }

    public override void FixedUpdateState(EnemyStateManager enemy)
    {
        Collider[] foundPlayer = Physics.OverlapSphere(enemyObj.transform.position, enemy.sightRange, enemy.playerMask);

        foreach (var player in foundPlayer)
        {
            MovePlayer(player.transform);
            playerInRange = true;
        }

        if (foundPlayer == null || foundPlayer.Length == 0)
        {
            playerInRange = false;
        }
    }

    public override void OnTriggerEnter(EnemyStateManager enemy, Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("FoundPlayer");
            MovePlayer(other.transform);
        }
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

            attackCD = 1 / (PlayerManager.acc.playerStats.baseAtkSpeed + enemyObj.GetComponent<EnemyManager>().enemyStats.attackSpeed / 100);
            nextAttack = Time.time + attackCD;

            bool playerHit = Physics.CheckSphere(attackPoint.position, attackRange, enemyObj.GetComponent<EnemyStateManager>().playerMask);

            //for (int i = 0; i < enemiesHit.Length; i++)
            //{
            //    Debug.Log("YOU HIT: " + enemiesHit[i].name);
            //    PlayerManager.acc.TakeDamage(enemyObj.GetComponent<EnemyManager>().enemyStats.attackDamage);
            //}
            if (playerHit)
            {
                Debug.Log("YOU HIT: Player");
                PlayerManager.acc.TakeDamage(enemyObj.GetComponent<EnemyManager>().enemyStats.attackDamage);
            }
        }
    }
}
