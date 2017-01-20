using UnityEngine;
using System.Collections.Generic;

namespace lab {
    /// <summary>
    /// Node responsible for running binded task script.
    /// <para>ANode represents logic of ai. TaskNodes are bridges between logic and implementation, they execute binded tasks.</para>
    /// </summary>
    [System.Serializable]
    public class TaskNode : ANode {

        [SerializeField]
        private string _taskKey;

        /// <summary>
        /// Sets/Gets key of task from AiController's tasks list to be executed. This is how tasks are binded currently.
        /// </summary>
        public string TaskKey {
            get { return _taskKey; }
            set { _taskKey = value; }
        }

        /// <summary>
        /// Runs this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <param name="tasks">List of task scripts to bind with.</param>
        /// <returns>True if node succeed. Otherwise false.</returns>
        public override bool Run(AiBlackboard parameters, IList<AiTree> trees, List<TaskBinder> tasks) {
            for(int i = 0; i < tasks.Count; ++i) {
				if(tasks[i].taskKeyName == TaskKey) {
                    return tasks[i].task.Execute();
                }
            }
            return false;
        }

        /// <summary>
        /// Runs debug this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <returns>Debug run always returns true for TaskNode.</returns>
        public override bool DebugRun(AiBlackboard parameters, IList<AiTree> trees) {
            OnDebugResult(this, true);
            return true;
        }

        public override string ToString() {
            return string.IsNullOrEmpty(_taskKey) ? GetType().Name : _taskKey;
        }
    }
}
