using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blade : MonoBehaviour
{
    private PolygonCollider2D collider;
    private int DMG;

    [SerializeField]
    private Vector3 initPos;
    private bool isAttack;
    private bool isReverse;

    public float rotateSpeed;

    private void Awake()
    {
        collider = GetComponent<PolygonCollider2D>();
        isAttack = false;
    }

    private void Update()
    {

        if (isAttack)
        {
            transform.RotateAroundLocal(new Vector3(0, 0, 1.0f), -rotateSpeed * Time.deltaTime * (isReverse ? -1 : 1));
        }
        else
        {
            transform.rotation = Quaternion.identity;
        }
    }

    public void AwakeBlade(int DMG,bool isReverse)
    {
        collider.enabled = true;
        this.DMG = DMG;
        this.isReverse = isReverse;
        isAttack = true;
    }

    public void SetBladeSpeed(float speed)
    {
        this.rotateSpeed = speed;
    }
    public void SleepBlade()
    {
        collider.enabled = false;
        isAttack = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.transform.GetComponent<EnemyBase>().TakenDamage(DMG);
        }
    }


}
