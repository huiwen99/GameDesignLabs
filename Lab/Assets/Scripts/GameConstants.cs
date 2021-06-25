using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConstants", menuName = "ScriptableObjects/GameConstants", order = 1)]
public class GameConstants : ScriptableObject
{
    // for Consume.cs
    public int consumeTimeStep = 10;
    public int consumeLargestScale = 4;

    // for Break.cs
    public int breakTimeStep = 30;
    public int breakDebrisTorque = 10;
    public int breakDebrisForce = 10;

    // for SpawnDebris.cs
    public int spawnNumberOfDebris = 5;


    // for EnemyController.cs
    public float maxOffset = 5.0f;
    public float enemyPatroltime = 2.0f;
    public float groundSurface = -4;

    // for PlayerController.cs
    public AudioClip dieClip;

    // Mario basic starting values
    public int playerMaxSpeed = 5;
    public int playerMaxJumpSpeed = 30;
    public int playerDefaultForce = 150;

    public float groundDistance = -4.0f;

}
