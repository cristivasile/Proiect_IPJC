using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailScript : MonoBehaviour
{
    public PlayerVisuals playerVisuals;
    Color playerColor;
    TrailRenderer trailRenderer;


    // Start is called before the first frame update
    void Start()
    {
        playerColor = playerVisuals.GetPlayerColor();
        trailRenderer = GetComponent<TrailRenderer>();

        trailRenderer.startColor = playerColor;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
