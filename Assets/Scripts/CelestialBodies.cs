using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CelestialBodies : MonoBehaviour
{
    public float mass;
    public float radius;
    public Rigidbody rigidbody;
    public List<Vector3> linePosition;
    public LineRenderer lineRenderer;
    public Material _material;

    [SerializeField]
    private Vector3 initialVelocity;
    private Vector3 currentVelocity;
    private Vector3 currentSimVelocity;

    public void Awake()
    {
        InilizeBody();
    }

    public void InilizeBody()
    {
        currentVelocity = initialVelocity;
        currentSimVelocity= currentVelocity;
        transform.localScale = Vector3.one*radius;
        //lineRenderer = new LineRenderer();
        linePosition = new List<Vector3>
        {
            transform.position
        };
        lineRenderer.SetColors(_material.color, _material.color);
    }

    public void SetLineRenderer()
    {
        lineRenderer.positionCount = linePosition.Count;
        for(int i=0; i<linePosition.Count; i++)
        {
            lineRenderer.SetPosition(i, linePosition[i]);
        }
        //linePosition.Clear();
        
    }

    public void UpdateVelocity(CelestialBodies[] allBodies, float timeStep, bool isGhost)
    {
        foreach(var otherBody in allBodies)
        {
            if(isGhost)
            {
                if(otherBody!=this)
                {
                    Vector3 distVec = otherBody.linePosition.Last() - linePosition.Last();
                    float sqrDst = (distVec).sqrMagnitude;
                    Vector3 forceDir = (distVec).normalized;
                    Vector3 force = forceDir * Universe.gravitationalConstant * mass * otherBody.mass / sqrDst;
                    Vector3 acceleration = force / mass;
                    currentSimVelocity += acceleration * timeStep;
                }
            }
            else
            {

                if(otherBody!=this) 
                {
                    float sqrDst = (otherBody.rigidbody.position - rigidbody.position).sqrMagnitude;
                    Vector3 forceDir = (otherBody.rigidbody.position - rigidbody.position).normalized;
                    Vector3 force = forceDir * Universe.gravitationalConstant * mass * otherBody.mass / sqrDst;
                    Vector3 acceleration = force / mass;
                    currentVelocity += acceleration * timeStep;

                }
            }
        }
    }
    public void UpdatePosition(float timeStep, bool isGhost)
    {
        if(isGhost)
        {
            linePosition.Add(currentSimVelocity*timeStep + linePosition.Last());
            //Debug.Log(gameObject.name + " current position :" + linePosition.Last());
            return;
        }
        rigidbody.position += currentVelocity * timeStep;
    }
}
