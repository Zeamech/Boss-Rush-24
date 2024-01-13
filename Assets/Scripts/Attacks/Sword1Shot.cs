using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "WeaponBehaviour/Sword/1Shot")]
public class Sword1Shot : AttackBehaviour
{
    public GameObject PlayerAttack;
    public AnimationClip anim1;

    public override void AttackCall(Transform transform)
    {
        GameObject spawn = Instantiate(PlayerAttack, transform);
        spawn.GetComponent<Animator>().Play(anim1.name);
        spawn.transform.parent = null;
    }

}
