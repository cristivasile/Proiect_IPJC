using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CoinBurst : MonoBehaviour
{
    public ParticleSystem burstPS;
    public SpriteRenderer sr;

    public UnityEvent<float> OnDestroy;

    public void Play()
    {
        var emission = burstPS.emission;
        var duration = burstPS.main.duration;

        sr.enabled = false;
        emission.enabled = true;
        burstPS.Play();

        OnDestroy?.Invoke(duration);
    }
}
