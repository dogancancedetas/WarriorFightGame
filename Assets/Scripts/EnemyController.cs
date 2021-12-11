using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EnemyState
{
    CHASE,
    ATTACK
}

public class EnemyController : MonoBehaviour
{
    private CharacterAnimations enemyAnim;
    private NavMeshAgent navAgent;
    private Transform playerTarget;

    public float moveSpeed = 3.5f;
    public float attackDistance = 1;
    public float chaseAfterAttackDistance = 1;
    private float waitBeforeAttackTime = 3;
    private float attackTimer;

    private EnemyState enemyState;
    public GameObject attackPoint;
    private CharacterSoundFX soundFX;

    void Awake()
    {
        enemyAnim = GetComponent<CharacterAnimations>();
        navAgent = GetComponent<NavMeshAgent>();
        playerTarget = GameObject.FindGameObjectWithTag(Tags.PLAYER_TAG).transform;
        soundFX = GetComponentInChildren<CharacterSoundFX>();
    }

    private void Start()
    {
        enemyState = EnemyState.CHASE;
        attackTimer = waitBeforeAttackTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyState == EnemyState.CHASE)
        {
            ChasePlayer();
        }
        if (enemyState == EnemyState.ATTACK)
        {
            AttackPlayer();
        }
    }

    void ChasePlayer()
    {
        navAgent.SetDestination(playerTarget.position);
        navAgent.speed = moveSpeed;

        if (navAgent.velocity.sqrMagnitude == 0)
        {
            enemyAnim.Walk(false);

        }
        else
        {
            enemyAnim.Walk(true);
        }
        if (Vector3.Distance(transform.position, playerTarget.position) <= attackDistance)
        {
            enemyState = EnemyState.ATTACK;
        }
    }

    void AttackPlayer()
    {
        navAgent.velocity = Vector3.zero;
        navAgent.isStopped = true;
        enemyAnim.Walk(false);
        attackTimer += Time.deltaTime;

        //if we can attack
        if (attackTimer > waitBeforeAttackTime)
        {
            if (Random.Range(0,2) > 0)
            {
                enemyAnim.Attack_1();
                soundFX.Attack1();
            }
            else
            {
                enemyAnim.Attack_2();
                soundFX.Attack2();

            }

            attackTimer = 0;
        }

        if (Vector3.Distance(transform.position, playerTarget.position) > attackDistance + chaseAfterAttackDistance)
        {
            navAgent.isStopped = false;
            enemyState = EnemyState.CHASE;
        }
    }

    void ActivateAttackPoint()
    {
        attackPoint.SetActive(true);
    }

    void DeactivateAttackPoint()
    {
        if (attackPoint.activeInHierarchy)
            attackPoint.SetActive(false);
    }
}
