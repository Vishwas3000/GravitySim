using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float energy { get; set; }
    public float health { get; set; }

    void Damage(float hitPoint)
    {
        health-=hitPoint;
        if(health <=0 )
        {
            health = 0;
            PlayerDead();
        }
    }
    void PlayerDead()
    {

    }
    public float GetEnegy()
    {
        return energy;
    }
    
}
