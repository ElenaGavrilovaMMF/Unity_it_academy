using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LevelGeneration : MonoBehaviour
{
    public GameObject[] walls;
    public NavMeshSurface navMeshSurface;
    
    void Start()
    {
        StartCoroutine(GenerationLevel());
    }

    private IEnumerator GenerationLevel()
    {
        foreach (GameObject wall in walls)
        {
            wall.SetActive(true);
            yield return new WaitForSeconds(0.3f);
        }
        navMeshSurface.BuildNavMesh();
    }
}