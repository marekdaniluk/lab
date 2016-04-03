using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// 
    /// <para></para>
    /// </summary>
    [System.Serializable]
    public abstract class ANode : ScriptableObject {

#if UNITY_EDITOR
		public delegate void DebugNodeHandler(ANode node, bool result);
		public DebugNodeHandler OnDebugResult = delegate { };

        [SerializeField, HideInInspector]
        public Vector2 Position;
#endif

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="trees"></param>
        /// <param name="level"></param>
        /// <param name="nodeIndex"></param>
        /// <returns></returns>
        public abstract bool DebugRun(AiBlackboard parameters, IList<AiTree> trees, int level, int nodeIndex);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="trees"></param>
        /// <param name="tasks"></param>
        /// <returns></returns>
        public abstract bool Run(AiBlackboard parameters, IList<AiTree> trees, List<ATaskScript> tasks);
    }
}
