using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 0f;
    [SerializeField] float mainrotate = 0f;
    [SerializeField] AudioClip mainEngine;
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem rightThrustParticles;
    [SerializeField] ParticleSystem leftThrustParticles;
    AudioSource audioSource;
    Rigidbody rb;
    MeshRenderer mr;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        processThrust();
        processRotate();
    }
    void processThrust()
    {
        if(Input.GetKey(KeyCode.W))
        {
            StartThrusting();
        }
        else
        {
            StopThrusting();
        }
    }
        private void processRotate()
    {
        if(Input.GetKey(KeyCode.A))
        {
            LeftRotating();
        }
        else if (Input.GetKey(KeyCode.D))
        {
            RightRotating();
        }
        else
        {
            StopRotating();
        }
    }

    private void StopThrusting()
    {
            audioSource.Stop();
        mainEngineParticles.Stop();
    }

    private void StartThrusting()
    {
        ApplyT(mainThrust);
        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(mainEngine);
        }
        if (!mainEngineParticles.isPlaying)
        {
            mainEngineParticles.Play();
        }
    }

    private void ApplyT(float thrustThisFrame)
    {
        rb.AddRelativeForce(Vector3.up * thrustThisFrame * Time.deltaTime);
    }
        private void LeftRotating()
    {
        ApplyR(-mainrotate);
        if (!rightThrustParticles.isPlaying)
        {
            rightThrustParticles.Play();
        }
    }
        private void RightRotating()
    {
        ApplyR(mainrotate);
        if (!leftThrustParticles.isPlaying)
        {
            leftThrustParticles.Play();
        }
    }
    private void StopRotating()
    {
        rightThrustParticles.Stop();
        leftThrustParticles.Stop();
    }
    private void ApplyR(float rotationThisFrame)
    {
        rb.freezeRotation = true;
        transform.Rotate(Vector3.back * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;
    }
}
