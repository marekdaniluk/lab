using UnityEngine;

public class FollowTask : lab.ATaskScript {

    public Transform follow;

    public override bool Execute() {
        transform.position = Vector3.Lerp(transform.position, follow.position, Time.smoothDeltaTime);
        return true;
    }
}
