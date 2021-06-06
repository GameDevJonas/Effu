using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindParticleSpawner : MonoBehaviour
{
    private BoxCollider2D boundingBox;
    private Transform player;
    [SerializeField] private float spawnInterval;
    private float timer;
    [SerializeField] private GameObject[] particles;

    private void Awake()
    {
        boundingBox = GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        timer = spawnInterval;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer >= spawnInterval)
        {
            SpawnParticle();
            timer = 0;
        }
        else
        {
            timer += Time.deltaTime;
        }

        transform.position = player.position;
    }

    private void SpawnParticle()
    {
        Vector2 spawnPos = new Vector2(Random.Range(boundingBox.bounds.min.x, boundingBox.bounds.max.x), Random.Range(boundingBox.bounds.min.y, boundingBox.bounds.max.y));
        GameObject toSpawn = particles[Random.Range(0, particles.Length)];
        GameObject clone = Instantiate(toSpawn, spawnPos, toSpawn.transform.rotation);
        clone.GetComponent<ParticleSystem>().Play();
        Destroy(clone, clone.GetComponent<ParticleSystem>().main.duration);
    }
}
