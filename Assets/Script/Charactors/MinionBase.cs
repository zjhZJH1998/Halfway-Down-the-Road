using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum MinionBaseState
{
    patrolling,
    attack,
    idel,
};

public abstract class MinionBase : MonoBehaviour
{

    public int type = 0;

    public abstract void UIHandler();
    public abstract void StateHandler();
    public abstract void Attack();
    public abstract void TakenDamage(int DMG);

    public abstract bool canHeal();
    public abstract void Heal(int value);




}
