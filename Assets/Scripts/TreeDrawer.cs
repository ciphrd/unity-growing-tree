using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
 * This class can draw the different elements of the tree to have a visual feedback of its growth
 **/
public class TreeDrawer: MonoBehaviour
{
	public GameObject _treeObject;
	Generator _treeGenerator;

	public GameObject _leafObject;
	public GameObject _rangeObject;
	public GameObject _killRangeObject;

	public Material _leaf;
	public Material _leafInactive;
	public Material _attractionRange;

	// the attractors
	GameObject[] _attractors;

	// Start is called before the first frame update
	void Start () {
		_treeGenerator = _treeObject.GetComponent<Generator>();
		_attractors = new GameObject[_treeGenerator._nbAttractors];

		for (int i = 0; i < _treeGenerator._nbAttractors; i++) {
			_attractors[i] = Instantiate(_leafObject);
			_attractors[i].transform.parent = transform;
		}
	}

	// Update is called once per frame
	void Update () {
		// we update the attractors
		if (_treeGenerator._nbAttractors != _attractors.Length) {
			GameObject[] attr = new GameObject[_treeGenerator._nbAttractors];
			for (int i = 0; i < _treeGenerator._nbAttractors; i++) {
				attr[i] = _attractors[i];
			}
			for (int i = _treeGenerator._nbAttractors; i < _attractors.Length; i++) {
				Destroy(_attractors[i]);
			}
			_attractors = attr;
		}

		for (int i = 0; i < _attractors.Length; i++) {
			_attractors[i].transform.position = _treeGenerator._attractors[i];
			_attractors[i].GetComponent<MeshRenderer>().material = _treeGenerator._activeAttractors.Contains(i) ? _leaf : _leafInactive;
		}

		/*
		_rangeObject.transform.localScale = new Vector3(_treeGenerator._attractionRange, _treeGenerator._attractionRange, _treeGenerator._attractionRange) * 2;
		_rangeObject.transform.position = _treeGenerator._extremities[0]._end;

		_killRangeObject.transform.localScale = new Vector3(_treeGenerator._killRange, _treeGenerator._killRange, _treeGenerator._killRange) * 2;
		_killRangeObject.transform.position = _treeGenerator._extremities[0]._end;
		*/
	}
}
