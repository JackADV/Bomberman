using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.TestTools;
using NUnit.Framework;

public class BombermanTests
{
    //public void Setup()
    //{
    //    throw new System.NotImplementedException();
    //}

    private GameObject game; // Store instance of entire game
    private Player[] players;

    // method ofr getting reference to player by index
    Player GetPlayer(int index)
    {
        // Loops through all playedrs from SetUp function
        foreach (var player in players)
        {
            // Compares the playerNumber with given index
            if (player.playerNumber == index)
            {
                // returns that player
                return player;
            }
        }
        // All else fails, return null
        return null;
    }

    [SetUp]
    public void SetUp()
    {
        GameObject gamePrefab = Resources.Load<GameObject>("Prefabs/Game");
        game = Object.Instantiate(gamePrefab);
        players = Object.FindObjectsOfType<Player>();


    }

    // Tests go here
    [UnityTest]
    public IEnumerator PlayerDropsBomb()
    {
        // Get the first player
        Player player1 = GetPlayer(1);

        // Simulate bomb dropping
        player1.DropBomb();

        yield return new WaitForEndOfFrame();

        // Check if bomb exists in the scene
        Bomb bomb = Object.FindObjectOfType<Bomb>();

        // Bomb is not null
        Assert.IsTrue(bomb != null, "The Bomb didn't spawn");

    }
    [UnityTest]
    public IEnumerator PlayerMovement()
    {
        Player player1 = GetPlayer(1);

        Vector3 oldPosition = player1.transform.position;

        player1.Move(true, false, false, false);

        yield return new WaitForEndOfFrame();

        Vector3 newPosition = player1.transform.position;

        Assert.IsTrue(oldPosition !=(newPosition));

    }
    [UnityTest]
    public IEnumerator Player2DropsBomb()
    {
        Player player2 = GetPlayer(2);

        player2.DropBomb();
        yield return new WaitForEndOfFrame();
        Bomb bomb = Object.FindObjectOfType<Bomb>();
        Assert.IsTrue(bomb != null, "The Bomb didn't spawn");
    }



    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game);
    }
}
