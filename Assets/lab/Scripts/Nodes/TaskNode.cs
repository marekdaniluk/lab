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
        private int _taskIndex = 0;
        [SerializeField]
        private string _description;

        /// <summary>
        /// Sets/Gets index of task from AiController's tasks list to be executed. This is how tasks are binded currently.
        /// </summary>
        public int TaskIndex {
            get { return _taskIndex; }
            set { _taskIndex = value; }
        }

        /// <summary>
        /// Sets/Gets description of current task.
        /// </summary>
        public string Description {
            get { return _description; }
            set { _description = value; }
        }

        /// <summary>
        /// Runs this node.
        /// </summary>
        /// <param name="parameters">AiBlackboard with global parameters.</param>
        /// <param name="trees">Readonly list with all ai trees.</param>
        /// <param name="tasks">List of task scripts to bind with.</param>
        /// <returns>True if node succeed. Otherwise false.</returns>
        public override bool Run(AiBlackboard parameters, IList<AiTree> trees, List<ATaskScript> tasks) {
            return tasks[_taskIndex].Execute();
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
    }
}
