using UnityEngine;
using System;

namespace lab {
    /// <summary>
    /// AiBlackboard with global information for current behaviour.
    /// <para>AiBlackboard keeps global information values to help making decisions to bevahiour trees. AiBlackboard supports four types (int, float, bool and string) of parameters.
    /// Values can be written or read to cache some more complicated calculation.</para>
    /// </summary>
    [System.Serializable]
    public class AiBlackboard {

        [SerializeField]
        private IntParameter _intParameters;
        [SerializeField]
        private FloatParameter _floatParameters;
        [SerializeField]
        private BoolParameter _boolParameters;
        [SerializeField]
        private StringParameter _stringParameters;

        /// <summary>
        /// Gets int parameters.
        /// </summary>
        public IntParameter IntParameters {
            get { return _intParameters; }
        }

        /// <summary>
        /// Gets float parameters.
        /// </summary>
        public FloatParameter FloatParameters {
            get { return _floatParameters; }
        }

        /// <summary>
        /// Gets bool parameters.
        /// </summary>
        public BoolParameter BoolParameters {
            get { return _boolParameters; }
        }

        /// <summary>
        /// Gets string parameters.
        /// </summary>
        public StringParameter StringParameters {
            get { return _stringParameters; }
        }

        /// <summary>
        /// Creates a copy of this object.
        /// </summary>
        /// <returns>Deep copy of AiBlackboard.</returns>
        public AiBlackboard Clone() {
            var aib = new AiBlackboard();
            aib._intParameters = new IntParameter(_intParameters);
            aib._floatParameters = new FloatParameter(_floatParameters);
            aib._boolParameters = new BoolParameter(_boolParameters);
            aib._stringParameters = new StringParameter(_stringParameters);
            return aib;
        }
    }
}
