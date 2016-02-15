using UnityEngine;

namespace AiBehaviour {
    [System.Serializable]
    public abstract class AParameterNode<T> : ANode {

        [SerializeField]
        private bool _dynamicValue;
        [SerializeField]
        private AiBlackboard _blackboard;
        [SerializeField]
        private string _key;
        [SerializeField]
        private string _dynamicValueKey;
        [SerializeField]
        private T _value;

        public AiBlackboard Blackboard {
            get { return _blackboard; }
            set { _blackboard = value; }
        }

        public string Key {
            get { return _key; }
            set { _key = value; }
        }

        public T Value {
            get { return _value; }
            set { _value = value; }
        }

        public bool DynamicValue {
            get { return _dynamicValue; }
            set { _dynamicValue = value; }
        }

        public string DynamicValueKey {
            get { return _dynamicValueKey; }
            set { _dynamicValueKey = value; }
        }
    }
}
