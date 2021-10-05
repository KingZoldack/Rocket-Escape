using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    //Config Params
    [SerializeField]
    AudioClip _mainThruster;

    [SerializeField] [Range(0, 1)]
    float _mainThrusterVolume;

    [SerializeField]
    float thrustSpeed, rotateSpeed;

    //Reference Caches
    Rigidbody _rb;
    AudioSource _audioSource;

    

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            _rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);
            
            if (!_audioSource.isPlaying)
            {
                _audioSource.PlayOneShot(_mainThruster, _mainThrusterVolume);
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            _audioSource.Stop();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ApplyRotation(rotateSpeed);
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ApplyRotation(-rotateSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        _rb.freezeRotation = true; // Freezing rotation in order to manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rb.freezeRotation = false; //Unfreezing rotation so physics system can take over.
    }
}
