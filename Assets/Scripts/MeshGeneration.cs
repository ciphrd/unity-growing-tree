using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * @author Ciphered <https://ciphered.xyz>
 * 
 * This class generates simple meshes.
 * It is used to illustrates the different volumes in which the leafs can be container 
 **/
public class MeshGeneration : MonoBehaviour {
	Mesh mesh;
	MeshFilter filter;
	Vector3[] vertices;
	int[] indices;

	[Header("Sphere dimensions")]
	[Range(0, 100)]
	public int _nLat = 10;
	[Range(0, 100)]
	public int _nLong = 10;
	[Range(0f, 20f)]
	public float _radius = 5f;

	// Start is called before the first frame update
	void Awake () {
		filter = GetComponent<MeshFilter>();
		_radius = GameObject.Find("Tree").GetComponent<Generator>()._radius;

		// initialization 
		vertices = new Vector3[_nLat*_nLong + 2];
		indices = new int[_nLong * (_nLat+4) * 4];

		// vertices construction, top and bottom points are excluded
		for (int a = 0; a < _nLong; a++) {
			for (int t = 0; t < _nLat; t++) {
				float alpha = (float)a / _nLong * 2*Mathf.PI;
				float theta = (float)(t+1) / (_nLat) * Mathf.PI;

				int idx = a + t*_nLat;

				float r = _radius;// * theta;

				vertices[idx] = new Vector3(
					r * Mathf.Cos(alpha) * Mathf.Sin(theta),
					r * Mathf.Cos(theta),
					r * Mathf.Sin(alpha) * Mathf.Sin(theta)
				);
			}
		}

		// top and bottom vertices 
		vertices[vertices.Length-2] = new Vector3(0f, _radius, 0f);
		vertices[vertices.Length-1] = new Vector3(0f, -_radius, 0f);

		// translation of the vertices 
		for (int i = 0; i < vertices.Length; i++) {
			vertices[i]+= transform.position;
		}

		// indices construction 
		int ic = 0;

		// we first build the lines connected to the top 
		for (int i = 0; i < _nLong; i++) {
			indices[ic++] = vertices.Length-2;
			indices[ic++] = i;
			indices[ic++] = i;
			indices[ic++] = i == _nLong-1 ? 0 : i+1;
		}

		// the bottom part 
		for (int i = 0; i < _nLong; i++) {
			indices[ic++] = _nLong*(_nLat-2) + i;
			indices[ic++] = vertices.Length-1;
		}

		// now we build the in between lines
		for (int lat = 1; lat < _nLat-1; lat++) {
			for (int lon = 0; lon < _nLong; lon++) {
				int idx = lat*_nLong + lon;
				indices[ic++] = idx;
				indices[ic++] = lon == _nLong-1 ? lat*_nLong : idx+1;
				indices[ic++] = idx;
				indices[ic++] = idx - _nLong;
			}
		}

		//
		filter = GetComponent<MeshFilter>();
		filter.mesh = mesh = new Mesh();
		mesh.SetVertices(vertices);
  }

  // Update is called once per frame
  void Update()
  {
        
  }

	private void OnDrawGizmos() {
		if (vertices == null) {
			return;
		}

		// we draw the lines to create the mesh 
		for (int i = 0; i < indices.Length/2; i++) {
			Gizmos.color = Color.blue;
			Gizmos.DrawLine(vertices[indices[i*2]], vertices[indices[i*2+1]]);
		}

		// we draw the vertices
		for (int i = 0; i < vertices.Length; i++) {
			Gizmos.color = Color.blue;
			Gizmos.DrawSphere(vertices[i], 0.1f);
		}
	}
}
