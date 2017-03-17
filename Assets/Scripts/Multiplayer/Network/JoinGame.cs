using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using System;

public class JoinGame : MonoBehaviour
{

    NetworkManager networkManager;

    List<GameObject> roomList = new List<GameObject>();

    [SerializeField]
    Text status;

    [SerializeField]
    GameObject roomListItemPrefab;

    [SerializeField]
    Transform roomListParent;

    public void RefreshRoomList()
    {
        ClearRoomList();
        networkManager.matchMaker.ListMatches(0, 20, "", true, 0, 0, OnMatchList);
        status.text = "Loading...";
    }

    public void JoinRoom(MatchInfoSnapshot matchInfo)
    {
        networkManager.matchMaker.JoinMatch(matchInfo.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        ClearRoomList();
        status.text = "Joining...";
    }

    private void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matchList)
    {

        // Connectivity issue, unable to continue
        if (!success)
        {
            status.text = "Couldn't get matches";
            return;
        }


        foreach (MatchInfoSnapshot match in matchList)
        {

            // Create item in the room list
            GameObject roomListItemGO = Instantiate(roomListItemPrefab);
            roomListItemGO.transform.SetParent(roomListParent, false);

            RoomListItem roomListItem = roomListItemGO.GetComponent<RoomListItem>();

            if (roomListItem != null)
            {
                roomListItem.Setup(match, JoinRoom);
            }

            /*
             * and setting up a callback function to join the game
             */

            roomList.Add(roomListItemGO);
        }

        if (roomList.Count == 0)
        {
            status.text = "No rooms at the moment";
        }
        else
        {
            status.text = "";
        }
    }

    private void ClearRoomList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);
        }

        roomList.Clear();
    }

    private void Start()
    {
        networkManager = NetworkManager.singleton;

        // make sure the match maker is set up before using it
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }

        RefreshRoomList();

        // Testing purposes
        //GameObject roomListItemGO = Instantiate(roomListItemPrefab);
        //roomListItemGO.transform.SetParent(roomListParent, false);
    }
}
