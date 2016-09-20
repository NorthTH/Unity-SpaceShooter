using UnityEngine;
using System.Collections;

public class AnimForceShield : MonoBehaviour {

	// Use this for initialization
	public float scrollSpeed;
	public Material ForceSheildMat;

	private float offset;

	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		offset = Time.time * scrollSpeed % 1;

		ForceSheildMat.mainTextureOffset = new Vector2 (offset, offset);
	}
}
