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
    public Transform startPos;
    public float sightRange;
    public LayerMask playerMask;
    public Transform attackpoint;
    public float stopDis;

    //HealthBar
    public Transform healthBarCanvas;

    void Start()
    {
        currentState = idleState;

        currentState.EnterState(this);
    }

    void Update()
    {
        currentState.UpdateState(this);
        HealthLookAtCam();
    }

    private void FixedUpdate()
    {
        currentState.FixedUpdateState(this);
    }

    public void SwitchState(EnemyBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    void HealthLookAtCam()
    {
        healthBarCanvas.LookAt(Camera.main.transform);
        healthBarCanvas.Rotate(0, 180, 0);
    }
}
