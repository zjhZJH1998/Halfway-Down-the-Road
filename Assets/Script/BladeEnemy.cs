using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BladeEnemy : MonoBehaviour
{
    private PolygonCollider2D collider;
    private int DMG;

    [SerializeField]
    private Vector3 initPos;
    private bool isAttack;
    private bool isReverse;


    private void Awake()
    {
        collider = GetComponent<PolygonCollider2D>();
        isAttack = false;
    }

    private void Update()
    {

        if (isAttack)
        {
            transform.RotateAroundLocal(new Vector3(0, 0, 1.0f), -5 * Time.deltaTime * (isReverse ? -1 : 1));
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    public void AwakeBlade(int DMG, bool isReverse)
    {
        collider.enabled = true;
        this.DMG = DMG;
        this.isReverse = isReverse;
        isAttack = true;
    }


    public void SleepBlade()
    {
        collider.enabled = false;
        isAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Minion")
        {
            collision.transform.GetComponent<MinionBase>().TakenDamage(DMG);
            if (collision.transform.GetComponent<MinionBase>().type == 1)
            {
                this.GetComponentInParent<EnemyBase>().TakenDamage(DMG);
            }
        }
        else if(collision.gameObject.tag == "Target")
        {
            EventCenter.Broadcast(EventDefine.WizardHurt);
        }
    }


}
