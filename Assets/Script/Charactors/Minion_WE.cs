using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_WE : MinionBase
{
    [SerializeField]
    private int ATK = 3;
    [SerializeField]
    private float ATKRange = 5f;
    [SerializeField]
    private float detectRange = 5;
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float HP = 10;
    [SerializeField]
    private float maxHP = 10;

    // Attack factors
    public float coolTime = 4.0f;
    private float attackTimer;
    [SerializeField]
    private bool attackReady;
    int impluseMaxNum;
    public GameObject spellBall;

    private bool upgrade_type1;

    private Rigidbody2D rb;
    private Animator animator;

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


    private void Awake()
    {
        tm = GetComponentInChildren<TextMesh>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackAudio = GetComponent<AudioSource>();
        defender = GameObject.FindWithTag("Target").transform;
    }

    private void Start()
    {
        attackReady = true;
        attackTimer = coolTime;
        mst = MinionBaseState.patrolling;
        impluseMaxNum = 3;
    }

    private void Update()
    {
        StateHandler();

        AttackCoolDownHandler();

        UIHandler();

        if (HP <= 0)
        {
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
            if (!animator.GetBool("isAttack"))
            {
                if(upgrade_type1)
                {
                    this.HP = maxHP;
                }

                Collider2D[]  colliders = Physics2D.OverlapCircleAll(transform.position, ATKRange);
                if(colliders.Length > 0)
                {
                    int impluseNum = impluseMaxNum;
                    foreach (Collider2D c in colliders)
                    {
                        if (c.transform == transform) continue;
                        if (impluseNum <= 0) break;
                        int isFlip = transform.localScale.x > 0 ? 1 : -1;
                        if (c.transform.tag == "Enemy")
                        {
                            GameObject go = Instantiate(spellBall, transform.position + new Vector3(0.6f * isFlip, 0,0) , Quaternion.identity);
                            go.GetComponent<SpellBall>().SetTarget(c.transform, ATK);
                            impluseNum--;
                        }
                        else if(c.transform.tag == "Minion" && c.transform.GetComponent<MinionBase>().canHeal())
                        {
                            GameObject go = Instantiate(spellBall, transform.position + new Vector3(0.6f * isFlip, 0, 0), Quaternion.identity);
                            go.GetComponent<SpellBall>().SetTarget(c.transform, ATK);
                            impluseNum--;
                        }
                    }
                }
                attackAudio.Play();
            }
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
        if(HP >= maxHP) HP = maxHP;
    }

    public void InitMinion(int type)
    {
        upgrade_type1 = true;
        maxHP += 2;
        HP = maxHP;
    }
}
