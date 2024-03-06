using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagDollControl : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();    
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("RagDollActivator"))
        {
            animator.enabled = false;   
        }
    }
}
