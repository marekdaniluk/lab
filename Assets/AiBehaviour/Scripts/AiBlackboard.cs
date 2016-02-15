using UnityEngine;
using System.Collections.Generic;

namespace AiBehaviour {
    [System.Serializable]
    public class AiBlackboard : ScriptableObject {

        [SerializeField]
        private IntParameter _intParameters;

        [SerializeField]
        private FloatParameter _floatParameters;

        [SerializeField]
        private BoolParameter _boolParameters;

        [SerializeField]
        private StringParameter _stringParameters;

        [SerializeField]
        private List<AiTree> _trees = new List<AiTree>();

        public IList<AiTree> Trees {
            get { return _trees.AsReadOnly(); }
        }

        public bool AddTree(AiTree tree) {
            if (_trees.Contains(tree)) {
                return false;
            }
            _trees.Add(tree);
            return true;
        }

        public bool RemoveTree(AiTree tree) {
            return _trees.Remove(tree);
        }

        public IntParameter IntParameters {
            get { return _intParameters; }
        }

        public FloatParameter FloatParameters {
            get { return _floatParameters; }
        }

        public BoolParameter BoolParameters {
            get { return _boolParameters; }
        }

        public StringParameter StringParameters {
            get { return _stringParameters; }
        }

        public bool Run(int i = 0) {
            return _trees[i].Run();
        }
    }
}
