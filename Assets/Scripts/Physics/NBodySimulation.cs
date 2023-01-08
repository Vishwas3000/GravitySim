using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NBodySimulation : MonoBehaviour
{
    public GameObject CelestialBodiesParent;
    CelestialBodies[] bodies;
    private void Awake()
    {
        if(bodies == null)
            FindCelestialBodies();
        Time.fixedDeltaTime = Universe.physicsTimeStep;
    }

    public void FindCelestialBodies()
    {
        bodies = CelestialBodiesParent.GetComponentsInChildren<CelestialBodies>();
    }
    private void FixedUpdate()
    {
        Simulate(false);
    }

    public void InilizeBodies()
    {
        for (int i = 0; i < bodies.Length; i++)
        {
                bodies[i].InilizeBody();
        }
    }

    public void Simulate(bool isGhost, float timeStep = Universe.physicsTimeStep)
    {

        
        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdateVelocity(bodies, timeStep, isGhost);
        }
        for (int i = 0; i < bodies.Length; i++)
        {
            bodies[i].UpdatePosition(timeStep, isGhost);
        }
        if(isGhost)
        {
            for (int i = 0; i < bodies.Length; i++)
            {
                bodies[i].SetLineRenderer();
            }
        }
    }
}
