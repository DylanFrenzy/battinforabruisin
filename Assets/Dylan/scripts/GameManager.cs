using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
    public CinemachineCamera mainCamera;
    public CinemachineCamera diceCamera;
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public bool test;
    public int testDestination;


    public bool actionInProgress;

    public int currentPlayer;

    public int team1Score;
    public int team2Score;

    public DiceRoller d4;
    public GameObject D4;
    public DiceRoller d20;
    public GameObject D20;

    public List<Player> players;


    public List<Transform> bases;

    private void FixedUpdate()
    {
        if (test)
        {
            test = false;
            RollToHit();
        }
    }

    public void SetPlayerDestination(int player, int _base)
    {
        actionInProgress = true;
        int newBaseInt = players[player].myCurrentBase++;

        if (newBaseInt >= bases.Count)
        {
            newBaseInt = 0;
            players[player].myCurrentBase = newBaseInt;
        }

        players[player].GetComponent<NavMeshAgent>().SetDestination(bases[newBaseInt].position);
    }

    public void PlayerOnBase(Player player, int _base, int team)
    {

        if (_base == 0)
        {
            switch (team)
            {
                case 1:
                    team1Score++;
                    actionInProgress = false;
                    players[currentPlayer].myDestination = 0;
                    currentPlayer++;
                    break;
                case 2:
                    team2Score++;
                    actionInProgress = false;
                    players[currentPlayer].myDestination = 0;
                    currentPlayer--;
                    break;
            }
        }



        if(player.myCurrentBase < player.myDestination)
        {
            SetPlayerDestination(currentPlayer, player.myCurrentBase);
        }
        if (player.myCurrentBase == player.myDestination)
        {
            actionInProgress = false;
        }

    }



    public void RollToHit()
    {
        if (!actionInProgress)
        {
            if (players[currentPlayer].myCurrentBase == 0)
            {
                SetPlayerDestination(currentPlayer, 0);
                players[currentPlayer].myDestination = 0;
            }

            d20.RollDice();
            actionInProgress = true;
        }
    }

    public void ReturnRollToHit(int numberRolled)
    {
        if(numberRolled == 20)
        {
            players[currentPlayer].myDestination = 999999999;
            SetPlayerDestination(currentPlayer, players[currentPlayer].myCurrentBase);
        }
        else
        {
            if (numberRolled % 2 == 0) //even
            {
                Debug.Log("Hit");
                RollToMove();
            }  
            
            if (numberRolled % 2 == 1) //odd
            {
                Debug.Log("Miss");
                actionInProgress = false;
            }
        }
    }

    public void RollToMove()
    {
        diceCamera.LookAt = D4;
        d4.RollDice();
        actionInProgress = true;
    }

    public void ReturnRollToMove(int numberRolled)
    {
        players[currentPlayer].myDestination = players[currentPlayer].myCurrentBase + numberRolled;
        SetPlayerDestination(currentPlayer, players[currentPlayer].myCurrentBase);
    }


}
