using UnityEngine;
using System.Collections;

public class FollowTask : lab.ATaskScript {

    public Transform follow;
    public float maxDistance = 10f;

    public override bool Execute() {
        transform.position = Vector3.Lerp(transform.position, follow.position, Time.smoothDeltaTime);
        return true;
    }
}
