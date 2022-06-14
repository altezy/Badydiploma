using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacticleController : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem sys;

    [SerializeField]
    private JetPackInput jetPackInput;

    private ThrustController thrustController;

    [SerializeField]
    private float minStartSpeed, maxStartSpeed;

    [SerializeField]
    private Vector3 minPosition, maxPosition;

    [SerializeField]
    private float minEmission, maxEmission;

    private ParticleSystem.EmissionModule sysEmit;

    private ParticleSystem.MainModule sysMain;

    private void OnEnable()
    {
        jetPackInput.EvtThrustInputChanged += SetStartSpeed;

        if (minPosition != maxPosition)
            jetPackInput.EvtThrustInputChanged += SetPosition;

        if (minPosition != maxPosition)
            jetPackInput.EvtThrustInputChanged += SetEmission;

        sysEmit = sys.emission;

        sysMain = sys.main;

    }

    private void SetStartSpeed(float thrustInPercent)
    {
        float speed = thrustInPercent * maxStartSpeed + (1f - thrustInPercent) * minStartSpeed;

        sysMain.startSpeed = speed;

    }

    private void SetPosition(float thrustInPercent)
    {
        Vector3 pos = thrustInPercent * maxPosition + (1f - thrustInPercent) * minPosition;

        sys.transform.localPosition = pos;
    }

    private void OnDisable()
    {
        thrustController.EvtThrustChanged -= SetStartSpeed;

        if (minPosition != maxPosition)
            ThrustController.EvtThrustChanged -= SetPosition;
    }

    [ContextMenu("SetMinPosition")]
    private void SetMinPosition()
    {
        minPosition = sys.transform.localPosition;
    }

    [ContextMenu("SetMaxPosition")]
    private void SetMaxPosition()
    {
        maxPosition = sys.transform.localPosition;
    }
}
