using UnityEngine;
using UnityEngine.AI;

[System.Serializable]
public class EnemyAttackingState : EnemyBaseState
{
    NavMeshAgent navMesh;
    GameObject enemyObj;

    bool playerInRange;

    public override void EnterState(EnemyStateManager enemy)
    {
        enemyObj = enemy.gameObject;
        navMesh = enemyObj.GetComponent<NavMeshAgent>();
        Debug.LogWarning(enemyObj.name + ": Attacking State");
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
        Debug.Log("player Attacked");
    }
}
