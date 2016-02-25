using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

    [SerializeField]
    private lab.AiController _controller;

	void Start () {
        StartCoroutine(CheckCoroutine());
	}
	
	IEnumerator CheckCoroutine () {
        yield return new WaitForSeconds(2f);
        Debug.Log(_controller.Run());
        Debug.Log(_controller.Run(1));
    }
}
