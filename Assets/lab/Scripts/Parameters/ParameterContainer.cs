using UnityEngine;
using System.Collections.Generic;

namespace lab {
    [System.Serializable]
    public class ParameterContainer {

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
