using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkedClass : MonoBehaviour
{
    public float x = 0;
    public float y = 0;
    public float z = 0;
    public float rotX = 0;
    public float rotY = 0;
    public float rotZ = 0;
    public float rotW = 0;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = transform.position.x;
        y = transform.position.y;
        z = transform.position.z;

        rotX = transform.rotation.x;
        rotY = transform.rotation.y;
        rotZ = transform.rotation.z;
        rotW = transform.rotation.w;

    }
}
