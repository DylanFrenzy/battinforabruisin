using System.Collections.Generic;
using Unity.Cinemachine;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class DiceRoller : MonoBehaviour
{
    public int diceNumber;
    public int diceTotal;

    [Header("triggers")]
    public bool rollDice;
    public bool diceRolling;
    public Rigidbody myRigidbody;

    [Header("rolling stats to change")]
    public float rollRotationMin;
    public float rollRotationMax;
    public float rollForceMin;
    public float rollForceMax;
    public float rollMaxTime;



    [Header("references")]
    public Vector3 myVelocity;
    public float forceStopRollTimer;
    public ParticleSystem particles;

    public List<DiceRayCasts> rayCasts;

    private void Start()
    {
        myRigidbody.linearVelocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;
        myRigidbody.isKinematic = true;
        diceRolling = false;
        foreach (DiceRayCasts rayCast in rayCasts)
        {
            rayCast.casting = false;
        }
    }

    public void RollDice()
    {
        diceRolling = true;
        foreach (DiceRayCasts rayCast in rayCasts)
        {
            rayCast.casting = true;
        }



        myRigidbody.isKinematic = false;
        myRigidbody.linearVelocity = new Vector3(Random.Range(rollForceMin, rollForceMax), Random.Range(rollForceMin, rollForceMax), Random.Range(rollForceMin, rollForceMax));
        myRigidbody.angularVelocity = new Vector3(Random.Range(rollRotationMin, rollRotationMax), Random.Range(rollRotationMin, rollRotationMax), Random.Range(rollRotationMin, rollRotationMax));
    }



    private void FixedUpdate()
    {
        myVelocity = myRigidbody.linearVelocity;

        if (rollDice)
        {
            RollDice();
            rollDice = false;
        }

        if (diceRolling)
        {
            forceStopRollTimer += Time.deltaTime;

            if(myRigidbody.linearVelocity.magnitude < 0.01f && myRigidbody.angularVelocity.magnitude < 0.01f)
            {
                StopRolling();
            }

            if (forceStopRollTimer >= rollMaxTime)
            {
                StopRolling();
            }

        }
    }

    public void StopRolling()
    {
        particles.Emit(30);
        myRigidbody.linearVelocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;
        myRigidbody.isKinematic = true;
        diceRolling = false;
        forceStopRollTimer = 0;
        foreach (DiceRayCasts rayCast in rayCasts)
        {
            rayCast.casting = false;
        }

        ////////////////////
        // Tell the game manager that I have rolled myNumber
        ////////////////////
        ///

        if (diceTotal == 20)
        {
            GameManager.Instance.ReturnRollToHit(diceNumber);
        }
        if (diceTotal == 4)
        {
            GameManager.Instance.ReturnRollToMove(diceNumber);
        }
    }

}
