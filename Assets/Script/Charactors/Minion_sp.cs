using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minion_sp : MinionBase
{
    [SerializeField]
    private int ATK = 1;
    [SerializeField]
    private float ATKRange = 1.0f;
    [SerializeField]
    private float detectRange = 5;
    [SerializeField]
    private float speed = 3;
    [SerializeField]
    private float angle = 0f;
    [SerializeField]
    private float HP = 5;
    [SerializeField]
    private float maxHP = 5;

    [SerializeField]
    private bool upgrade_type1;

    // Heal factors
    public float coolTime = 2.5f;
    private float HealTimer;
    
    [SerializeField]
    private bool healReady;

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

    private Collider2D collider;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        tm = GetComponentInChildren<TextMesh>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        attackAudio = GetComponent<AudioSource>();
        defender = GameObject.FindWithTag("Target").transform;
    }

    private void Start()
    {
        coolTime = 2.5f;
        HealTimer = coolTime;
        healReady = true;
        mst = MinionBaseState.patrolling;
    }

    private void Update()
    {
        Patrolling();

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


    private void Patrolling()
    {
        if (HealTimer > 0) HealTimer -= Time.deltaTime;
        else if(!healReady) healReady = true;

        angle += speed * Time.deltaTime;
        var offset = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * ATKRange;
        transform.position = defender.position + new Vector3(offset.x,offset.y,defender.position.z);
        if (upgrade_type1 && healReady )
        {
            HP = HP + 1  > maxHP? maxHP : HP + 1;
            healReady = false;
            HealTimer = coolTime;
        }
    } 

    public override void Attack()
    {

    }

    public override void StateHandler()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.transform.GetComponent<EnemyBase>().TakenDamage(ATK);
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

    public void initMinion(int type)
    {
        if(type == 4)
        {
            upgrade_type1 = true;
        }
    }

}
