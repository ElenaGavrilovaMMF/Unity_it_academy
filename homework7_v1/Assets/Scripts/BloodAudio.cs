using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodAudio : MonoBehaviour
{
    public AudioSource bloodAudio;
    public AudioClip bloodClip;
    public Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        bloodAudio.PlayOneShot(bloodClip);
        transform.position = startPos;
    }
    
   
}
