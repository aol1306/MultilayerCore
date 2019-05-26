using System;
using System.Collections.Generic;
using System.Text;

namespace MultilayerCore
{
    public class Neuron
    {
        public const double MIN_RANDOM = -1.0;
        public const double MAX_RANDOM = 1.0;
        public const double LAMBDA = 1.0;

        public List<double> weights;
        public double bias;
        private Random random;

        /// <summary>
        /// Neuron constructor.
        /// Initializes neuron with random weights & bias.
        /// </summary>
        /// <param name="inputs">Amount of inputs</param>
        public Neuron(int inputs)
        {
            if (inputs < 1)
            {
                throw new Exception("Can't have less than 1 input");
            }

            weights = new List<double>(inputs);

            for (int i = 0; i < inputs; i++)
            {
                weights.Add(GenerateRandomInRange(MIN_RANDOM, MAX_RANDOM));
            }

            bias = GenerateRandomInRange(MIN_RANDOM, MAX_RANDOM);
        }

        public static double Activation(double x)
        {
            return 1.0 / (1.0 + Math.Pow(Math.E, -LAMBDA * x));
        }

        public static double DerivedActivation(double x)
        {
            var actX = Activation(x);
            return LAMBDA * actX * (1.0 - actX);
        }

        public double CalculateOutput(List<double> x)
        {
            if (x.Count != weights.Count)
            {
                throw new Exception("Invalid neuron input");
            }
            double net = 0.0;
            for (int i=0; i<x.Count; i++)
            {
                net += x[i] * weights[i];
            }
            net += bias;
            return Activation(net);
        }

        /// <summary>
        /// Generates a random number in given range.
        /// </summary>
        /// <param name="min">Minimum value</param>
        /// <param name="max">Maximum value</param>
        /// <returns>Random number</returns>
        public double GenerateRandomInRange(double min, double max)
        {
            if (max < min)
            {
                throw new Exception("min cannot be greater than max");
            }
            if (random == null)
            {
                random = new Random();
            }
            return min + (max - min) * random.NextDouble();
        }
    }
}
