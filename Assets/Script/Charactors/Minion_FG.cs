using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_FG : MinionBase
{
    [SerializeField]
    private int ATK = 0;
    [SerializeField]
    private float ATKRange = 1.3f;
    [SerializeField]
    private float detectRange = 5;
    [SerializeField]
    private float speed = 1;
    [SerializeField]
    private float HP = 5;
    [SerializeField]
    private float maxHP = 5;

    // Attack factors
    public float coolTime = 5.0f;
    private float attackTimer;
    [SerializeField]
    private bool attackReady;
    public GameObject blade;


    private Rigidbody2D rb;
    private Animator animator;

    // UI
    public GameObject uiPanel;

    // upgrade
    [SerializeField]
    private bool upgrade_type1;

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
    //[SerializeField]
    //private AudioSource attackAudio;


    private void Awake()
    {
        type = 1;
        blade = transform.Find("Blade").gameObject;
        tm = GetComponentInChildren<TextMesh>();
        rb = GetComponent<Rigidbody2D>();
        //animator = GetComponent<Animator>();
        //attackAudio = GetComponent<AudioSource>();
        defender = GameObject.FindWithTag("Target").transform;
    }

    private void Start()
    {
        attackReady = true;
        attackTimer = coolTime;
        mst = MinionBaseState.patrolling;
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
                EventCenter.Broadcast(EventDefine.SummonCopy, transform);
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
            //if (!animator.GetBool("isAttack"))
            //{
                blade.GetComponent<Blade>().AwakeBlade(ATK, false);
                //animator.SetBool("isAttack", true);
                //attackAudio.Play();
            //}
            attackReady = false;
        }

        rb.velocity = Vector2.zero;
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
        //animator.SetBool("isAttack", false);
        blade.GetComponent<Blade>().SleepBlade();
    }

    public void initMinion(int type)
    {
        if(type ==5)
        {
            upgrade_type1 = true;
        }
    }

    public void InitSmallCopy()
    {
        HP = 9;
        maxHP = 9;
    }
}
