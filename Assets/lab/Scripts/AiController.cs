using UnityEngine;
using System.Collections.Generic;

namespace lab {
    [DisallowMultipleComponent]
    public class AiController : MonoBehaviour {

        [SerializeField]
        private AiBlackboard _blackboard;
        [SerializeField]
        private List<ATaskScript> _tasks;

        public AiBlackboard Blackboard {
            get { return _blackboard; }
            set { _blackboard = value; }
        }

        public List<ATaskScript> Tasks {
            get { return _tasks; }
            set { _tasks = value; }
        }

        public void SetInt(string key, int val) {
            Blackboard.IntParameters[key] = val;
        }

        public int GetInt(string key) {
            return Blackboard.IntParameters[key];
        }

        public IntParameter.KeyCollection IntKeys {
            get { return Blackboard.IntParameters.Keys; }
        }

        public void SetFloat(string key, float val) {
            Blackboard.FloatParameters[key] = val;
        }

        public float GetFloat(string key) {
            return Blackboard.FloatParameters[key];
        }

        public FloatParameter.KeyCollection FloatKeys {
            get { return Blackboard.FloatParameters.Keys; }
        }

        public void SetBool(string key, bool val) {
            Blackboard.BoolParameters[key] = val;
        }

        public bool GetBool(string key) {
            return Blackboard.BoolParameters[key];
        }

        public BoolParameter.KeyCollection BoolKeys {
            get { return Blackboard.BoolParameters.Keys; }
        }

        public void SetString(string key, string val) {
            Blackboard.StringParameters[key] = val;
        }

        public string GetString(string key) {
            return Blackboard.StringParameters[key];
        }

        public StringParameter.KeyCollection StringKyes {
            get { return Blackboard.StringParameters.Keys; }
        }

        public bool Run(int i = 0) {
            return Blackboard.Run(Tasks, i);
        }

        public IList<AiTree> Trees {
            get { return Blackboard.Trees; }
        }
    }
}
