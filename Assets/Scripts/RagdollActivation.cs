using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollActivation : MonoBehaviour
{
    private Rigidbody _mainRigidbody;
    private Collider _mainCollider;
    private Animator _animator;

    private Rigidbody[] _rigidbodies;
    private Collider[] _colliders;
    
    void Start()
    {
        _mainRigidbody = GetComponent<Rigidbody>();
        _mainCollider = GetComponent<Collider>();
        _animator = GetComponent<Animator>();

        _rigidbodies = GetComponentsInChildren<Rigidbody>();
        _colliders = GetComponentsInChildren<Collider>();
    }

    private void RigidbodiesKinematicDeactivate()
    {
        foreach (Rigidbody rigidbody in _rigidbodies)
        {
            rigidbody.isKinematic = false;
        }
    }

    private void CollidersActivate()
    {
        foreach (Collider collider in _colliders)
        {
            collider.enabled = true;
        }
    }

    public void Activate()
    {
        _mainRigidbody.isKinematic = true;
        _mainCollider.enabled = false;
        _animator.enabled = false;
        
        RigidbodiesKinematicDeactivate();
        CollidersActivate();
    }
}
