using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class MeshGenerator : MonoBehaviour
{
    public Material green;
    private float meshSize = 10f;
    private float basedMeshSize = 10f;

    void Start()
    {
        Mesh mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        mesh.vertices = GenerateVertices(meshSize, meshSize);
        mesh.triangles = GenerateTriangle();
        mesh.RecalculateNormals();
        MeshCollider collider = gameObject.AddComponent<MeshCollider>();
        collider.convex = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        float centerOfBasedMesh = basedMeshSize / 4f;
        if (gameObject.transform.position.x > centerOfBasedMesh && gameObject.transform.position.z > 0f)
        {
            GenerateFallingMesh(
                GenerateVertices(basedMeshSize - gameObject.transform.position.x, basedMeshSize - gameObject.transform.position.z), gameObject.transform.position);
           GenerateFallingMesh(
                GenerateVertices(meshSize - (basedMeshSize - gameObject.transform.position.x), basedMeshSize - gameObject.transform.position.z),
                new Vector3(gameObject.transform.position.x + (basedMeshSize - gameObject.transform.position.x), gameObject.transform.position.y, gameObject.transform.position.z));
            GenerateFallingMesh(
                GenerateVertices(basedMeshSize - gameObject.transform.position.x, meshSize - (basedMeshSize - gameObject.transform.position.z)),
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z + (basedMeshSize - gameObject.transform.position.z)));
            GenerateFallingMesh(
                GenerateVertices(meshSize - (basedMeshSize - gameObject.transform.position.x), meshSize - (basedMeshSize - gameObject.transform.position.z)),
                new Vector3(gameObject.transform.position.x + (basedMeshSize - gameObject.transform.position.x), gameObject.transform.position.y, gameObject.transform.position.z + (basedMeshSize - gameObject.transform.position.z)));
        }

        if (gameObject.transform.position.x > centerOfBasedMesh && gameObject.transform.position.z < 0f)
        {
            GenerateFallingMesh(
                GenerateVertices(basedMeshSize - gameObject.transform.position.x, meshSize - Math.Abs(gameObject.transform.position.z)),
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0f));
            GenerateFallingMesh(
                GenerateVertices(meshSize - (basedMeshSize - gameObject.transform.position.x), meshSize - Math.Abs(gameObject.transform.position.z)),
                new Vector3(gameObject.transform.position.x + (basedMeshSize - gameObject.transform.position.x), gameObject.transform.position.y, 0f));
            GenerateFallingMesh(
                GenerateVertices(basedMeshSize - gameObject.transform.position.x, Math.Abs(gameObject.transform.position.z)),
                new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -Math.Abs(gameObject.transform.position.z)));
            GenerateFallingMesh(
                GenerateVertices(meshSize - (basedMeshSize - gameObject.transform.position.x), meshSize - Math.Abs(gameObject.transform.position.z)), 
                new Vector3(gameObject.transform.position.x + (basedMeshSize - gameObject.transform.position.x), gameObject.transform.position.y, -Math.Abs(gameObject.transform.position.z)));
        }
        Destroy(gameObject);

    }
    
    Vector3[] GenerateVertices(float coordX, float coordZ)
    {
        return new Vector3[]
        {
            new Vector3(0f, 0f, 0f),
            new Vector3(0f, 0f, coordZ),
            new Vector3(coordX, 0f, 0f),
            new Vector3(coordX, 0f, coordZ),
            
			new Vector3(0f, 1f, 0f),
            new Vector3(coordX, 1f, 0f), 
            
            new Vector3(0f, 1f, coordZ),
            new Vector3(coordX, 1f, coordZ)
        };
    }
    
    int[] GenerateTriangle()
    {
        return new int[] {0, 1, 2, 1, 3, 2, 
            4, 2, 0, 5, 2, 4,
            3, 7, 1, 7, 6, 1,
            3, 2, 7, 5, 7, 2,
            4, 6, 5, 6, 7, 5,
            1, 0, 6, 4, 6, 0

        };
    }

    void GenerateFallingMesh(Vector3[] vertices, Vector3 position)
        {
            GameObject tempCurrentMesh = new GameObject();
            tempCurrentMesh.AddComponent<MeshFilter>();
            tempCurrentMesh.AddComponent<MeshRenderer>();
            Mesh tempMesh = new Mesh();
            tempCurrentMesh.GetComponent<MeshFilter>().mesh = tempMesh;
            tempMesh.vertices = vertices;
            tempMesh.triangles = GenerateTriangle();
            tempMesh.RecalculateNormals();
            MeshCollider collider = tempCurrentMesh.AddComponent<MeshCollider>();
            collider.convex = true;
            tempCurrentMesh.GetComponent<MeshRenderer>().material = green;
            tempCurrentMesh.transform.position = position;
            tempCurrentMesh.AddComponent<Rigidbody>();
        }
    
}


