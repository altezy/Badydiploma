using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JetPackInput : MonoBehaviour
{
    [SerializeField]
    private OVRInput.Controller m_controller = OVRInput.Controller.None;

    [SerializeField]
    private Transform engine, fan;

    [SerializeField]
    private float minFanRotationSpeed, maxFanRotationSpeed;

    private float currentFanRotationSpeed;

    private float currentThrustInput;

    private Vector3 currentThrust;

    public Vector3 CurrentForce { get => currentThrust;  }

    [SerializeField]
    private float engineRotationSpeed;


    public event Action<Vector3> EvtThrustChanged = delegate { };
    public event Action<float> EvtThrustInputChanged = delegate { };

    private void Start()
    {
        ThrustInputChanged(0f);
    }

    private void Update()
    {
        float newInput = OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller);

        if(newInput != currentThrustInput)
        {
            ThrustInputChanged(newInput);
        }
        if (OVRInput.Get(OVRInput.Button.One, m_controller))
            engine.Rotate(new Vector3(engineRotationSpeed * Time.deltaTime, 0f, 0f));

        else if (OVRInput.Get(OVRInput.Button.Two, m_controller))
            engine.Rotate(new Vector3(-engineRotationSpeed * Time.deltaTime, 0f, 0f));

        fan.Rotate(new Vector3(0f, 0f, currentFanRotationSpeed * Time.deltaTime));
    }
    public void ThrustInputChanged(float newInput)
    {
        currentThrustInput = newInput;
        currentThrust = newInput * engine.forward;

        currentFanRotationSpeed = newInput * maxFanRotationSpeed + (1f - newInput) * minFanRotationSpeed;

        EvtThrustInputChanged(newInput);
        EvtThrustChanged(currentThrust);
    }
}
