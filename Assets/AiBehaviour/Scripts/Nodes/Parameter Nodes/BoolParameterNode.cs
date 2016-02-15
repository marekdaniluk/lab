namespace AiBehaviour {
    [System.Serializable]
    public class BoolParameterNode : AParameterNode<bool> {

        public override bool Run() {
            return Blackboard.BoolParameters[Key] == (DynamicValue ? Blackboard.BoolParameters[Key] : Value);
        }

        public override string ToString() {
            return string.Format("Is {0} {1}?", Blackboard.BoolParameters[Key], (DynamicValue ? Blackboard.BoolParameters[Key] : Value));
        }
    }
}
