using UnityEngine;

public class RagDollControl : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody[] _ragdollRigidbodies;
    private CharacterController _characterController;


    private void Awake()
    {
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

    }
    void Start()
    {

    }
    private void DisableRagdoll()
    {
        foreach(var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = true;
        }
        _animator.enabled = true;
        _characterController.enabled = true;    
    }
    private void EnableRagdoll()
    {
        foreach (var rigidbody in _ragdollRigidbodies)
        {
            rigidbody.isKinematic = false;
        }
        _animator.enabled = false;
        _characterController.enabled = false;
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("RagDollActivator"))
        {
            EnableRagdoll();
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}
