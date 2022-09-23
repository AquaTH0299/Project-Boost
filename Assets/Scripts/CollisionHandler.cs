using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] AudioClip success;
    [SerializeField] AudioClip crash;
    [SerializeField] ParticleSystem successParticles;
    [SerializeField] ParticleSystem crashParticles;
    AudioSource audioSource;
    int sceneCurrentIndex;
    bool isTransitioning = false;
    bool collisionDisable = false;
     void Start() 
    {
        audioSource = GetComponent<AudioSource>();  
    }
    void Update()
    {
        RespondToDebugKeys();
    }
    void RespondToDebugKeys()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Loadnextlevel();
        }
        else if(Input.GetKeyDown(KeyCode.Space))
        {
            collisionDisable = !collisionDisable;
        }
    }
    void OnCollisionEnter(Collision other)
    {
        if(isTransitioning || collisionDisable){return;}
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("This thing a Lauch Pad");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            default:
                StartCrashSequence();
                break;
        }
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(success);
        successParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("Loadnextlevel", levelLoadDelay);
    }
    void StartCrashSequence()
    {
        isTransitioning = true;
        audioSource.Stop();
        audioSource.PlayOneShot(crash);
        crashParticles.Play();
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadLevel", levelLoadDelay);
    }
    void Loadnextlevel()
    {
        sceneCurrentIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = sceneCurrentIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }
    void ReloadLevel()
    {
        SceneManager.LoadScene(sceneCurrentIndex);
    }
}
