using UnityEngine;

namespace AiBehaviour {
    [System.Serializable]
    public class AiTree {
        [SerializeField]
        private ANode _root;

        public ANode Root {
            get { return _root; }
            set { _root = value; }
        }

        public bool Run() {
            return Root.Run();
        }
    }
}
