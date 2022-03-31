using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrashColliderHandler : MonoBehaviour
{
    [SerializeField] AudioClip crashAudio;
    [SerializeField] AudioClip finishAudio;
    [SerializeField] float levelDelay = 2f;
    [SerializeField] ParticleSystem crashParticle;
    [SerializeField] ParticleSystem finishParticle;

    bool colisionDisabled = false;
    bool isPlaying = false;
    AudioSource audioS;

    private void OnCollisionEnter(Collision collision)
    {
        if (isPlaying || colisionDisabled) { return; }
        
        switch (collision.gameObject.tag) {
            case "Start":
                Debug.Log("This is start");
                break;
            case "Finish":
                finishParticle.Play();
                newLevelSequence();
                break;
            default:
                crashSequence();
                break;
        }
    }

    void crashSequence() {
        crashParticle.Play();
        isPlaying = true;
        audioS.Stop();
        audioS.PlayOneShot(crashAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("ReloadScene", levelDelay);
    }

    void ReloadScene() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void newLevelSequence() {
        finishParticle.Play();
        isPlaying = true;
        audioS.Stop();
        audioS.PlayOneShot(finishAudio);
        GetComponent<Movement>().enabled = false;
        Invoke("NewLevel", levelDelay);
    }
    void NewLevel() {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentSceneIndex + 1;
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings) {
            nextSceneIndex = 0;
        }
        SceneManager.LoadScene(nextSceneIndex);
    }


    void Start()
    {
        audioS = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Cheats();
    }

    private void Cheats() {
        if (Input.GetKey(KeyCode.L)) {
            NewLevel();
        }
        if (Input.GetKey(KeyCode.C)) {
            colisionDisabled = !colisionDisabled;
        }
        if (Input.GetKey(KeyCode.Escape)) {
            Application.Quit();
        }
    }
} 
