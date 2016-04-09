using NUnit.Framework;

namespace lab {
    namespace unittests {
        [TestFixture]
        [Category("parameter tests")]
        internal class UTParameters {

            [Test]
            public void IntParameterTest() {
                string testKey = "test";
                IntParameter parameter = new IntParameter();
                parameter[testKey] = 10;
                IntParameter copy = new IntParameter(parameter);
                Assert.IsTrue(parameter != copy);
                Assert.IsTrue(parameter[testKey] == copy[testKey]);
                copy[testKey] = 20;
                Assert.IsTrue(parameter[testKey] != copy[testKey]);
                IntParameter reference = parameter;
                Assert.IsTrue(parameter == reference);
            }

            [Test]
            public void FloatParameterTest() {
                string testKey = "test";
                FloatParameter parameter = new FloatParameter();
                parameter[testKey] = 10f;
                FloatParameter copy = new FloatParameter(parameter);
                Assert.IsTrue(parameter != copy);
                Assert.IsTrue(parameter[testKey] == copy[testKey]);
                copy[testKey] = 20f;
                Assert.IsTrue(parameter[testKey] != copy[testKey]);
                FloatParameter reference = parameter;
                Assert.IsTrue(parameter == reference);
            }

            [Test]
            public void BoolParameterTest() {
                string testKey = "test";
                BoolParameter parameter = new BoolParameter();
                parameter[testKey] = true;
                BoolParameter copy = new BoolParameter(parameter);
                Assert.IsTrue(parameter != copy);
                Assert.IsTrue(parameter[testKey] == copy[testKey]);
                copy[testKey] = false;
                Assert.IsTrue(parameter[testKey] != copy[testKey]);
                BoolParameter reference = parameter;
                Assert.IsTrue(parameter == reference);
            }

            [Test]
            public void StringParameterTest() {
                string testKey = "test";
                StringParameter parameter = new StringParameter();
                parameter[testKey] = "some value";
                StringParameter copy = new StringParameter(parameter);
                Assert.IsTrue(parameter != copy);
                Assert.IsTrue(parameter[testKey] == copy[testKey]);
                copy[testKey] = "other value";
                Assert.IsTrue(parameter[testKey] != copy[testKey]);
                StringParameter reference = parameter;
                Assert.IsTrue(parameter == reference);
            }
        }
    }
}
