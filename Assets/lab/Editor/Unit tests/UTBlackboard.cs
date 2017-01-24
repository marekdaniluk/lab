using NUnit.Framework;

namespace lab.unittests {
    [TestFixture]
    [Category("blackboard tests")]
    internal class UTBlackboard {

        [Test]
        public void BlackboardCreationTest() {
            AiBlackboard blackboard = new AiBlackboard();
            Assert.IsTrue(blackboard.IntParameters != null);
            Assert.IsTrue(blackboard.FloatParameters != null);
            Assert.IsTrue(blackboard.BoolParameters != null);
            Assert.IsTrue(blackboard.StringParameters != null);
        }

        [Test]
        public void BlackboardCopyTest() {
            AiBlackboard blackboard = new AiBlackboard();
            AiBlackboard copy = new AiBlackboard(blackboard);
            Assert.IsTrue(blackboard != copy);
        }
    }
}
