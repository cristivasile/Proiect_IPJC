using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseEnemy : MonoBehaviour
{
    public static GameObject player;
    protected float health = 100f;

    // Start is called before the first frame update
    protected void Start()
    {
    }

    // Update is called once per frame
    protected void Update()
    {
        //face player
        transform.rotation = Utils.GetRelativeRotation(transform.position, player.transform.position);
    }
}
