using UnityEngine;

namespace lab {
    /// <summary>
    /// 
    /// <para></para>
    /// </summary>
    [System.Serializable]
    public abstract class ATaskScript : MonoBehaviour {

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public abstract bool Execute();
    }
}
