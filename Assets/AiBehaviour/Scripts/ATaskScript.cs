using UnityEngine;

namespace AiBehaviour {
    [System.Serializable]
    public abstract class ATaskScript : MonoBehaviour {

        public abstract bool Execute();
    }
}
