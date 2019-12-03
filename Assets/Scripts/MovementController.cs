using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    [Header("Serialized Fields")]
    [SerializeField] private float Sensitivity = 1;
    [SerializeField] private Camera camera;
    [SerializeField] private float speed = 5.0f;


    private float rotY = 0;
    private Vector3 playerRotation = Vector3.zero;
    private float rotX;
    private Vector3 cameraRotation = Vector3.zero;

    private float inputX = 0;
    private float inputY = 0;
    private Vector3 movementHorizontal;
    private Vector3 movementVertical;
    private Vector3 velocity;

    private Rigidbody rigidBody;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputY = Input.GetAxis("Vertical");

        movementHorizontal = transform.right * inputX;
        movementVertical = transform.forward * inputY;

        velocity = (movementHorizontal + movementVertical).normalized * speed;


        //mouse movement 
        rotY = Input.GetAxisRaw("Mouse X");
        playerRotation = new Vector3(0, rotY, 0) * Sensitivity;

        rotX = Input.GetAxisRaw("Mouse Y");
        cameraRotation = new Vector3(rotX, 0, 0) * Sensitivity;

        //apply camera rotation

        //move the actual player here
        if (velocity != Vector3.zero)
        {
            rigidBody.MovePosition(rigidBody.position + velocity * Time.fixedDeltaTime);
        }

        if (playerRotation != Vector3.zero)
        {
            //rotate the camera of the player
            rigidBody.MoveRotation(rigidBody.rotation * Quaternion.Euler(playerRotation));
        }

        if (camera != null)
        {
            //negate this value so it rotates like a FPS not like a plane
            camera.transform.Rotate(-cameraRotation);
        }
    }
}
