using UnityEngine;
using UnityEngine.Networking;

public class HostGame : MonoBehaviour {

	[SerializeField]
	private uint roomSize = 2;

	private string roomName;

	private NetworkManager networkManager;

	void Start ()
	{
		networkManager = NetworkManager.singleton;

		if (networkManager.matchMaker == null)
		{
			networkManager.StartMatchMaker();
		}
	}

	public void setRoomName (string name)
	{
		roomName = name;
	}

	public void createRoom ()
	{
		if (roomName == "" || roomName == null) {
			roomName = "TestRoom";
		}
		Debug.Log ("Creating Room: " + roomName + " with room for " + roomSize + " players.");

		// Create room
		networkManager.matchMaker.CreateMatch (roomName, roomSize, true, "", "", "", 0, 0, networkManager.OnMatchCreate);
	}
}
