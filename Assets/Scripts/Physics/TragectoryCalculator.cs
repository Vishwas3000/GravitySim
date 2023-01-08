using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TragectoryCalculator : MonoBehaviour
{
    public NBodySimulation nBodySimulator;
    public float tragectoryStep = 0.5f;
    public int maxStep = 100;
    public bool autoUpdate = false;

    private void Awake()
    {
        nBodySimulator.FindCelestialBodies();
        
    }
    private void Update()
    {
        if(autoUpdate)
            CalculateTragectory();
    }
    public void CalculateTragectory()
    {

        nBodySimulator.InilizeBodies();
        for (int i = 0; i < maxStep; i++)
        {
            nBodySimulator.Simulate(true, tragectoryStep);
        }
    }
}
