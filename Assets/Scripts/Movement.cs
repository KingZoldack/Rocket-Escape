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

    [SerializeField]
    ParticleSystem _leftThruster, _rightThruster, _middleThruster;

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
            StartThrusting();
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            StopThrusting();
        }
    }

    private void StartThrusting()
    {
        _rb.AddRelativeForce(Vector3.up * thrustSpeed * Time.deltaTime);

        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(_mainThruster, _mainThrusterVolume);
        }

        if (!_middleThruster.isPlaying)
        {
            _middleThruster.Play();
        }
    }

    private void StopThrusting()
    {
        _audioSource.Stop();
        _middleThruster.Stop();
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            ProcessLeftTurn();
        }

        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            ProcessRightTurn();
        }

        else
        {
            StopRotations();
        }
    }

    private void ProcessLeftTurn()
    {
        ApplyRotation(rotateSpeed);

        if (!_leftThruster.isPlaying)
        {
            _leftThruster.Play();
        }
    }

    private void ProcessRightTurn()
    {
        ApplyRotation(-rotateSpeed);

        if (!_rightThruster.isPlaying)
        {
            _rightThruster.Play();
        }
    }

    private void StopRotations()
    {
        _leftThruster.Stop();
        _rightThruster.Stop();
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        _rb.freezeRotation = true; // Freezing rotation in order to manually rotate.
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        _rb.freezeRotation = false; //Unfreezing rotation so physics system can take over.
    }
}
