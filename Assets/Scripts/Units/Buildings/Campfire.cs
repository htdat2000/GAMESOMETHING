using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Campfire : Buildings
{
    public override void Interact()
    {
        Debug.Log("interact campfire");
    }

    protected override void Broken()
    {
        return;
    }

    public override void TakeDmg(int dmg)
    {
        return;
    }
    protected override void HPEqual0()
    {
        return;
    }
}
