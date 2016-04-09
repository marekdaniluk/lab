using UnityEngine;

namespace lab {
    /// <summary>
    /// Abstract class for task implementation.
    /// <para>Logic nodes need to be binded with implementations. Scripts that inherits ATaskScript should be attached to your game object and added to AiController's slot. TaskNodes' index reference to the AiControllers' slot index.</para>
    /// </summary>
    /// <example>
    /// Example below shows simple follow task implementation should look like.
    /// <code>
    ///using UnityEngine;
    ///using lab;
    ///
    ///public class FollowTask : ATaskScript {
    ///    public Transform follow;
    ///    public override bool Execute() {
    ///        transform.position = Vector3.Lerp(transform.position, follow.position, Time.smoothDeltaTime);
    ///        return true;
    ///    }
    ///}
    /// </code>
    /// </example>
    [System.Serializable]
    public abstract class ATaskScript : MonoBehaviour {

        /// <summary>
        /// The method that will be invoked by binded TaskNode with current ATaskScript.
        /// </summary>
        /// <returns>The result of execution. If true, TaskNode binded with current ATaskScript will return true, otherwise node will return false.</returns>
        public abstract bool Execute();
    }
}
