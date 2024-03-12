using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentScanner : MonoBehaviour
{

    public Color drawlinecolor;
    public float drawlinetime;

    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] Transform position1, position2;
    [SerializeField] float rotation1;

    [Header("Stairs ray")]
    [Tooltip("Checkaa stairsit")]
    [SerializeField] Transform rayTarget;
    [SerializeField] float rayMaxDistance;
    [SerializeField] LayerMask rayIgnoreMask;
    [SerializeField] string HitLayerName;

    [SerializeField] List<string> HitLayerList = new();

    public Animator animator;
    public float weightValue;
    public int animationLayer;


    void RayCastController()
    {
        //Vector3 direction = (position2.position - transform.position).normalized;
        Ray rayForward = new(transform.position, position2.position);

        float RayMaxDistanceEnd = position2.position.z;
        float RayMaxDistanceStart = position1.position.z;   
        rayMaxDistance = RayMaxDistanceEnd - RayMaxDistanceStart;

        HitLayerName = HitLayerList[0]; 

        LayerMask layermask = LayerMask.GetMask(HitLayerName);

       // Ray rayForward = new(transform.position, position2.position);
        if(Physics.Raycast(rayForward, out RaycastHit hit, rayMaxDistance, layermask))
        {
            // Get the layer of the hit object
            string hitLayerName = LayerMask.LayerToName(hit.collider.gameObject.layer);

            Debug.Log("Raycast hit: " + hit.collider.name);
            Debug.Log("Hit object's layer: " + hitLayerName);
            // gameObject.SetActive(false);    

           LineRendererController();
            animationLayer = 1;
            weightValue = 1;
            AnimationLayerController(animationLayer, weightValue);     
        }
        else
        {
            lineRenderer.enabled = false;
            animationLayer = 1;
            weightValue = 0;
            AnimationLayerController(animationLayer, weightValue);    
        }

    }

    void AnimationLayerController(int layer, float value)
    {
        if(animator)        animator.SetLayerWeight(layer, value);
    }
    void LineRendererController()
    {
        lineRenderer.enabled = true;    
        lineRenderer.SetPosition(0, position1.position);
      //  transform.rotation = Quaternion.Euler(rotation1, 0, 0);
        lineRenderer.SetPosition(1, position2.position);   
    }

    void Start()
    {
        lineRenderer.enabled = false;
    }


    void Update()
    {   

        
        RayCastController();    

    }
}
