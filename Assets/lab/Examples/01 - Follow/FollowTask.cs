using UnityEngine;

namespace lab.Example1 {
    public class FollowTask : ATaskScript {

        public Transform follow;

        public override bool Execute() {
            transform.position = Vector3.Lerp(transform.position, follow.position, Time.smoothDeltaTime);
            return true;
        }
    }
}
