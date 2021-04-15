using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using System.IO;
using ExitGames.Client.Photon;

public class PlayerListingsMenu : MonoBehaviourPunCallbacks
{
    [SerializeField]
    private Transform _content;
    [SerializeField]
    private PlayerListing _playerListing;
    [SerializeField]
    private TextMeshProUGUI _readyText;

    private List<PlayerListing> _listings = new List<PlayerListing>();
    //Player[] photonPlayers;
    private RoomCanvases _roomCanvases;
    private bool _ready = false;
    private ExitGames.Client.Photon.Hashtable _myTurn = new ExitGames.Client.Photon.Hashtable();
    
    private const byte FARMER1_EVENT = 12;
    private const byte FARMER2_EVENT = 13;


    RaiseEventOptions options = new RaiseEventOptions()
    {
        CachingOption = EventCaching.AddToRoomCacheGlobal,
        Receivers = ReceiverGroup.All,
        TargetActors = null,
        InterestGroup = 0
    };
    //  private PhotonView PV

    //public bool rejoin = false;

    public override void OnEnable()
    {
        base.OnEnable();
        GetCurrentRoomPlayers();
        SetReadyUp(false);
        //  PV = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient)
        {
            //_myTurn["TurnID"] = 1;
            //PhotonNetwork.SetPlayerCustomProperties(_myTurn);
            PlayerPrefs.SetInt("TurnID", 1);
        }
        else
        {
            //_myTurn["TurnID"] = 2;
            //PhotonNetwork.SetPlayerCustomProperties(_myTurn);
            PlayerPrefs.SetInt("TurnID", 2);

        }
        PhotonNetwork.NetworkingClient.EventReceived += NetworkingClientGetNamesAndFarmers;


    }

    public override void OnDisable()
    {
        base.OnDisable();
        for (int i = 0; i < _listings.Count; i++)
            Destroy(_listings[i].gameObject);

        _listings.Clear();
    }

    public void Initialize(RoomCanvases canvases)
    {
        _roomCanvases = canvases;
    }

    private void SetReadyUp(bool state)
    {
        _ready = state;
        if (_ready)
            _readyText.text = "Ready!";
        else
            _readyText.text = "Waiting...";
        base.photonView.RPC("RPC_SendWaitMessage", RpcTarget.MasterClient);

    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected)
            return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null)
            return;

        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);

        }

    }



    private void AddPlayerListing(Player player)
    {

        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
        {
            _listings[index].SetPlayerInfo(player);
        }
        else
        {
            PlayerListing listing = Instantiate(_playerListing, _content);
            if (listing != null)
            {
                listing.SetPlayerInfo(player);
                _listings.Add(listing);

            }
        }

    }


    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("Master Client Switched");
        //_roomCanvases.CurrentRoom.LeaveRoomMenu.OnClick_LeaveRoom();
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);

        photonView.RPC("SetUpGameBoard", RpcTarget.All, (int)System.DateTime.Now.Ticks);
        photonView.RPC("CallFarmCodeForAll",RpcTarget.All);
    }

    [PunRPC]
    private void CallFarmCodeForAll()
    {
        Debug.Log("Call Farm Code For All");

        if (PhotonNetwork.IsMasterClient)
        {
            // P1_Name.text = PlayerPrefs.GetString("PlayerName");
            //P2_Name.text = photonView.Owner?.NickName ?? "";

            object[] data = new object[] { PhotonNetwork.LocalPlayer.NickName, GameInformation.farmer };
            Debug.Log($"Farm 1 Sending farmer {GameInformation.farmer}");
            PhotonNetwork.RaiseEvent(FARMER1_EVENT, data, options, SendOptions.SendReliable);
        }
        else
        {
            //P1_Name.text = photonView.Owner?.NickName ?? "";
            //P2_Name.text = PlayerPrefs.GetString("PlayerName");
            object[] data = new object[] { PhotonNetwork.LocalPlayer.NickName, GameInformation.farmer };
            Debug.Log($"Farm 2 Sending farmer {GameInformation.farmer}");
            PhotonNetwork.RaiseEvent(FARMER2_EVENT, data, options, SendOptions.SendReliable);
        }
    }


    private void NetworkingClientGetNamesAndFarmers(EventData obj)
    {
        if (obj.Code == FARMER1_EVENT)
        {
            object[] data = (object[])obj.CustomData;
            string name = (string)data[0];
            Farmer farmer = (Farmer)data[1];
            Debug.Log($"Player 1 - {name} - {farmer} : {GameInformation.farmer}");
            GameInformation.Player1Username = name;
            var player1Farmer = !PhotonNetwork.IsMasterClient && farmer == GameInformation.farmer ? (Farmer)((int)farmer + 3 % 4) : farmer;
            Debug.Log($"PLayer 1 Farmer placed is {player1Farmer}");
            GameInformation.Player1Farmer = player1Farmer;
        }
        else if (obj.Code == FARMER2_EVENT)
        {
            object[] data = (object[])obj.CustomData;
            string name = (string)data[0];
            Farmer farmer = (Farmer)data[1];
            Debug.Log($"Player 2 - {name} - {farmer} : {GameInformation.farmer}");
            GameInformation.Player2Username = name;
            var player2Farmer = PhotonNetwork.IsMasterClient && farmer == GameInformation.farmer ? (Farmer)((int)farmer + 1 % 4) : farmer;
            Debug.Log($"PLayer 2 Farmer placed is {player2Farmer}");
            GameInformation.Player2Farmer = player2Farmer;

        }

    }


    [PunRPC]
    public void SetUpGameBoard(int id)
    {
        //var id = (int)objs[0];
        //GameInformation.Player1Username = (string)objs[1];
        //GameInformation.Player2Username = (string)objs[2];
        //GameInformation.Player1Farmer = PhotonNetwork.IsMasterClient ? GameInformation.farmer : GameInformation.farmer == Farmer.RAGSDALE ? Farmer.BAIRD : Farmer.RAGSDALE;
        //GameInformation.Player2Farmer = !PhotonNetwork.IsMasterClient ? GameInformation.farmer : GameInformation.farmer == Farmer.RAGSDALE ? Farmer.BAIRD : Farmer.RAGSDALE;
        GameBoard.Seed = id;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listings.FindIndex(x => x.Player == otherPlayer);
        if (index != -1)
        {
            Destroy(_listings[index].gameObject);
            _listings.RemoveAt(index);
            //photonPlayers = PhotonNetwork.PlayerList;
        }
    }

    //public bool RejoinState()
    //{
    //    return rejoin;
    //}


    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            for(int i = 0; i < _listings.Count; i++)
            {
                if(_listings[i].Player != PhotonNetwork.LocalPlayer)
                {
                    if (_listings[i].Ready)
                        return;
                }
            }

            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.LoadLevel(2);
            //PV.RPC("RPC_CreatePlayer", RpcTarget.AllBuffered);

        }
        
    }

    //[PunRPC]
    //private void RPC_CreatePlayer()
    //{
    //    PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonNetworkPlayer"), transform.position, Quaternion.identity, 0);
    //}



    public void OnClick_ReadyUp()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            SetReadyUp(!_ready);
            base.photonView.RPC("RPC_ChangeReadyState", RpcTarget.MasterClient, PhotonNetwork.LocalPlayer, _ready);
        }

    }

    [PunRPC]
    private void RPC_ChangeReadyState(Player player, bool ready)
    {
        int index = _listings.FindIndex(x => x.Player == player);
        if (index != -1)
            _listings[index].Ready = ready;
        
    }

    [PunRPC]
    private void RPC_SendWaitMessage()
    {
        _readyText.text = "Waiting...";

    }


}
