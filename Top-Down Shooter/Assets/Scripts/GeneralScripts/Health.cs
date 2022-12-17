using UnityEngine;
using UnityEngine.Events;

public class Health : Progressive, IDamageable, IHealable
{
    [SerializeField]
    private UnityEvent OnDie;

    public void Damage(float amount)
    {
        Current -= amount;

        if(Current <= 0f)
        {
            Current = 0f;
            OnDie.Invoke();
        }
    }

    public void Heal(float amount)
    {
        Current += amount;

        if (Current > Initial)
            Current = Initial;
    }
}
