using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;
    private const int cameraHeight = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position, Time.fixedDeltaTime * 10);
        transform.position = new Vector3(transform.position.x, transform.position.y, -cameraHeight);
    }
}
