using System;
using System.Collections.Generic;
using System.Text;

namespace MultilayerCore
{
    public class Network
    {
        public const double MOMENTUM = 1.0;
        public const int MAX_LEARNING_EPOCH = 1000000;

        public List<List<Neuron>> layers;
        public double alpha;

        public Network(double alpha, List<int> layout, int inputs)
        {
            CheckLayoutCorrect(layout);

            this.alpha = alpha;
            this.layers = new List<List<Neuron>>(layout.Count);

            // fill with neurons
            // input layer
            layers.Add(new List<Neuron>(layout[0]));
            for (int i = 0; i < layout[0]; i++)
            {
                layers[0].Add(new Neuron(inputs));
            }

            // remaining layers
            if (layout.Count > 1)
            {
                for (int i = 1; i < layout.Count; i++)
                {
                    layers.Add(new List<Neuron>(layout[i]));
                    for (int j = 0; j < layout[i]; j++)
                    {
                        layers[i].Add(new Neuron(layout[i - 1]));
                    }
                }
            }
        }

        public List<double> CalculateLayerResult(List<double> x, int layer)
        {
            var result = new List<double>(layers[layer].Count);
            foreach (Neuron neuron in layers[layer])
            {
                result.Add(neuron.CalculateOutput(x));
            }
            return result;
        }

        public List<double> CalculateNetworkOutput(List<double> x)
        {
            var lastLayerResult = CalculateLayerResult(x, 0);
            if (layers.Count > 1)
            {
                for (int i = 1; i < layers.Count; i++)
                {
                    lastLayerResult = CalculateLayerResult(lastLayerResult, i);
                }
            }
            return lastLayerResult;
        }

        /// <summary>
        /// Calculates network output and remembers all outputs and inputs of all neurons.
        /// </summary>
        /// <param name="x">Network input</param>
        public List<List<double>> CalculateNetworkOutputFull(List<double> x)
        {
            var ret = new List<List<double>>(layers.Count);
            ret.Add(CalculateLayerResult(x, 0));

            for (int i = 1; i < layers.Count; i++)
            {
                ret.Add(new List<double>(layers[i].Count));
                ret[i] = CalculateLayerResult(ret[i - 1], i);
            }

            return ret;
        }

        public void RecalculateWeights(List<double> input, List<double> received, List<double> expected, List<List<double>> allNeuronOuts)
        {
            var errors = new List<List<double>>();

            // output layer
            errors.Add(new List<double>());
            for (int i = 0; i < layers[layers.Count - 1].Count; i++)
            {
                var neuronErr = (expected[i] - received[i]) * Neuron.DerivedActivation(received[i]);
                errors[0].Add(neuronErr);
            }

            // other layers - error calculation for backpropagation
            if (layers.Count > 1)
            {
                for (int layerNum = (layers.Count - 2); layerNum >= 0; layerNum--)
                {
                    // from last layer to input
                    errors.Add(new List<double>());
                    for (int neuronNum = 0; neuronNum < layers[layerNum].Count; neuronNum++)
                    {
                        double e = 0.0;
                        int errorsLast = errors.Count - 1;
                        for (int neuronNextLayer = 0; neuronNextLayer < layers[layerNum + 1].Count; neuronNextLayer++)
                        {
                            e += layers[layerNum + 1][neuronNextLayer].weights[neuronNum] * errors[errorsLast - 1][neuronNextLayer];
                        }
                        e *= Neuron.DerivedActivation(allNeuronOuts[layerNum][neuronNum]);
                        errors[errorsLast].Add(e);
                    }
                }
            }

            // we put in backwards, so now reverse
            errors.Reverse();

            // TODO: can someone please remake this into some enumerable foreach?

            // recalculate weights for first layer
            for (int neuronNum = 0; neuronNum < layers[0].Count; neuronNum++)
            {
                for (int weightNum = 0; weightNum < layers[0][neuronNum].weights.Count; weightNum++)
                {
                    layers[0][neuronNum].weights[weightNum] = MOMENTUM * (layers[0][neuronNum].weights[weightNum] + alpha * errors[0][neuronNum] * input[weightNum]);
                }
                layers[0][neuronNum].bias = MOMENTUM * (layers[0][neuronNum].bias + alpha * errors[0][neuronNum]);
            }

            // recalculate weights for all other layers
            for (int layerNum = 1; layerNum < layers.Count; layerNum++)
            {
                for (int neuronNum = 0; neuronNum < layers[layerNum].Count; neuronNum++)
                {
                    for (int weightNum = 0; weightNum < layers[layerNum][neuronNum].weights.Count; weightNum++)
                    {
                        layers[layerNum][neuronNum].weights[weightNum] = MOMENTUM * (layers[layerNum][neuronNum].weights[weightNum] + alpha * errors[layerNum][neuronNum] * allNeuronOuts[layerNum - 1][weightNum]);
                    }
                    layers[layerNum][neuronNum].bias = MOMENTUM * (layers[layerNum][neuronNum].bias + alpha * errors[layerNum][neuronNum]);
                }
            }
        }

        public static double CalculateNetworkError(List<double> expected, List<double> received)
        {
            var netErr = 0.0;
            for (int i = 0; i < expected.Count; i++)
            {
                netErr += Math.Pow(expected[i] - received[i], 2.0);
            }
            return netErr / 2.0;
        }

        /// <summary>
        /// Train the network using list of training data.
        /// </summary>
        /// <returns>Network error history</returns>
        public List<double> StartLearning(List<TrainingData> trainingSet, double stopError)
        {
            var ret = new List<double>(200);
            for (int i = 0; i < MAX_LEARNING_EPOCH; i++)
            {
                Console.WriteLine(String.Format("Epoch {0}", i));
                var epochErr = 0.0;
                foreach (TrainingData training in trainingSet)
                {
                    var output = CalculateNetworkOutputFull(training.inputs);
                    RecalculateWeights(training.inputs, output[output.Count - 1], training.expected, output);
                    epochErr += CalculateNetworkError(training.expected, output[output.Count - 1]);
                }
                trainingSet = ShuffleList(trainingSet);
                Console.WriteLine(String.Format("Epoch error: {0}", epochErr));
                ret.Add(epochErr);

                if (epochErr < stopError)
                {
                    return ret;
                }
            }
            return ret;
        }

        public void CheckLayoutCorrect(List<int> layout)
        {
            if (layout.Count < 1)
            {
                throw new ArgumentException("Cannot have 0 layers");
            }
            foreach (int layer in layout)
            {
                if (layer < 1)
                {
                    throw new ArgumentException("Cannot have less than 1 neuron in a layer");
                }
            }
        }

        private List<E> ShuffleList<E>(List<E> inputList)
        {
            List<E> randomList = new List<E>();

            Random r = new Random();
            int randomIndex = 0;
            while (inputList.Count > 0)
            {
                randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
                randomList.Add(inputList[randomIndex]); //add it to the new, random list
                inputList.RemoveAt(randomIndex); //remove to avoid duplicates
            }

            return randomList; //return the new random list
        }
    }
}
