using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class Campfire : Buildings
{
    TimeSystem timeSystem;
    [SerializeField] Light2D light2D;

    void Start()
    {
        timeSystem = TimeSystem.timeSystem;
    }
    void Update()
    {
        SetLightByTime();
    }

    void SetLightByTime()
    {
        int hour = timeSystem.hour;
        if(6 <= hour && hour <= 16)
        {
            light2D.intensity = 0.2f;
        }
        else
        {
            light2D.intensity = 0.5f;
        }
    }

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
