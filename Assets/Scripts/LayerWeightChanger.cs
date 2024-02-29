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

    Coroutine tiredWalkCoroutine;

    void Start()
    {

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
        animator.SetLayerWeight(layerIndex, targetWeight);

    }
}
