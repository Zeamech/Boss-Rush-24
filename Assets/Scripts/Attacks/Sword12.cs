using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponBehaviour/Sword/12combo")]
public class Sword12 : AttackBehaviour
{
    public GameObject PlayerAttack;
    public AnimationClip anim1;
    public AnimationClip anim2;

    bool attackLeft;

    public override void AttackCall(Transform transform)
    {
        GameObject spawn = Instantiate(PlayerAttack, transform);

        if (!attackLeft)
        {
            spawn.GetComponent<Animator>().Play(anim1.name);
            spawn.transform.parent = null;
            attackLeft = !attackLeft;
        }
        else
        {
            spawn.GetComponent<Animator>().Play(anim2.name);
            spawn.transform.parent = null;
            attackLeft = !attackLeft;
        }
    }

}
