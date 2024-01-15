using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponBehaviour/Sword/12combo")]
public class Sword12 : AttackBehaviour
{
    public GameObject PlayerAttack;
    public AnimationClip anim1;
    public AnimationClip anim2;
    public float attackDuration1 = 0.3f;
    public float attackDuration2 = 0.3f;

    bool attackLeft = false;

    public override void AttackCall(Transform transform)
    {
        GameObject spawn = Instantiate(PlayerAttack, transform);

        if (attackLeft)
        {
            spawn.GetComponent<Animator>().Play(anim1.name);
            spawn.transform.parent = null;
            spawn.GetComponent<AttackController>().LifeTime = anim1.length;
            attackLeft = !attackLeft;
        }
        else
        {
            spawn.GetComponent<Animator>().Play(anim2.name);
            spawn.transform.parent = null;
            spawn.GetComponent<AttackController>().LifeTime = anim2.length;
            attackLeft = !attackLeft;
        }
    }

    public override float AttackDuration()
    {
        if(attackLeft)
            return attackDuration1;
        else
            return attackDuration2;
    }
}
