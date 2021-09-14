using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeHandler : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Pipe pipePrefab = null;

    [Header("Settings")]
    [SerializeField] private float gapSize = 4f;
    [SerializeField] private float secondsBetweenSpawns = 2f;

    private float spawnTimer;
    private readonly List<Pipe> pipes = new List<Pipe>();

    private void Update()
    {
        RemoveOldPipes();
        SpawnNewPipes();
    }

    public void ResetPipes()
    {
        foreach (var pipe in pipes)
        {
            Destroy(pipe.gameObject);
        }

        pipes.Clear();
        spawnTimer = 0f;
    }

    private void RemoveOldPipes()
    {
        for (int i = pipes.Count - 1; i>= 0; i--)
        {
            if(pipes[i].transform.position.x < -15f)
            {
                Destroy(pipes[i].gameObject);
                pipes.RemoveAt(i);
            }
        }
    }

    private void SpawnNewPipes()
    {
        spawnTimer -= Time.deltaTime;
        if(spawnTimer > 0f)
        {
            return;
        }

        Pipe topPipe = Instantiate(pipePrefab, transform.position, Quaternion.Euler(0f, 0f, 180f));
        Pipe bottomPipe = Instantiate(pipePrefab, transform.position, Quaternion.identity);

        float gapHeight = UnityEngine.Random.Range(-3f, 1f);

        topPipe.transform.Translate(Vector3.up * (gapHeight + (gapSize / 2)), Space.World);
        bottomPipe.transform.Translate(Vector3.up * (gapHeight - (gapSize / 2)), Space.World);

        pipes.Add(topPipe);
        pipes.Add(bottomPipe);

        spawnTimer = secondsBetweenSpawns;
    }
}
