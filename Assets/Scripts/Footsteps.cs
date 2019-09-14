using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    private AudioSource footstepSource;
    public AudioClip[] footstepClips;

    private void Start()
    {
        footstepSource = gameObject.AddComponent<AudioSource>();
    }

    public void setFootstepClip()
    {
        footstepSource.clip = footstepClips[(int)Random.Range(0, footstepClips.Length)];

    }
}
