using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using System.Collections;

public class GameScript : MonoBehaviour
{
    // Singleton
    public static GameScript instance;
    //public PlayerScript player;
    public Vector2 spawn1;
    public Vector2 spawn2;
    public Vector2 spawnFake;
    public int randX;
    public int randY;
    
    public GameObject player1;
    public GameObject player2;
    public GameObject fake;
    public int fakeCount;

    // List of active player that join the game

    // State machine variable to orchestrate the game loop
    public GamePhase phase = GamePhase.waiting;

    // The Player who has won the round
    public PlayerScript victor;

    public void Awake()
    {
        // Singleton initialization
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        randX = Random.Range(-6, 7);
        randY = Random.Range(-4, 5);
        spawn1 = new Vector2(randX, randY);
        
    }

    public void FixedUpdate()
    {
        
        if (phase == GamePhase.starting)
        {
            //Destroy(player.gameObject);
            randX = Random.Range(-6, 7);
            randY = Random.Range(-4, 5);
            spawn1 = new Vector2(randX, randY);
            spawn2 = new Vector2(0, 0);
            
            Instantiate(player1, spawn1, Quaternion.identity);
            Instantiate(player2, spawn2, Quaternion.identity);
            SpawnFake();
            phase = GamePhase.started;
        }
    }
    

    // Only one player can jump per round & only if the game has started
    public bool CanMove()
    {
        return phase == GamePhase.started;
    }

    // If the player can jump we call this function and end the round
    public void OnPlayerGuessed(SpotlightScript player)
    {
        StartCoroutine(EndRound());
    }

    public void OnPlayerLeft(PlayerScript player)
    {
        //StartCoroutine(EndRound());
    }

    // Used by both round outcomes to end & reset the game
    public IEnumerator EndRound()
    {
        // Start the ending phase (pauses timer) & prevents jumping
        phase = GamePhase.ending;
        // Give the players time to Jump & Die
        yield return new WaitForSeconds(3);
        // Start resetting the game
        phase = GamePhase.starting;
    }
    

    public void SpawnFake()
    {
        for (int i = 0; i < fakeCount; i++)
        {
            randX = Random.Range(-6, 7);
            randY = Random.Range(-4, 5);
            spawnFake = new Vector2(randX, randY);
            Instantiate(fake, spawnFake, Quaternion.identity);
        }
    }
}

public enum GamePhase
{
    starting,
    started,
    ending,
    waiting
}