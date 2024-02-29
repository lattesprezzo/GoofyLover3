using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneRotator : MonoBehaviour
{
    public Transform spine;
    public float spineRotationValue;


    void Start()
    {
  
    }


    void Update()
    {
        spine.Rotate(40, 40, spineRotationValue);
    }
}
