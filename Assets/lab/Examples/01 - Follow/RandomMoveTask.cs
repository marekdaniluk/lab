using UnityEngine;
using System.Collections;

public class RandomMoveTask : lab.ATaskScript {

    Vector3 random;
    public float maxDistance = 10f;

    public IEnumerator Start() {
        while(true) {
            yield return new WaitForSeconds(2f);
            random = (Random.insideUnitCircle * maxDistance);
            random += transform.position;
        }
    }

    public override bool Execute() {
        transform.position = Vector3.Lerp(transform.position, random, Time.smoothDeltaTime);
        return true;
    }
}
