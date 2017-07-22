using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Grid : MonoBehaviour {

	public int sizeX, sizeY;

	private Vector3[] vertices;
	private Mesh mesh;

	void Awake () {
		float[] noiseSamples = PerlinNoiseSampler.getPerlinSample (sizeX, sizeY);

		Generate (noiseSamples);
	}

	// Use this for initialization
//	void Start () {
//		
//	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Generate (float[] noiseSamples) {
		GetComponent<MeshFilter> ().mesh = mesh = new Mesh ();
		mesh.name = "Procedural Grid";

		vertices = new Vector3[(sizeX + 1) * (sizeY + 1)];
		Vector2[] uv = new Vector2[vertices.Length];
		Vector4[] tangents = new Vector4[vertices.Length];
			
		Vector4 tangent = new Vector4 (1f, 0f, 0f, -1f);

		for (int i = 0, y = 0; y <= sizeY; y++) {
			for (int x = 0; x <= sizeX; x++, i++) {
				vertices [i] = new Vector3 (x, noiseSamples[i], y);
				uv [i] = new Vector2 ((float)x / sizeX, (float)y / sizeY);
				tangents [i] = tangent;
			}
		}

		mesh.vertices = vertices;
		mesh.uv = uv;
		mesh.tangents = tangents;

		int[] triangles = new int[sizeX * sizeY * 6];
		for (int triIndex = 0, vertIndex = 0, y = 0; y < sizeY; y++, vertIndex++) {
			for (int x = 0; x < sizeX; x++, triIndex += 6, vertIndex++) {
				triangles [triIndex] = vertIndex;
				triangles [triIndex + 3] = triangles [triIndex + 2] = vertIndex + 1;
				triangles [triIndex + 4] = triangles [triIndex + 1] = vertIndex + sizeX + 1;
				triangles [triIndex + 5] = vertIndex + sizeX + 2;
			}
		}

		mesh.triangles = triangles;
		mesh.RecalculateNormals ();

		this.gameObject.AddComponent<MeshCollider> ();
	}

//	private void OnDrawGizmos() {
//		if (vertices == null) {
//			return;
//		}
//
//		Gizmos.color = Color.black;
//		for (int i = 0; i < vertices.Length; i++) {
//			Gizmos.DrawSphere (vertices [i], 0.1f);
//		}
//	}
}
