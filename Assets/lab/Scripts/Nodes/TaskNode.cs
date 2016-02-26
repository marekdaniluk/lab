using UnityEngine;
using System.Collections.Generic;

namespace lab {
    [System.Serializable]
    public class TaskNode : ANode {

        [SerializeField]
        private int _taskIndex = 0;
		[SerializeField]
		private string _description;
		
		public int TaskIndex {
			get { return _taskIndex; }
			set { _taskIndex = value; }
		}
		
		public string Description {
			get { return _description; }
			set { _description = value; }
		}

        public override bool Run(List<ATaskScript> tasks) {
            return tasks[_taskIndex].Execute();
        }
    }
}
