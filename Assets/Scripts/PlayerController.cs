using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Serialized Fields REQUIRED")]
    [SerializeField] private GameObject Camera;
    [SerializeField] private MovementController movementController;
    [SerializeField] private float interpolationSpeed = 0.4f;

    
    
    private NetworkClient networkClient;
    private NetworkedClass networkedClass;

    public bool isLocal = false;
    public string id;

    // Start is called before the first frame update
    void Start()
    {
        GameObject networkObject = GameObject.Find("NetworkHandler");
        networkClient = networkObject.GetComponent<NetworkClient>();
        networkedClass = GetComponent<NetworkedClass>();
        StartCoroutine(updateToServer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void init(string _id)
    {
        id = _id;
    }

    public void setLocal()
    {
        isLocal = true;
        Camera.SetActive(true);
        movementController.enabled = true;
    }


    private IEnumerator updateToServer ()
    {
        while (isLocal)
        {
            networkClient.tickUpdate(networkedClass);
            yield return new WaitForSeconds(.07f);
        }
    }

    public void updatePosition(Vector3 position, Quaternion rotation)
    {
        if (isLocal)
            return;
        transform.position = Vector3.Lerp(transform.position, position, interpolationSpeed);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, interpolationSpeed);

    }

}
