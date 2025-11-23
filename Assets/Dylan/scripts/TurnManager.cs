using System.Runtime.CompilerServices;
using NUnit.Framework;
using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;
public enum GameState
{
    WaitingToStart,
    PlayerTurn,
    Player2Turn,
    GameOver
}
public class TurnManager : NetworkBehaviour

{
    private NetworkVariable<GameState> currentState = new(GameState.WaitingToStart);

    private NetworkVariable<int> currentPlayerTurn = new(1);

    private List<ulong> playerIDs = new List<ulong>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void OnNetworkSpawn()
    {
        currentPlayerTurn.OnValueChanged += OnPlayerTurnChanged;
        currentState.OnValueChanged += OnGameStateChanged;

        if (IsServer)
        {
            currentState.Value = GameState.PlayerTurn;
        }
    }
    
    
    
    
    void Start()




    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
