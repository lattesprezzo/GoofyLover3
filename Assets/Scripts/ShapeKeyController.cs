using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeKeyController : MonoBehaviour
{
    public SkinnedMeshRenderer skinnedMeshRenderer;
    public string shapeKeyName;
    public float shapeSpeed = 1f;

    private int shapeKeyIndex;
    private float t = 0;



    void Start()
    {
        shapeKeyIndex = skinnedMeshRenderer.sharedMesh.GetBlendShapeIndex(shapeKeyName);    
    }


    void Update()
    {
        skinnedMeshRenderer.SetBlendShapeWeight(shapeKeyIndex, 1);
    }
}
