using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MinionState
{
    patrolling,
    attack,
    moveToEnemy,
    idel,
};

public class Minion : MonoBehaviour
{
    [SerializeField]
    private int ATK = 2;
    [SerializeField]
    private float ATKRange = 1.3f;
    [SerializeField]
    private float detectRange = 5;
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float HP = 10;

    // Attack cd
    public float coolTime = 1.0f;
    private float attackTimer;
    [SerializeField]
    private bool attackReady;

    private GameObject blade;

    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField]
    private float patrolRange = 20;
    private float patrolDist = 0;
    private TextMesh tm;

    [SerializeField]
    private Transform target;

    [SerializeField]
    private MinionState mst;

    private void Awake()
    {
        blade = transform.Find("Blade").gameObject;
        tm = GetComponentInChildren<TextMesh>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        attackReady = true;
        attackTimer = coolTime;
        mst = MinionState.patrolling;
    }

    private void Update()
    {
        StateHandler();

        AttackCoolDownHandler();

        UIHandler();

        if(HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void AttackCoolDownHandler()
    {
        if (!attackReady)
        {
            attackTimer -= Time.deltaTime;
        }

        if(attackTimer < 0 && !attackReady)
        {
            attackReady = true;
            attackTimer = coolTime;
        }
    }

    private void StateHandler()
    {
        if (mst == MinionState.idel)
        {

        }

        if (mst == MinionState.patrolling)
        {
                Patrolling();
        }

        if (mst == MinionState.attack)
        {
            if (target != null)
                Attack();
            else
                mst = MinionState.patrolling;
        }
    }

    private void UIHandler()
    {
        tm.text = HP.ToString();
    }

    private void Patrolling()
    {
        Vector3 diff, dir;
        float distance = -1;
        
        if(target == null)
        {
            rb.velocity = Vector2.right * speed;
        }
        else
        {
            diff = target.position - transform.position;
            dir = diff.normalized;
            distance = diff.magnitude;
            rb.velocity = dir * speed;
        }

        Collider2D[] colliders;
        colliders = Physics2D.OverlapCircleAll(transform.position, detectRange);
        if(colliders.Length > 0)
        {
            foreach(Collider2D c in colliders)
            {
                if(c.transform.tag == "Enemy")
                {
                    if (target == null)
                    {
                        target = c.transform;
                    }
                    else
                    {
                        float dist = (c.transform.position - transform.position).magnitude;
                        if (dist <= ATKRange)
                        {
                            target = c.transform;
                            mst = MinionState.attack;
                            rb.velocity = Vector2.zero;
                            return;
                        }
                    }
                }
            }
        }

        if(distance>0 && distance <= ATKRange)
        {
            mst = MinionState.attack;
        }

    }

    private void Attack()
    {
        Vector3 diff = target.position - transform.position;
        Vector3 dir = diff.normalized;
        float distance = diff.magnitude;
        if(distance > ATKRange)
        {
            mst = MinionState.patrolling;
            return;
        }


        if (attackReady)
        {
            if (!animator.GetBool("isAttack"))
            {
                blade.GetComponent<Blade>().AwakeBlade(ATK, false);
                animator.SetBool("isAttack", true);
            }
            attackReady = false;
        }

        rb.velocity = Vector2.zero;
    }

    public void DebugAfterAnimationEnd()
    {
        animator.SetBool("isAttack", false);
        blade.GetComponent<Blade>().SleepBlade() ;
    }

    public void TakenDamage(int DMG)
    {
        HP -= DMG;
    }

}
