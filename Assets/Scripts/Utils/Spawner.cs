using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform parent;

    [SerializeField] private GameObject redBall;
    [SerializeField] private GameObject greenBall; 

    [SerializeField] private Vector3 firstPoint;
    [SerializeField] private Vector3 lastPoint;
    [SerializeField] private float spacePerObject;

    private List<Vector3> spawnPositions;
    private List<GameObject> pooledBalls, spawnedBalls = new List<GameObject>();

    private void Start()
    {
        spawnPositions = GetPositions();
        pooledBalls = PoolBalls(spawnPositions.Count);
        GameManager.Instance.OnGameStateChanged.AddListener(HandleStateChanged);
    }

    private void HandleStateChanged(GameManager.GameState currentState)
    {
        if (currentState == GameManager.GameState.Pregame)
        {
            SpawnBalls();
        }
    }

    private List<Vector3> GetPositions()
    {
        var positions = new List<Vector3>();
        var x = firstPoint.x;
        var y = 1.5f;
        var z = firstPoint.z;

        while (z <= lastPoint.z)
        {
            if (x <= lastPoint.x)
            {
                positions.Add(new Vector3(x, y, z));
                
            }
            else
            {
                z += spacePerObject;
                x = firstPoint.x;
                continue;
            }
            x += spacePerObject;
        }

        return positions;
    }

    private List<GameObject> PoolBalls(float amount)
    {
        var pooledRedBalls = GetPooledObjects(redBall, (int)(amount * 2f / 5f)); //To get 40% red balls.
        var pooledGreenBalls = GetPooledObjects(greenBall, Mathf.CeilToInt(amount * 3 / 5));
        
        pooledGreenBalls.AddRange(pooledRedBalls);
        return pooledGreenBalls;
    }

    public List<GameObject> GetPooledObjects(GameObject originalball, int amountToPool)
    {
        List<GameObject> balls = new List<GameObject>();

        for (int i = 0; i < amountToPool; i++)
        {
            var ball = Instantiate(originalball, parent);
            ball.SetActive(false);
            balls.Add(ball);
        }

        return balls;
    }

    private void SpawnBalls()
    {
        pooledBalls.Shuffle();
        ClearSpawnedBalls();
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            var rand = Random.Range(1, 5);
            if (rand == 1 || rand == 3) continue;
            pooledBalls[i].transform.position = spawnPositions[i];
            pooledBalls[i].SetActive(true);
            spawnedBalls.Add(pooledBalls[i]);
        }
    }

    private void ClearSpawnedBalls()
    {
        if (spawnedBalls.Count == 0) return;
        for (int i = 0; i < spawnedBalls.Count; i++)
        {
            spawnedBalls[i].SetActive(false);
        }
        spawnedBalls.Clear();
    }
}
