using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Golem : MonoBehaviour
{
    public GolemHand golemHand;
    public int golemDamege;

    private void Start()
    {
        golemHand.damage = golemDamege;
    }
}
