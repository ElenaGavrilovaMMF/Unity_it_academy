using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class GenerateFirstMesh : MonoBehaviour
{
    public GameObject prefabMesh;
    public Material blue;
    
    private GameObject currentGameObject = null;
    private float speed = 4f;
    private bool movingFlag = true;
    private float hideForGenarationMesh = 3f;
    private float leftBorder = -10f;
    
    void Start()
    {
        GameObject basedMeshObject = new GameObject();
        basedMeshObject.AddComponent<MeshFilter>();
        basedMeshObject.AddComponent<MeshRenderer>().material = blue;
        Mesh basedMesh = new Mesh();
        basedMeshObject.GetComponent<MeshFilter>().mesh = basedMesh;
        basedMesh.vertices = GenerateVertices(10f , 10f);
        basedMesh.triangles = GenerateTriangle();
        basedMesh.RecalculateNormals();
        MeshCollider collider = basedMeshObject.AddComponent<MeshCollider>();
        collider.convex = true;
    }
    
    void Update()
    {
        if (currentGameObject == null)
        {
            currentGameObject = Instantiate(prefabMesh, new Vector3(2.6f,hideForGenarationMesh,15f), Quaternion.identity);
            movingFlag = true;
            hideForGenarationMesh += 0.7f;
        }

        if (currentGameObject != null && movingFlag)
        {
            currentGameObject.transform.position = currentGameObject.transform.position - Vector3.forward*Time.deltaTime*speed;
        }

        if (currentGameObject.transform.position.z < leftBorder)
        {
            Destroy(currentGameObject);
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            movingFlag = false;
            currentGameObject.AddComponent<Rigidbody>();
        }
    }
    
    Vector3[] GenerateVertices(float coordX, float coordZ)
    {
        return new Vector3[]
        {
            new Vector3(0f, 0f, 0f),
            new Vector3(0f, 0f, coordZ),
            new Vector3(coordX, 0f, 0f),
            new Vector3(coordX, 0f, coordZ)
        };
    }

    int[] GenerateTriangle()
    {
        return new int[] {0, 1, 2, 1, 3, 2};
    }
}
