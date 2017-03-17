using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking.Match;

public class RoomListItem : MonoBehaviour
{

    MatchInfoSnapshot matchInfo;

    [SerializeField]
    Text roomNameText;

    public delegate void JoinRoomDelegate(MatchInfoSnapshot matchInfo);
    private JoinRoomDelegate joinRoomDelegate;

    public void Setup(MatchInfoSnapshot matchInfo, JoinRoomDelegate joinRoomDelegate)
    {
        this.matchInfo = matchInfo;
        this.joinRoomDelegate = joinRoomDelegate;

        roomNameText.text = matchInfo.name + " (" + matchInfo.currentSize + "/" + matchInfo.maxSize + ")";
    }

    public void JoinRoom()
    {
        joinRoomDelegate.Invoke(matchInfo);
    }
}
