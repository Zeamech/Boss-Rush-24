using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponBehaviour/Sword/1Shot")]
public class Sword1Shot : AttackBehaviour
{
    public GameObject PlayerAttack;
    public AnimationClip anim1;
    public float attackDuration = 0.3f;

    public override void AttackCall(Transform transform)
    {

        GameObject spawn = Instantiate(PlayerAttack, transform);
        spawn.GetComponent<Animator>().Play(anim1.name);
        spawn.GetComponent<AttackController>().LifeTime = anim1.length;
        spawn.transform.parent = null;


    }

    public override float AttackDuration()
    {
        return attackDuration;
    }

}
