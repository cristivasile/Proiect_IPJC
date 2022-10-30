using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    private static int instances = 0;
    public static GameObject player;
    protected float health = 100f;

    protected BaseEnemy()
    {

    }

    // Start is called before the first frame update
    protected void Start()
    {
        instances++;
    }

    // Update is called once per frame
    protected void Update()
    {
        //face player
        transform.rotation = Utils.GetRelativeRotation(transform.position, player.transform.position);
    }

    /// <summary>
    /// Should be called on death.
    /// </summary>
    protected void Destroy()
    {
        Destroy(gameObject);
        instances--;
    }

    public static int GetNumberOfInstances()
    {
        return instances;
    }
}
