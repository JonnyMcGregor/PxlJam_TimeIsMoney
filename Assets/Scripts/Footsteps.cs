using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource footstepSource;
    public AudioClip footstep1, footstep2, footstep3, footstep4;
    private int clipNumber = 0;

    void setFootstepClip()
    {
        clipNumber = (int)Random.Range(1, 4);
    }
}
