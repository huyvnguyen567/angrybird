using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AngryBirdAudio : MonoBehaviour
{
    [SerializeField] AudioClip hitAudio;
    
    private void Start()
    {
       
    }
    
    public AudioClip PlayAudioClip()
    {
        return hitAudio;
    }
}
