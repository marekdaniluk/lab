using System;
using System.Collections.Generic;
using UnityEngine;

namespace lab {
    [System.Serializable]
    public class TreeNode : ABlackboardNode {

        [SerializeField]
        private int _treeIndex = 0;

        public int TreeIndex {
            get { return _treeIndex; }
            set { _treeIndex = value; }
        }

        public override bool Run(List<ATaskScript> tasks) {
            return Blackboard.Trees[TreeIndex].Run(tasks);
        }
    }
}
