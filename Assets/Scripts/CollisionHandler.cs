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
    [Range(0, 1)]
    float _crashSoundVolume, _winSoundVolume;

    //Reference Caches
    Movement movementScript;
    AudioSource _audioSource;

    //States
    bool isTransitioning;

    private void Awake()
    {
        movementScript = GetComponent<Movement>();
        _audioSource = GetComponent<AudioSource>();
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
        //Add particle effect
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
        //Add particle effect
        movementScript.enabled = false;
        Invoke("LoadNextLevel", _levelLoadDelay);
    }
}
