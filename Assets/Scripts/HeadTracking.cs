using Cinemachine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class HeadTracking : MonoBehaviour
{
    public float Radius; // milloin LookAt aktivoituu
    public float RetargetSpeed;
    [SerializeField] CinemachineVirtualCamera characterCamera;
    public float MaxAngle;
    public Transform Target;
    public Rig Headrig;
    float RadiusSqr;

    [SerializeField] List<PointOfInterest> POIs;

    void Start()
    {
        POIs = FindObjectsOfType<PointOfInterest>().ToList();
        RadiusSqr = Radius * Radius;
    }


    void Update()
    {
        Transform tracking = null; // Luodaan lennossa tyhjä positio, mikä ei näy pelaajalle

        foreach (PointOfInterest poi in POIs)
        {// Etäisyys poin ja Playerin väliltä:
            Vector3 delta = poi.transform.position - transform.position;
            if (delta.sqrMagnitude < RadiusSqr)
            {
                float angle = Vector3.Angle(transform.forward, delta);
                if (angle < MaxAngle)
                {
                    tracking = poi.transform; // Asetetaan tyhjä positio poi:n positioksi
                    break;
                }
            }
        }

        float rigWeight = 0;
        Vector3 targetPos = transform.position + (transform.up * 2f) + (transform.forward * 2f);


        if (tracking != null) // Tracker-pallo on kiinni kohteessa
        {
            targetPos = tracking.position;
            rigWeight = 1;
        }
        else
        {
            float angle = Vector3.Angle(transform.forward, characterCamera.transform.forward);

            if (angle < MaxAngle)
            {
                targetPos = transform.position + characterCamera.transform.forward;
                rigWeight = 1;  
            }
        }
        Target.position = Vector3.Lerp(Target.position, targetPos, Time.deltaTime * RetargetSpeed);
        Headrig.weight = Mathf.Lerp(Headrig.weight, rigWeight, Time.deltaTime * 2);

    }
}
