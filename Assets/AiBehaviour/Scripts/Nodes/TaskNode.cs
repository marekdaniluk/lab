using UnityEngine;
using System.Collections.Generic;

namespace AiBehaviour {
    [System.Serializable]
    public class TaskNode : ANode {

        [SerializeField]
        private int _taskIndex = 0;

        public int TaskIndex {
            get { return _taskIndex; }
            set { _taskIndex = value; }
        }

        public override bool Run(List<ATaskScript> tasks) {
            return tasks[_taskIndex];
        }
    }
}
