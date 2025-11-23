using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GameManager : MonoBehaviour
{
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
    public int testPlayer;
    public int testDestination;


    public int team1Score;
    public int team2Score;

    public DiceRoller d4;
    public DiceRoller d20;

    public List<Player> players;


    public List<Transform> bases;

    private void FixedUpdate()
    {
        if (test)
        {
            test = false;
            SetPlayerDestination(testPlayer, testDestination);
        }
    }

    public void SetPlayerDestination(int player, int _base)
    {
        int newBaseInt = players[player].myCurrentBase++;

        if (newBaseInt >= bases.Count)
        {
            newBaseInt = 0;
            players[player].myCurrentBase = newBaseInt;
        }

        players[player].GetComponent<NavMeshAgent>().SetDestination(bases[newBaseInt].position);
    }

    public void PlayerOnBase(int _base, int team)
    {

        if (_base == 0)
        {

            switch (team)
            {
                case 1:
                    team1Score++;
                    break;
                case 2:
                    team2Score++;
                    break;
            }
        }

    }
}
