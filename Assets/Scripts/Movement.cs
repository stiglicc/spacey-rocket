using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float rotationSteer;
    [SerializeField] float rocketPower;
    [SerializeField] AudioClip thrustAudio;
    [SerializeField] ParticleSystem engineParticle;
    Rigidbody rb;
    AudioSource audioS;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioS = GetComponent<AudioSource>(); 
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Ground " && collision.gameObject.tag == "Obstacle" && collision.gameObject.tag == "Finish") {
            GetComponent<Movement>().enabled = false;
        }
    }
    void Update()
    {  
        Moving();
        Thrust();
    }
    private void Thrust() {
        if (Input.GetKey(KeyCode.Space)) {
           rb.AddRelativeForce(0, rocketPower * Time.deltaTime, 0);
            if (!audioS.isPlaying) {
                engineParticle.Play();
                audioS.PlayOneShot(thrustAudio);} 
        } else {
            engineParticle.Stop();
            audioS.Stop();
        }
    }   
        private void Moving() {
        float horizontalMovement = Input.GetAxis("Horizontal") * rotationSteer * Time.deltaTime;
        transform.Rotate(-horizontalMovement, 0, 0);  
        }
}
