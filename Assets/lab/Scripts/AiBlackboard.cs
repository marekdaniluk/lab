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
        private IntParameter _intParameters = new IntParameter();
        [SerializeField]
        private FloatParameter _floatParameters = new FloatParameter();
        [SerializeField]
        private BoolParameter _boolParameters = new BoolParameter();
        [SerializeField]
        private StringParameter _stringParameters = new StringParameter();
		[SerializeField]
		private TaskParameter _taskParameters = new TaskParameter();

        /// <summary>
        /// Default constructor.
        /// </summary>
        public AiBlackboard() {
            _intParameters = new IntParameter();
            _floatParameters = new FloatParameter();
            _boolParameters = new BoolParameter();
            _stringParameters = new StringParameter();
            _taskParameters = new TaskParameter();
        }

        /// <summary>
        /// Copy constructor.
        /// </summary>
        /// <param name="blackboard">Blackboard to copy.</param>
        public AiBlackboard(AiBlackboard blackboard) {
            _intParameters = new IntParameter(blackboard._intParameters);
            _floatParameters = new FloatParameter(blackboard._floatParameters);
            _boolParameters = new BoolParameter(blackboard._boolParameters);
            _stringParameters = new StringParameter(blackboard._stringParameters);
			_taskParameters = new TaskParameter(blackboard._taskParameters);
        }

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
		/// Gets task parameters.
		/// </summary>
		public TaskParameter TaskParameters {
			get { return _taskParameters; }
		}
    }
}
