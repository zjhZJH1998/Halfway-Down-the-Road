using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//test

public class Arrow : MonoBehaviour
{

    [SerializeField]
    private float speed = 20f;

    private int DMG;
    private Transform target;
    private Vector3 dir;

    private void Awake()
    {
        dir = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            dir = (target.position - transform.position).normalized;
            transform.Translate(dir * speed * Time.deltaTime);
        }
        else
        {
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }

    }

    private void Rotation()
    {
        Vector3 targ = target.position;
        targ.z = 0f;

        Vector3 objectPos = transform.position;
        targ.x = targ.x - objectPos.x;
        targ.y = targ.y - objectPos.y;

        float angle = Mathf.Atan2(targ.y, targ.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    public void SetTarget(Transform transform, int DMG)
    {
        target = transform;
        Rotation();
        this.DMG= DMG;

        //Vector3 rotateVector = target.position - transform.position;
        //Quaternion newRotation = Quaternion.LookRotation(rotateVector);
        //transform.rotation = Quaternion.RotateTowards(transform.rotation, newRotation, 100f * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.transform.GetComponent<EnemyBase>().TakenDamage(DMG);
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Enemy")
        {
            collision.transform.GetComponent<EnemyBase>().TakenDamage(DMG);
            Destroy(gameObject);
        }
    }
}
