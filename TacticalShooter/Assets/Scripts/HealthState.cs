using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthState : MonoBehaviour
{
    [SerializeField]
    protected float maxHealth = 100.0f;
    public float Health {
        get
        {
            return this.Health;
        }
        set
        {
            Health += value;
            // Clamp logic
            if (this.Health > maxHealth)
            {
                this.Health = maxHealth;
            }
            else if (this.Health < 0.0f)
            {
                Health = 0.0f;
            }
        }
    }
}
