using UnityEngine;
using System.Collections;

public class FollowAgent : MonoBehaviour {

    public Transform follow;
    public lab.AiController controller;
    public Color32 color;

    public void Start() {
        GetComponent<MeshRenderer>().material.color = color;
    }

    public void Update() {
        controller.SetFloat("distance", Vector3.Distance(transform.position, follow.position));
        controller.Run();
    }
}
