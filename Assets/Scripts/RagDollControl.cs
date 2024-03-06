using JetBrains.Annotations;
using UnityEngine;
using Cinemachine;

public class RagDollControl : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody[] _ragdollRigidbodies;
    private CharacterController _characterController;
    public Camera GoofyCamera;
    public CinemachineVirtualCamera ThirdPersonCamera;
    public CinemachineVirtualCamera RagdollCamera;

    Vector3 pushDirection;
    public float pushForce;

    private void Awake()
    {
        _ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        _animator = GetComponent<Animator>();
        _characterController = GetComponent<CharacterController>();

    }
    void Start()
    {
        RagdollCamera.gameObject.SetActive(false);
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

    void Pusher() {

        Vector3 pushDirection = Vector3.up;
       
        pushDirection.Normalize();
        _characterController.Move(pushDirection * pushForce * Time.deltaTime);

    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("RagDollActivator"))
        {
            EnableRagdoll();
            Pusher();
            ThirdPersonCamera.gameObject.SetActive(false);
            RagdollCamera.gameObject.SetActive(true);

            //GoofyCamera.gameObject.SetActive(true);
            gameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}
