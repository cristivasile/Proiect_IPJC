using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : BaseEnemy
{
    private static int instances = 0;

    public MeleeEnemy()
    {
        instances++;
    }


    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

        // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Should be called on death.
    /// </summary>
    void Destroy()
    {
        Destroy(gameObject);
        instances--;
    }

    public static int GetNumberOfInstances()
    {
        return instances;
    }
}
