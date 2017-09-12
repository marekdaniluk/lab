namespace lab {
    /// <summary>
    /// Helper struct for mapping task implementations with ai trees' nodes.
    /// </summary>
    [System.Serializable]
    public struct TaskBinder {

        public string taskKeyName;
        public ATaskScript task;
    }
}
