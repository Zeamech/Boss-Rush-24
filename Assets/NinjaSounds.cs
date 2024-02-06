using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NinjaSounds : MonoBehaviour
{
    [SerializeField] private AK.Wwise.Event laserSwordSlash;
    [SerializeField] private AK.Wwise.Event dash;
    [SerializeField] private AK.Wwise.Event groundStab;
    [SerializeField] private AK.Wwise.Event shuriken;

    public void PostDashEvent()
    {
        dash.Post(gameObject);
    }

    public void PostLaserEvent()
    {
        laserSwordSlash.Post(gameObject);
    }

    public void PostGroundStab()
    {
        groundStab.Post(gameObject);
    }

    public void PostShurikenEvent()
    {
        shuriken.Post(gameObject);
    }
}
