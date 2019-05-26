using Microsoft.VisualStudio.TestTools.UnitTesting;
using MultilayerCore;
using System;
using System.Collections.Generic;

namespace UnitTestProject1
{
    [TestClass]
    public class NetworkUnitTest
    {
        public NetworkUnitTest()
        {

        }

        [TestMethod]
        public void NetworkCreate_NeuronCounts_OneLayer()
        {
            Network network = new Network(1.0, new List<int> { 3 }, 24);
            // network layers count
            Assert.AreEqual(1, network.layers.Count);
            // neurons in layer
            Assert.AreEqual(3, network.layers[0].Count);
            // amount of inputs
            Assert.AreEqual(24, network.layers[0][0].weights.Count);
        }

        [TestMethod]
        public void NetworkCreate_NeuronCounts_TwoLayers()
        {
            Network network = new Network(1.0, new List<int> { 3, 5 }, 2);
            // network layers count
            Assert.AreEqual(2, network.layers.Count);
            // neurons in layer
            Assert.AreEqual(3, network.layers[0].Count);
            Assert.AreEqual(5, network.layers[1].Count);
            // amount of inputs
            Assert.AreEqual(2, network.layers[0][2].weights.Count);
            Assert.AreEqual(3, network.layers[1][3].weights.Count);
        }

        [TestMethod]
        public void NetworkCreate_NeuronCounts_ThreeLayers()
        {
            Network network = new Network(1.0, new List<int> { 3, 5, 2 }, 3);
            // network layers count
            Assert.AreEqual(3, network.layers.Count);
            // neurons in layer
            Assert.AreEqual(3, network.layers[0].Count);
            Assert.AreEqual(2, network.layers[2].Count);
            // amount of inputs
            Assert.AreEqual(3, network.layers[0][2].weights.Count);
            Assert.AreEqual(3, network.layers[1][3].weights.Count);
            Assert.AreEqual(5, network.layers[2][1].weights.Count);
        }

        [TestMethod]
        public void Calculate_LayerResult()
        {
            var network = new Network(1.0, new List<int> { 1 }, 2);
            network.layers[0][0].weights = new List<double> { 1.0, 2.0 };
            network.layers[0][0].bias = -1.0;

            Assert.AreEqual(0.5, network.CalculateLayerResult(new List<double> { 3.0, -1.0 }, 0)[0]);
        }

        [TestMethod]
        public void Calculate_NetworkOutput()
        {
            var network = new Network(1.0, new List<int> { 2, 1 }, 2);
            network.layers[0][0].weights = new List<double> { 1.0, 1.0 };
            network.layers[0][0].bias = 0.0;
            network.layers[0][1].weights = new List<double> { 1.0, 1.0 };
            network.layers[0][1].bias = 0.0;
            network.layers[1][0].weights = new List<double> { 1.0, 1.0 };
            network.layers[1][0].bias = 0.0;

            Assert.AreEqual(0.7310585786300049, network.CalculateNetworkOutput(new List<double> { 0.0, 0.0 })[0]);
        }

        [TestMethod]
        public void Calculate_FullNetworkOutput()
        {
            var network = new Network(1.0, new List<int> { 2, 1 }, 2);
            network.layers[0][0].weights = new List<double> { 1.0, 1.0 };
            network.layers[0][0].bias = 0.0;
            network.layers[0][1].weights = new List<double> { 1.0, 1.0 };
            network.layers[0][1].bias = 0.0;
            network.layers[1][0].weights = new List<double> { 1.0, 1.0 };
            network.layers[1][0].bias = 0.0;

            var output = network.CalculateNetworkOutputFull(new List<double> { 0.0, 0.0 });

            Assert.AreEqual(0.7310585786300049, output[1][0]);
            Assert.AreEqual(0.5, output[0][0]);
            Assert.AreEqual(0.5, output[0][1]);
        }

        [TestMethod]
        public void RecalculateWeights_TwoLayer()
        {
            var network = new Network(1.0, new List<int> { 2, 1 }, 2);
            network.layers[0][0].weights = new List<double> { 1.0, 1.0 };
            network.layers[0][0].bias = 0.0;
            network.layers[0][1].weights = new List<double> { 1.0, 1.0 };
            network.layers[0][1].bias = 0.0;
            network.layers[1][0].weights = new List<double> { 1.0, 1.0 };
            network.layers[1][0].bias = 0.0;

            var input = new List<double> { 0.0, 0.0 };
            var fullOutput = network.CalculateNetworkOutputFull(input);
            network.RecalculateWeights(input, fullOutput[fullOutput.Count - 1], new List<double> { 0.0 }, fullOutput);

            Assert.AreEqual(1.0, network.layers[0][0].weights[0]);
            Assert.AreEqual(1.0, network.layers[0][0].weights[1]);
            Assert.AreEqual(1.0, network.layers[0][1].weights[0]);
            Assert.AreEqual(1.0, network.layers[0][1].weights[1]);

            Assert.AreEqual(0.9198168137456808, network.layers[1][0].weights[0]);
            Assert.AreEqual(0.9198168137456808, network.layers[1][0].weights[1]);

            Assert.AreEqual(-0.037686692851833764, network.layers[0][0].bias);
            Assert.AreEqual(-0.037686692851833764, network.layers[0][1].bias);
            Assert.AreEqual(-0.16036637250863844, network.layers[1][0].bias);
        }

        [TestMethod]
        public void Calculate_NetworkError()
        {
            var err = Network.CalculateNetworkError(new List<double> { 0.0, 1.0 }, new List<double> { 0.0, 0.0 });
            Assert.AreEqual(0.5, err);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LayoutBelowZero_Invalid()
        {
            var layout = new List<int> { 23, 19, -32 };
            Network network = new Network(1.0, layout, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LayoutZero_Invalid()
        {
            var layout = new List<int> { 23, 19, 0, 10 };
            Network network = new Network(1.0, layout, 10);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void EmptyLayout_Invalid()
        {
            var layout = new List<int>();
            Network network = new Network(1.0, layout, 10);
        }
    }
}