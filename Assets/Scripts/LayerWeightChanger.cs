using StarterAssets;
using System.Collections;
using UnityEngine;

public class LayerWeightChanger : MonoBehaviour
{
    public Animator animator;
    public int layerIndex;
    public float targetWeight;
    public float weightValue;
    public float newWeight;


    bool BlendStarted;

    Coroutine tiredWalkCoroutine;
    [SerializeField]
    ThirdPersonController sprintValue;

    float t = 0;
    float duration = 2.0f;



    void Start()
    {
        weightValue = 0f;
    }
    private void Update()
    {
        if (sprintValue._speed == 4 && !BlendStarted)
        {
            if (t < 1)
            {
                t += Time.deltaTime / duration;
                targetWeight = 1;
                //newWeight += 0.05f;
                newWeight = Mathf.Lerp(0, targetWeight, t);
                Debug.Log(newWeight);

                animator.SetLayerWeight(1, newWeight);
                //StopAllCoroutines();    
                //layerIndex = 1;
                //BlendStarted = true;
                //StartCoroutine(ChangeLayerWeight(1));

                Debug.Log("Getting tired");

            }
        }
        if (sprintValue._speed == 0)
        {
            if (t > 0)
            {
                t-= Time.deltaTime / duration;  

                targetWeight = 0;
                newWeight = Mathf.Lerp(1, targetWeight, t);
                animator.SetLayerWeight(1, 0);
                //StopAllCoroutines();
                ////layerIndex = 0;

                //StartCoroutine(ChangeLayerWeight(0));
                Debug.Log("Feeling Goofy again");
            }
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
            if (newWeight == targetWeight)
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
