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



    void Start()
    {
     
    }
    private void Update()
    {
        if(sprintValue._speed == 4 && !coroutineHasStarted)
        {
            StopCoroutine(ChangeLayerWeight(1));
            layerIndex = 1;
           StartCoroutine(ChangeLayerWeight(1));
            coroutineHasStarted = true;
            Debug.Log("Getting tired");
        }
        if(sprintValue._speed == 0)
        {
            StopCoroutine(ChangeLayerWeight(1));
            layerIndex = 0;
            StartCoroutine(ChangeLayerWeight(1));
            Debug.Log("Feeling Goofy again");
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
