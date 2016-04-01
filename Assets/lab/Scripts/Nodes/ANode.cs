using UnityEngine;
using System.Collections.Generic;

namespace lab {
    [System.Serializable]
    public abstract class ANode : ScriptableObject {

#if UNITY_EDITOR
		public delegate void DebugNodeHandler(ANode node, bool result);
		public DebugNodeHandler OnDebugResult = delegate { };

        [SerializeField, HideInInspector]
        public Vector2 Position;

        public abstract bool DebugRun(int level, int nodeIndex);
#endif

        public abstract bool Run(ParameterContainer parameters, List<ATaskScript> tasks);
    }
}
