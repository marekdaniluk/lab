using UnityEngine;

namespace AiBehaviour {
    [System.Serializable]
    public abstract class ANode : ScriptableObject {

        public abstract bool Run();
    }
}
