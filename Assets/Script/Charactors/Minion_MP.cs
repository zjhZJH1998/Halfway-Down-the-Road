using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_MP : MinionBase
{
    [SerializeField]
    private int ATK = 2;
    [SerializeField]
    private float ATKRange = 2.0f;
    [SerializeField]
    private float detectRange = 5;
    [SerializeField]
    private float speed = 2;
    [SerializeField]
    private float HP = 15;
    [SerializeField]
    private float maxHP = 15;

    // Attack factors
    public float coolTime = 4.0f;
    private float attackTimer;
    [SerializeField]
    private bool attackReady;
    private GameObject blade;

    // baisc Components
    private SpriteRenderer spr;
    private Rigidbody2D rb;
    private Animator animator;


    private bool upgrade_type1;

    // UI
    public GameObject uiPanel;

    [SerializeField]
    private float patrolRange = 20;
    private float patrolDist = 0;
    private TextMesh tm;

    [SerializeField]
    private Transform target;
    private Transform defender;

    [SerializeField]
    private MinionBaseState mst;
    //audio source
    [SerializeField]
    private AudioSource attackAudio;

    [SerializeField]
    private float overLoadValue;

    private void Awake()
    {
        blade = transform.Find("Blade").gameObject;
        tm = GetComponentInChildren<TextMesh>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackAudio = GetComponent<AudioSource>();
        spr = GetComponent<SpriteRenderer>();
        defender = GameObject.FindWithTag("Target").transform;
    }

    private void Start()
    {
        attackReady = true;
        attackTimer = coolTime;
        mst = MinionBaseState.patrolling;
        overLoadValue = 0;
    }

    private void Update()
    {
        StateHandler();

        AttackCoolDownHandler();

        UIHandler();

        if (HP <= 0)
        {
            if(upgrade_type1)
            {
                Collider2D[] colliders;
                colliders = Physics2D.OverlapCircleAll(transform.position, ATKRange);
                foreach (Collider2D c in colliders)
                {
                    if (c.transform.tag == "Enemy")
                    {
                        c.gameObject.GetComponent<EnemyBase>().TakenDamage(3);
                    }
                }
            }
             Destroy(gameObject);
        }

        if (target != null)
        {
            if (target.position.x < transform.position.x)
            {
                transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
                uiPanel.transform.localScale = new Vector3(-1, transform.localScale.y, transform.localScale.z);
            }

            else
            {
                transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
                uiPanel.transform.localScale = new Vector3(1, transform.localScale.y, transform.localScale.z);
            }
        }

        OverLoadUpdate();

        GetComponent<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, overLoadValue / 10);

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
            if (overLoadValue == 10)
                attackTimer = 1.0f;
            else
                attackTimer = coolTime - (overLoadValue * 0.3f);

        }
    }

    private void Patrolling()
    {
        Vector3 diff, dir;
        float distance = -1;

        if (target == null)
        {
            rb.velocity = (defender.position - transform.position).normalized * speed * 0.5f;
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
        if (colliders.Length > 0)
        {
            foreach (Collider2D c in colliders)
            {
                if (c.transform.tag == "Enemy")
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
                            mst = MinionBaseState.attack;
                            rb.velocity = Vector2.zero;
                            return;
                        }
                    }
                }
            }
        }

        if (distance > 0 && distance <= ATKRange)
        {
            mst = MinionBaseState.attack;
        }

    }

    public override void Attack()
    {
        Vector3 diff = target.position - transform.position;
        Vector3 dir = diff.normalized;
        float distance = diff.magnitude;
        if (distance > ATKRange)
        {
            mst = MinionBaseState.patrolling;
            return;
        }


        if (attackReady)
        {
            if (!animator.GetBool("isAttack"))
            {
                if(overLoadValue < 10)
                    overLoadValue++;
                blade.GetComponent<Blade>().AwakeBlade(ATK, true);
                animator.SetBool("isAttack", true);
                attackAudio.Play();
            }
            attackReady = false;
        }

        rb.velocity = Vector2.zero;
    }

    private void OverLoadUpdate()
    {
        if(mst == MinionBaseState.idel || mst == MinionBaseState.patrolling)
        {
            if(overLoadValue > 0)
                overLoadValue -= Time.deltaTime;
        }


        //spr.color = new Color(255, 255 - overLoadValue * 25.5f, 255 - overLoadValue * 25.5f);
        //Debug.Log(spr.color);
    }

    public override void StateHandler()
    {
        if (mst == MinionBaseState.idel)
        {

        }

        if (mst == MinionBaseState.patrolling)
        {
            Patrolling();
        }

        if (mst == MinionBaseState.attack)
        {
            if (target != null)
                Attack();
            else
                mst = MinionBaseState.patrolling;
        }
    }

    public override void TakenDamage(int DMG)
    {
        HP -= DMG;
    }

    public override void UIHandler()
    {
        tm.text = HP.ToString();
    }

    public override bool canHeal()
    {
        if (maxHP <= HP) return false;
        else return true;
    }
    public override void Heal(int value)
    {
        HP += value;
        if (HP >= maxHP) HP = maxHP;
    }

    public void DebugAfterAnimationEnd()
    {
        animator.SetBool("isAttack", false);
        blade.GetComponent<Blade>().SleepBlade();
    }

    public void initMinion(int type)
    {
        if(type == 2) // mp_1
        {
            upgrade_type1 = true;
        }
    }

}
