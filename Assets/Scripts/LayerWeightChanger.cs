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
    public float weightValue;
    public float newWeight;
 
    public float duration;
    bool BlendStarted;

    Coroutine tiredWalkCoroutine;
    [SerializeField]
    ThirdPersonController sprintValue;



    void Start()
    {
     weightValue = 0f;  
    }
    private void Update()
    {
        if(sprintValue._speed == 4 && !BlendStarted)
        {
            targetWeight = 1;
            newWeight = Mathf.Lerp(0, targetWeight, 3f);

            animator.SetLayerWeight(0, newWeight);
            //StopAllCoroutines();    
            //layerIndex = 1;
            //BlendStarted = true;
            //StartCoroutine(ChangeLayerWeight(1));

            Debug.Log("Getting tired");
        }
        if(sprintValue._speed == 0)
        {
            targetWeight = 0;
            newWeight = Mathf.Lerp(1, targetWeight, 3f);
            animator.SetLayerWeight(1,newWeight);
            //StopAllCoroutines();
            ////layerIndex = 0;

            //StartCoroutine(ChangeLayerWeight(0));
            Debug.Log("Feeling Goofy again");
        }
    }
    void ChangeLayerWeights(float targetWeight)
    {
        float startTime = Time.time;
        float startWeight = animator.GetLayerWeight(layerIndex);
        while (Time.time < startTime + duration)
        {
            float t = (Time.time - startTime) / duration;
            float newWeight = Mathf.Lerp(startWeight, targetWeight, 10f);
            animator.SetLayerWeight(layerIndex, newWeight);
            if(newWeight==targetWeight)
            {
                BlendStarted = false;
                break;  
            }

        }
        


    }

    IEnumerator ChangeLayerWeight(float targetWeight)
    {
        float startTime = Time.time;
        float startWeight = animator.GetLayerWeight(layerIndex);
        while (Time.time < startTime + duration)
        {
            Debug.Log("Coroutine at work");
            float t = (Time.time - startTime) / duration;
            float newWeight = Mathf.Lerp(startWeight, targetWeight, t);
            animator.SetLayerWeight(layerIndex, newWeight);
            //if (newWeight == targetWeight)
            //{
            //    BlendStarted = false;
            //    break;
            //}
            yield return null; // This line is crucial! It tells Unity to wait until the next frame before continuing the loop.
        }
    }

    //IEnumerator ChangeLayerWeight(float targetWeight)
    //{
    //    float startTime = Time.time;
    //    float startWeight = animator.GetLayerWeight(layerIndex);
    //    while (Time.time < startTime + duration)
    //    {
    //        float t = (Time.time - startTime) / duration;
    //        float newWeight = Mathf.Lerp(startWeight, targetWeight, t); 
    //        animator.SetLayerWeight(layerIndex, newWeight);
    //        yield return null;

    //    }
    //    coroutineHasStarted = false;
    //    animator.SetLayerWeight(layerIndex, targetWeight);

    //}
}
