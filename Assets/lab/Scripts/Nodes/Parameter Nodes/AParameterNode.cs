using UnityEngine;

namespace lab {
    /// <summary>
    /// Abstract, generic, base class for parameter nodes.
    /// <para>AParameterNode is a type of ANode that checks global information from AiBlackboard. It compares blackboard's values for two different ways.
    /// One way is to compare to static value provided to parameter node, another way is to compare to two different blackboard's values.</para>
    /// </summary>
    [System.Serializable]
    public abstract class AParameterNode<T> : ANode {

        [SerializeField]
        private bool _dynamicValue;
        [SerializeField]
        private string _key;
        [SerializeField]
        private string _dynamicValueKey;
        [SerializeField]
        private T _value;

        /// <summary>
        /// Gets/Sets key of parameter in AiBlackboard.
        /// </summary>
        public string Key {
            get { return _key; }
            set { _key = value; }
        }

        /// <summary>
        /// Gets/Sets static value to compare to global information in AiBlackboard.
        /// </summary>
        public T Value {
            get { return _value; }
            set { _value = value; }
        }

        /// <summary>
        /// Gets/Sets a flag to compare to static or dynamic (another blackboard's value) parameter.
        /// </summary>
        public bool DynamicValue {
            get { return _dynamicValue; }
            set { _dynamicValue = value; }
        }

        /// <summary>
        /// Gets/Sets key of parameter to compare to.
        /// </summary>
        public string DynamicValueKey {
            get { return _dynamicValueKey; }
            set { _dynamicValueKey = value; }
        }

        public override ANode Clone() {
            var clone = Instantiate(this);
            clone.Position -= Vector2.one * 20f;
            clone.name = this.name;
            clone.hideFlags = HideFlags.HideInHierarchy;
            return clone;
        }
    }
}
