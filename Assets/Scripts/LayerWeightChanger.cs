using StarterAssets;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LayerWeightChanger : MonoBehaviour
{
    public Animator animator;
    public int layerIndex;
    public float targetWeight;
    public float duration;
    bool coroutineHasStarted;

    Coroutine tiredWalkCoroutine;
    [SerializeField]
    ThirdPersonController sprintValue;

    private void OnEnable()
    {
   
        if (sprintValue._speed == 4) { 

        }
        
    }

    void Start()
    {
     
    }
    private void Update()
    {
        if(sprintValue._speed == 4 && !coroutineHasStarted)
        {
           StartCoroutine(ChangeLayerWeight(layerIndex));
            coroutineHasStarted = true;
            Debug.Log("Getting tired");
        }
    }

    IEnumerator ChangeLayerWeight(float targetWeight)
    {
        float startTime = Time.time;
        float startWeight = animator.GetLayerWeight(layerIndex);
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            float newWeight = Mathf.Lerp(startWeight, targetWeight, t); 
            animator.SetLayerWeight(layerIndex, newWeight);
            yield return null;

        }
        coroutineHasStarted = false;
        animator.SetLayerWeight(layerIndex, targetWeight);

    }
}
