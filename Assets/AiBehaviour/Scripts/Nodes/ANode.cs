﻿using UnityEngine;

namespace AiBehaviour {
    [System.Serializable]
    public abstract class ANode : ScriptableObject {

#if UNITY_EDITOR
        [SerializeField, HideInInspector]
        public Vector2 Position;
#endif

        public abstract bool Run();
    }
}
