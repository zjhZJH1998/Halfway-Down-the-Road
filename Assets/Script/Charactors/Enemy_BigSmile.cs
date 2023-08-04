using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BigSmile : EnemyBase
{

    [SerializeField]
    private int ATK = 1;
    [SerializeField]
    private float ATKRange = 1.5f;
    [SerializeField]
    private float detectRange = 7;
    [SerializeField]
    private float speed = 3;

    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform minionTarget;

    // basic components
    private SpriteRenderer spr;
    private Rigidbody2D rb;
    private Animator animator;

    // UI
    public GameObject uiPanel;

    // Attack cd
    public float coolTime = 2.5f;
    private float attackTimer;
    [SerializeField]
    private bool attackReady;
    private GameObject blade;

    private TextMesh tm;
    private SpriteRenderer heartIcon;

    private EnemyBaseState est;

    [SerializeField]
    private GameObject smallSmile;

    private bool isSlowDown;
    private void Awake()
    {
        Morale = Random.Range(40 - ThreatValue * 10, 50 - ThreatValue * 10); ;
        blade = transform.Find("Blade").gameObject;
        rb = GetComponent<Rigidbody2D>();
        tm = GetComponentInChildren<TextMesh>();
        heartIcon = GetComponentInChildren<SpriteRenderer>();
        spr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        est = EnemyBaseState.idel;
        attackReady = true;
        attackTimer = coolTime;

    }

    private void Update()
    {
        UIHandler();
        StateHandler();
        AttackCoolDownHandler();


        if (minionTarget != null)
        {
            if (minionTarget.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                uiPanel.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
            else
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                uiPanel.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }
        }
        else
        {
            if (target != null)
            {
                if (target.position.x < transform.position.x)
                {
                    transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                    uiPanel.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                }
                else
                {
                    transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                    uiPanel.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                }

            }
        }
    }

    public override void UIHandler()
    {
        tm.text = HP.ToString();
    }

    public override void StateHandler()
    {
        if (est == EnemyBaseState.fleeing)
        {
            Fleeing();
            return;
        }

        if (target == null && est != EnemyBaseState.fleeing)
        {
            est = EnemyBaseState.idel;
        }
        else
        {
            est = EnemyBaseState.moveToTarget;
        }

        if (est == EnemyBaseState.moveToTarget && est != EnemyBaseState.fleeing)
        {
            MoveToTarget();
        }

        if (est == EnemyBaseState.attack && est != EnemyBaseState.fleeing)
        {
            if (minionTarget != null || target != null)
                Attack();
            else
                est = EnemyBaseState.idel;
        }


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
    private void Fleeing()
    {
        if (minionTarget != null)
        {
            Vector3 dir = transform.position - minionTarget.position;
            rb.velocity = dir.normalized * speed * (isSlowDown ? 0.5f : 1.0f); 

        }
        else
        {
            Vector3 dir = transform.position - target.position;
            rb.velocity = dir.normalized * speed * (isSlowDown ? 0.5f : 1.0f); 

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
            if (dist <= ATKRange)
            {
                est = EnemyBaseState.attack;
            }
        }
        else
        {
            diff = minionTarget.position - transform.position;
            dir = diff.normalized;

        }


        Collider2D[] colliders;
        colliders = Physics2D.OverlapCircleAll(transform.position, detectRange);

        if (colliders.Length > 0)
        {
            foreach (Collider2D c in colliders)
            {
                if (c.transform.tag == "Minion")
                {
                    if (minionTarget == null)
                    {
                        minionTarget = c.transform;
                    }
                    else
                    {
                        float dist = (c.transform.position - transform.position).magnitude;
                        if (dist <= ATKRange)
                        {
                            minionTarget = c.transform;
                            est = EnemyBaseState.attack;
                            rb.velocity = Vector2.zero;
                            return;
                        }
                    }
                }
            }
        }

        rb.velocity = dir * speed * (isSlowDown ? 0.5f : 1.0f); 
    }

    public override void Attack()
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
            rb.velocity = dir * speed * (isSlowDown ? 0.5f : 1.0f); 
        }
    }

    public override void Init(Transform targetPos, int threatValue)
    {
        this.target = targetPos;

        // threat config
        this.ThreatValue = threatValue;
        HP = 6 + threatValue - 1;
        MaxHP = 6 + threatValue - 1;
        ATK = 1 + (threatValue >= 30 ? 1 :0 ) ;
    }

    public void DebugAfterAnimationEnd()
    {
        animator.SetBool("isAttack", false);
        blade.GetComponent<BladeEnemy>().SleepBlade();
    }

    public override void TakenDamage(int DMG)
    {
        HP -= DMG;
        if (HP > MaxHP) HP = MaxHP;
        EventCenter.Broadcast(EventDefine.GenerateSmallSmile,this.transform.position);
    }

    public override void set2Flee()
    {
        isFleeing = true;
        est = EnemyBaseState.fleeing;
    }

    public override void attackUp(int value)
    {
        if (!atkBuffered)
        {
            atkBuffered = true;
            ATK += value;
        }
    }
    public override void slowDown()
    {
        isSlowDown = true;
    }
}
