using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{

	public Transform enemy;

	public Vector3 point1;
	public Vector3 point2;

	public float speed = 1;

	bool toggle = true; 

	float t = 0;

    // Start is called before the first frame update
    void Start()
    {
        enemy.position = point1;
    }

    // Update is called once per frame
    void Update()
    {
       if(toggle){
       		enemy.position = Vector3.Lerp(point1, point2, t);
       }
       else{
			enemy.position = Vector3.Lerp(point2, point1, t);
       }

       enemy.rotation = Quaternion.Euler(0, toggle? 0 : 180, 0);

		t += speed*Time.deltaTime;

       if(t >= 1){
       			toggle = !toggle;
       			t = 0;
       	}
    }

    
}
