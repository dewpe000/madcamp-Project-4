using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviourPunCallbacks {
    public TMPro.TMP_InputField makeRoomCodeInputField;
    public TMPro.TMP_InputField joinRoomCodeInputField;

    // Start is called before the first frame update
    void Start() {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    void Update() {

    }

    public void MakeRoom() {
        GameManager.isHacker = true;

        string code = makeRoomCodeInputField.text;
        RoomOptions ros = new RoomOptions();
        ros.MaxPlayers = 2;
        ros.IsVisible = true;

        PhotonNetwork.CreateRoom(code, ros, null);
    }

    public void JoinRoom() {
        GameManager.isHacker = false;

        string code = joinRoomCodeInputField.text;
        PhotonNetwork.JoinRoom(code);
    }

    public void RemoveRoom() {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnJoinedRoom() {
        Debug.Log("success join room!");

        SceneManager.LoadScene("GameScene");
    }

    public override void OnJoinRoomFailed(short returnCode, string message) {
        Debug.Log("fail join room");
    }

    public override void OnCreatedRoom() {
        Debug.Log("success create room!");
    }

    public override void OnCreateRoomFailed(short returnCode, string message) {
        Debug.Log("fail create room!");
    }

    public override void OnConnected() {
        Debug.Log("Connect To Server Success!");
    }

    public override void OnDisconnected(DisconnectCause cause) {
        Debug.Log("Disconnect To Server!");
    }

}
