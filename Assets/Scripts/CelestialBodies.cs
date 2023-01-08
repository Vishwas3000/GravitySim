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
    public Rigidbody rb;
    public List<Vector3> linePosition;
    public LineRenderer lineRenderer;
    public Material _material;

    [SerializeField]
    private Vector3 _initialVelocity;
    [SerializeField]
    private LineRenderer _lineRender;
    private Vector3 _currentVelocity;
    private Vector3 _currentSimVelocity;
    

    private void Awake()
    {
        InilizeBody();
    }
    private void Start()
    {
        _lineRender.enabled = false;
    }

    public void InilizeBody()
    {
        _currentVelocity = _initialVelocity;
        _currentSimVelocity= _currentVelocity;
        transform.localScale = Vector3.one*radius;
        linePosition = new List<Vector3>
        {
            transform.position
        };
        lineRenderer.startColor = _material.color;
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
                    _currentSimVelocity += acceleration * timeStep;
                }
            }
            else
            {

                if(otherBody!=this) 
                {
                    float sqrDst = (otherBody.rb.position - rb.position).sqrMagnitude;
                    Vector3 forceDir = (otherBody.rb.position - rb.position).normalized;
                    Vector3 force = forceDir * Universe.gravitationalConstant * mass * otherBody.mass / sqrDst;
                    Vector3 acceleration = force / mass;
                    _currentVelocity += acceleration * timeStep;

                }
            }
        }
    }
    public void UpdatePosition(float timeStep, bool isGhost)
    {
        if(isGhost)
        {
            linePosition.Add(_currentSimVelocity*timeStep + linePosition.Last());
            //Debug.Log(gameObject.name + " current position :" + linePosition.Last());
            return;
        }
        rb.position += _currentVelocity * timeStep;
    }
}
