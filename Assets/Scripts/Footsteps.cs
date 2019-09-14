using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip[] footstepClips;

    public void setFootstepClip()
    {
        footstepSource.clip = footstepClips[(int)Random.Range(0, footstepClips.Length)];

    }
}
