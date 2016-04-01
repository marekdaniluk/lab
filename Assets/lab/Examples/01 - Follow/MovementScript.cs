using UnityEngine;
using System.Collections;

public class MovementScript : MonoBehaviour {

	void Update () {
        transform.Translate(Input.GetAxis("Horizontal") * 0.5f, Input.GetAxis("Vertical") * 0.5f, 0f);
	}
}
