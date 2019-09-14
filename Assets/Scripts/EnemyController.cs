using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	public Transform enemy;

	public Transform point1;
	public Transform point2;

	public float speed = 1;

	bool toggle = true; 

	float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemy.position = point1.position;
    }

    // Update is called once per frame
    void Update()
    {
       if(toggle){
       		enemy.position = Vector3.Lerp(point1.position, point2.position, t);
       }
       else{
			enemy.position = Vector3.Lerp(point2.position, point1.position, t);
       }

		t += speed*Time.deltaTime;

       if(t >= 1){
       			toggle = !toggle;
       			t = 0;
       	}
    }
}
