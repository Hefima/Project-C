using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateManager : MonoBehaviour
{
    EnemyBaseState currentState;

    [SerializeField]
    public EnemyAttackingState attackState = new EnemyAttackingState();
    [SerializeField]
    public EnemyIdleState idleState = new EnemyIdleState();

    //Enemy
    public float sightRange;
    public LayerMask playerMask;
    public Transform attackpoint;

    public float stopDis;

    void Start()
    {
        currentState = idleState;

        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void OnTriggerEnter(Collider other)
    {
        currentState.OnTriggerEnter(this, other);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }
}
