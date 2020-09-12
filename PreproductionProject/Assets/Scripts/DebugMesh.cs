using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugMesh : MonoBehaviour
{
    public MeshFilter meshFilter;
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<DebugText>()?.AddDebugText(gameObject.name + ": " + meshFilter.mesh.name);
    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
