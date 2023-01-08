using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Technology
{
    public PlayerController player;

    Bullet bullet;
    float coolDownTime;
    float coolDownDuration;
    float energyCost;
    
    bool canShoot()
    {
        if(Time.time> coolDownTime && player.GetEnegy()>= energyCost)
        {
            return true;
        }
        else
            return false;
    }

    void Shoot()
    {

    }
    public void DestroyWeapon()
    {

    }
}
