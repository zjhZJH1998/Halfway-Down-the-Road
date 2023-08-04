using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum EnemyBaseState
{
    moveToTarget,
    attack,
    idel,
    dodge,
    fleeing
}

public abstract class EnemyBase : MonoBehaviour
{
    public int  HP;
    public int MaxHP;
    public int ThreatValue;

    public bool isFleeing;

    public int Morale;


    public bool atkBuffered;

    // Start is called before the first frame update
    public abstract void UIHandler();
    public abstract void StateHandler();
    public abstract void Attack();
    public abstract void TakenDamage(int DMG);
    public abstract void Init(Transform targetPos,int threatValue);
    public abstract void set2Flee();
    public abstract void attackUp(int value);

    public abstract void slowDown();
}
