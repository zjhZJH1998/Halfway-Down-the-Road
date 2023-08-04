using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyState
{
    moveToTarget,
    attack,
    idel
};

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private int ATK = 1;
    [SerializeField]
    private float ATKRange = 1.3f;
    [SerializeField]
    private float detectRange = 3;
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private int HP = 3;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform minionTarget;
    
    private Rigidbody2D rb;
    private Animator animator;

    // Attack cd
    public float coolTime = 2.5f;
    private float attackTimer;
    [SerializeField]
    private bool attackReady;
    private GameObject blade;

    private TextMesh tm;
    private SpriteRenderer heartIcon;

    private EnemyState est;

    private void Awake()
    {
        blade = transform.Find("Blade").gameObject;
        rb = GetComponent<Rigidbody2D>();
        tm = GetComponentInChildren<TextMesh>();
        heartIcon = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        est = EnemyState.idel;
        attackReady = true;
        attackTimer = coolTime;
    }

    private void Update()
    {
        StateHandler();
        AttackCoolDownHandler();
        UIHander();

        if (HP <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void StateHandler()
    {
        if (target == null)
        {
            est = EnemyState.idel;
        }
        else
        {
            est = EnemyState.moveToTarget;
        }

        if (est == EnemyState.moveToTarget)
        {
            MoveToTarget();
        }

        if (est == EnemyState.attack)
        {
            if (minionTarget != null || target != null)
                Attack();
            else
                est = EnemyState.idel;
        }
    }

    private void UIHander()
    {
        tm.text = HP.ToString();
    }

    private void AttackCoolDownHandler()
    {
        if (!attackReady)
        {
            attackTimer -= Time.deltaTime;
        }

        if (attackTimer < 0 && !attackReady)
        {
            attackReady = true;
            attackTimer = coolTime;
        }
    }


    private void MoveToTarget()
    {
        Vector3 diff, dir;

        if (minionTarget == null)
        {
            diff = target.position - transform.position;
            dir = diff.normalized;
            float dist = diff.magnitude;
            if(dist <= ATKRange)
            {
                est = EnemyState.attack;
            }
        }
        else
        {
            diff = minionTarget.position - transform.position;
            dir = diff.normalized;

        }


        Collider2D[] colliders;
        colliders = Physics2D.OverlapCircleAll(transform.position, detectRange);

        if(colliders.Length > 0)
        {
            foreach(Collider2D c in colliders)
            {
                if(c.transform.tag == "Minion")
                {
                    if(minionTarget == null)
                    {
                        minionTarget = c.transform;
                    }
                    else
                    {
                        float dist = (c.transform.position - transform.position).magnitude;
                        if (dist <= ATKRange)
                        {
                            minionTarget = c.transform;
                            est = EnemyState.attack;
                            rb.velocity = Vector2.zero;
                            return;
                        }
                    }
                }
            }
        }

        rb.velocity = dir * speed;
    }

    private void Attack()
    {
        Vector3 diff, dir;
        float distance = -1;
        if (minionTarget != null)
        {
            diff = minionTarget.position - transform.position;
            dir = diff.normalized;
            distance = diff.magnitude;
        }
        else
        {
            diff = target.position - transform.position;
            dir = diff.normalized;
            distance = diff.magnitude;
        }

        if (distance <= ATKRange)
        {

            if (attackReady)
            {
                if (!animator.GetBool("isAttack"))
                {
                    blade.GetComponent<BladeEnemy>().AwakeBlade(ATK, false);
                    animator.SetBool("isAttack", true);
                }
                attackReady = false;
            }

            rb.velocity = Vector2.zero;
        }
        else
        {
            rb.velocity = dir * speed;
        }
    }

    public void setTarget(Transform targetPos)
    {
        this.target = targetPos;
    }

    public void DebugAfterAnimationEnd()
    {
        animator.SetBool("isAttack", false);
        blade.GetComponent<BladeEnemy>().SleepBlade();
    }

    public void TakenDamage(int DMG)
    {
        HP -= DMG;
    }
}
