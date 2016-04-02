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

        public override bool Run(Blackboard parameters, IList<AiTree> trees, List<ATaskScript> tasks) {
            return tasks[_taskIndex].Execute();
        }
#if UNITY_EDITOR
        public override bool DebugRun(Blackboard parameters, IList<AiTree> trees, int level, int nodeIndex) {
			Debug.Log(string.Format("{0}{1}. Task Node. Result in debug mode for tasks is always <b><color=orange>true</color></b>", new string('\t', level), nodeIndex));
			OnDebugResult(this, true);
            return true;
        }
#endif
    }
}
