using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class ThrustController : MonoBehaviour
{
    private Vector3 currentForce;

    [SerializeField]
    private JetPackInput jetPack01, jetPack02;

    [SerializeField]
    private Rigidbody rigidBody;

    [SerializeField]
    private float maxVelocity;

    [SerializeField]
    private float addForceMulti = 1f;

    private void OnEnable()
    {
        jetPack01.EvtThrustChanged += ChangeForce;
        jetPack02.EvtThrustChanged += ChangeForce;
    }

    public void ChangeForce(Vector3 newForce)
    {
        currentForce = (jetPack01.CurrentForce + jetPack02.CurrentForce) / 2f * addForceMulti;
    }

    public Vector3 TestVelocity;

    private void FixedUpdate()
    {
        rigidBody.AddForce(currentForce);
        Vector3 velocity = rigidBody.velocity;
        velocity = new Vector3(Mathf.Clamp(velocity.x, -maxVelocity, maxVelocity),
            Mathf.Clamp(velocity.y, -maxVelocity, maxVelocity),
            Mathf.Clamp(velocity.z, -maxVelocity, maxVelocity));

        rigidBody.velocity = velocity;
        TestVelocity = velocity;
    }
}
