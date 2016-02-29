using UnityEngine;
using System.Collections.Generic;

namespace lab {
    [System.Serializable]
    public abstract class ANode : ScriptableObject {

#if UNITY_EDITOR
        [SerializeField, HideInInspector]
        public Vector2 Position;

        public abstract bool DebugRun(int level, int nodeIndex);
#endif

        public abstract bool Run(List<ATaskScript> tasks);
    }
}
