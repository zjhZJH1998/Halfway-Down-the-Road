using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellBall : MonoBehaviour
{

    [SerializeField]
    private float speed = 5f;

    private int DMG;
    private Transform target;
    private Vector3 dir;

    private bool isInit;

    // Start is called before the first frame update
    private void Awake()
    {
        dir = Vector3.zero;
        isInit = false;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            dir = (target.position - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }

        if(isInit && target == null)
        {
            Destroy(gameObject);
        }
        
    }

    public void SetTarget(Transform transform, int DMG)
    {
        target = transform;
        this.DMG = DMG;
        isInit = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.transform.GetComponent<EnemyBase>().TakenDamage(DMG);
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Minion" && collision.transform.GetComponent<MinionBase>().canHeal())
        {
            collision.transform.GetComponent<MinionBase>().Heal(DMG/2);
            Destroy(gameObject);
        }
    }
}
