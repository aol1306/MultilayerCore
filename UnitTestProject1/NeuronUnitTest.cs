using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultilayerCore;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class NeuronUnitTest
    {
        private const int INPUT_COUNT = 10;
        private Neuron neuron;

        public NeuronUnitTest()
        {
            neuron = new Neuron(INPUT_COUNT);
        }

        [TestMethod]
        public void Generate_RadomValue_WithinBounds()
        {
            double max = 15.0;
            double min = 10.0;
            for (int i = 0; i < 100; i++)
            {
                Assert.IsTrue(neuron.GenerateRandomInRange(min, max) < max);
                Assert.IsTrue(neuron.GenerateRandomInRange(min, max) > min);
            }
        }

        [TestMethod]
        public void Constructor_InputLength()
        {
            Assert.AreEqual(INPUT_COUNT, neuron.weights.Count);
        }

        [TestMethod]
        public void Activaction_CorrectValues()
        {
            Assert.AreEqual(0.5, Neuron.Activation(0.0));
            Assert.IsTrue(Neuron.Activation(10.0) > 0.95);
            Assert.IsTrue(Neuron.Activation(-10.0) < 0.05);
        }

        [TestMethod]
        public void DerivedActivation_CorrectValues()
        {
            for (int i = -10; i < 10; i++)
            {
                Assert.IsTrue(Neuron.DerivedActivation(i) > 0.0);
            }
        }

        [TestMethod]
        public void CalculateOutput_CorrectValues()
        {
            var n = new Neuron(2);
            n.weights = new List<double> { 1.0, 2.0 };
            n.bias = -1.0;

            Assert.AreEqual(0.9525741268224331, n.CalculateOutput(new List<double> { 2.0, 1.0 }));
            Assert.AreEqual(0.5, n.CalculateOutput(new List<double> { 3.0, -1.0 }));
        }
    }
}
