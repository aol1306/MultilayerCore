using System;
using System.Collections.Generic;

namespace MultilayerCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var network = new Network(0.3, new List<int> { 15, 10 }, 24);
            var trainingData = TrainingData.GenerateForNumbers();

            network.StartLearning(trainingData, 0.1);

            foreach (TrainingData training in TrainingData.GenerateForNumbers())
            {
                Console.Write("Expected: ");
                PrintList(training.expected);
                Console.Write("\tGot: ");
                PrintList(network.CalculateNetworkOutput(training.inputs));
                Console.WriteLine();
            }
        }

        static void PrintList(List<double> vs)
        {
            Console.Write("{");
            foreach (var x in vs)
            {
                Console.Write(String.Format("{0,3}", Math.Round(x, 1)));
                Console.Write(" ");
            }
            Console.Write("}");
        }
    }
}
