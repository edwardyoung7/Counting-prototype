using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    protected float speed = 10.0f;
    protected float spawnRange = 15;
    protected float spawnPosZ = 0;
    protected float spawnPosY = 30;
    protected GameManager gameManager;
    protected PlayerController playerController;
    protected GameObject player;
    protected Rigidbody snacksRb;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        playerController = GameObject.Find("Player").GetComponent<PlayerController>();
        player = GameObject.Find("Player");
        snacksRb = GetComponent<Rigidbody>();
        RandomSpawnPos();
    }

    // Update is called once per frame
    public virtual void Update()
    {
        ObjectFallSpeed();
    }

    private void RandomSpawnPos()
    {
        transform.position = RandomPosition();
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-spawnRange, spawnRange), spawnPosY, spawnPosZ);
    }

    protected void ObjectFallSpeed()
    {
        transform.Translate(Vector3.down * Time.deltaTime * speed);
    }
}
