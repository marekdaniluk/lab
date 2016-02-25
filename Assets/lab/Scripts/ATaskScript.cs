using UnityEngine;

namespace lab {
    [System.Serializable]
    public abstract class ATaskScript : MonoBehaviour {

        public abstract bool Execute();
    }
}
