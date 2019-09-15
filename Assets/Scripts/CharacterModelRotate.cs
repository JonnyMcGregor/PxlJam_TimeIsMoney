using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterModelRotate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = -1 * Input.GetAxisRaw("Horizontal");
        float moveVertical = -1 * Input.GetAxisRaw("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        transform.rotation = Quaternion.LookRotation(movement);
    }
}
