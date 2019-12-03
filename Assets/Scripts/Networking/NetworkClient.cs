using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SocketIO;

public class NetworkClient : SocketIOComponent
{
    [Header("Serialized Fields input REQUIRED")]
    [SerializeField] private GameObject prefabPlayer;
    [SerializeField] private GameObject LobbyCamera;
    [SerializeField] private GameObject PlayerHolder;

    [Header("Debugging Values")]
    private Dictionary<string, PlayerController> playerGameobjects = new Dictionary<string, PlayerController>();
    private GameObject self;
    private PlayerController selfController;


    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        setupNetworkCalls();
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();


    }

    private void setupNetworkCalls()
    {
        On("Open", (e) => {
            Debug.Log("Connection to Server Made");
        });

        On("init", (e) =>
        {
            Debug.Log("Id is: " + e.data["id"].ToString());
        });

        On("spawnSelf", (e) =>
        {
            Debug.Log("You have joined as player: " + e.data["id"]);
            GameObject player = createNewPlayer();
            selfController = player.GetComponent<PlayerController>();
            self = player;
            selfController.setLocal();
            selfController.init(e.data["id"].ToString());
            LobbyCamera.SetActive(false);
            playerGameobjects.Add(selfController.id, selfController);
        });

        On("spawnNew", (e) =>
        {
            Debug.Log("Spawning New PLayer");
            GameObject player = createNewPlayer();
            PlayerController controller = player.GetComponent<PlayerController>();
            controller.init(e.data["id"].ToString());
            playerGameobjects.Add(controller.id, controller);
        });
        On("removePlayer", (e) =>
        {
            Debug.Log("Player disconnected");
            PlayerController controller = playerGameobjects[e.data["id"].ToString()];
            Destroy(controller.gameObject);
        });

        On("updatePosition", (e) =>
        {
            PlayerController controller = playerGameobjects[e.data["id"].ToString()];
            Vector3 updatedPosition = new Vector3();
            updatedPosition.x = e.data["position"]["x"].f;
            updatedPosition.y = e.data["position"]["y"].f;
            updatedPosition.z = e.data["position"]["z"].f;
            Quaternion updatedRotation = new Quaternion();
            updatedRotation.x = e.data["lookRotation"]["x"].f;
            updatedRotation.y = e.data["lookRotation"]["y"].f;
            updatedRotation.z = e.data["lookRotation"]["z"].f;
            updatedRotation.w = e.data["lookRotation"]["w"].f;
            controller.updatePosition(updatedPosition, updatedRotation);
            
        });
    }

    private GameObject createNewPlayer()
    {
        return Instantiate(prefabPlayer, PlayerHolder.transform);
    }

    public void tickUpdate(NetworkedClass player)
    {
        Emit("updatePositioning", new JSONObject(JsonUtility.ToJson(player)));
    }
}
