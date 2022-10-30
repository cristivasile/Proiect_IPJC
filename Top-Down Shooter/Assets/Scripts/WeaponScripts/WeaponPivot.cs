using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPivot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.rotation = Utils.GetRelativeRotation(transform.position, Camera.main.ScreenToWorldPoint(Input.mousePosition));
    }
}
