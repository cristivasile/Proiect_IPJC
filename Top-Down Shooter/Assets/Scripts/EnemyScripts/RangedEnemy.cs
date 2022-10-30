using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{
    private static int instances = 0;

    public RangedEnemy()
    {
        instances++;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
