using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    //Config Params
    [SerializeField]
    float _levelLoadDelay = 1.5f;

    [SerializeField]
    AudioClip _crashSound, _winSound;

    [SerializeField]
    ParticleSystem _winParticles, _crashParticles;

    [SerializeField]
    [Range(0, 1)]
    float _crashSoundVolume, _winSoundVolume;

    //Reference Caches
    Movement movementScript;
    AudioSource _audioSource;
    Collider[] _colliders;

    //States
    bool isTransitioning, collidersOff = true;


    private void Awake()
    {
        movementScript = GetComponent<Movement>();
        _audioSource = GetComponent<AudioSource>();
        _colliders = GetComponentsInChildren<Collider>();
    }
    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }

        switch (other.gameObject.tag)
        {

            case "Friendly":
                Debug.Log("This is s a friendly object");
                break;
            case "Finished":
                StartWinSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }

    private void Update()
    {
        DebugKeys();
    }

    private void DebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }

        if (Input.GetKeyDown(KeyCode.C) && collidersOff)
        {
            foreach (var collider in _colliders)
            {
                collider.enabled = false;
            }

            collidersOff = false;
        }

        else if (Input.GetKeyDown(KeyCode.C) && !collidersOff)
        {
            foreach (var collider in _colliders)
            {
                collider.enabled = true;
            }

            collidersOff = true;
        }
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void StartCrashSequence()
    {
        isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_crashSound, _crashSoundVolume);
        _crashParticles.Play();
        movementScript.enabled = false;
        Invoke("ReloadLevel", _levelLoadDelay);
    }

    void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;

        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }

        SceneManager.LoadScene(nextSceneIndex);
    }

    void StartWinSequence()
    {
        isTransitioning = true;
        _audioSource.Stop();
        _audioSource.PlayOneShot(_winSound, _winSoundVolume);
        _winParticles.Play();
        movementScript.enabled = false;
        Invoke("LoadNextLevel", _levelLoadDelay);
    }
}
