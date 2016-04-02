using UnityEngine;

namespace lab {
    /// <summary>
    /// Blackboard with global information for current behaviour.
    /// <para>Blackboard keeps global information values to help making decisions to bevahiour trees. Blackboard supports four types (int, float, bool and string) of parameters.
    /// Values can be written or read to cache some more complicated calculation.</para>
    /// </summary>
    [System.Serializable]
    public class Blackboard {

        [SerializeField]
        private IntParameter _intParameters;
        [SerializeField]
        private FloatParameter _floatParameters;
        [SerializeField]
        private BoolParameter _boolParameters;
        [SerializeField]
        private StringParameter _stringParameters;

        /// <summary>
        /// Getter for int parameters.
        /// </summary>
        public IntParameter IntParameters {
            get { return _intParameters; }
        }

        /// <summary>
        /// Getter for float parameters.
        /// </summary>
        public FloatParameter FloatParameters {
            get { return _floatParameters; }
        }

        /// <summary>
        /// Getter for bool parameters.
        /// </summary>
        public BoolParameter BoolParameters {
            get { return _boolParameters; }
        }

        /// <summary>
        /// Getter for string parameters.
        /// </summary>
        public StringParameter StringParameters {
            get { return _stringParameters; }
        }
    }
}
