using UnityEngine;

[CreateAssetMenu(fileName = "Weapons", menuName = "Weapon")]
public class RangedWeapon : ScriptableObject
{
    public float fireRate = 5.0f;
    public float recoil = 0.2f;
    public float bulletSpeed = 10.0f;
    public float damage = 10.0f;
    public float weaponOffset = 1.0f;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
