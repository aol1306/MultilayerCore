using System;
using System.Collections.Generic;
using System.Text;

namespace MultilayerCore
{
    public class TrainingData
    {
        public List<double> inputs;
        public List<double> expected;

        public TrainingData(List<double> inputs, List<double> expected)
        {
            this.inputs = inputs;
            this.expected = expected;
        }

        public static List<TrainingData> GenerateForXOR()
        {
            var ret = new List<TrainingData>();

            ret.Add(new TrainingData(new List<double> { 0.0, 0.0 }, new List<double> { 0.0 }));
            ret.Add(new TrainingData(new List<double> { 1.0, 0.0 }, new List<double> { 1.0 }));
            ret.Add(new TrainingData(new List<double> { 0.0, 1.0 }, new List<double> { 1.0 }));
            ret.Add(new TrainingData(new List<double> { 1.0, 1.0 }, new List<double> { 0.0 }));

            return ret;
        }

        public static List<TrainingData> GenerateForNumbers()
        {
            var ret = new List<TrainingData>();
            ret.Add(new TrainingData(new List<double> {
                    0.0, 1.0, 1.0, 0.0,
                    1.0, 0.0, 0.0, 1.0,
                    1.0, 0.0, 0.0, 1.0,
                    1.0, 0.0, 0.0, 1.0,
                    1.0, 0.0, 0.0, 1.0,
                    0.0, 1.0, 1.0, 0.0}, new List<double> { 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 }));
            ret.Add(new TrainingData(new List<double> {0.0, 0.0, 1.0, 0.0,
                    0.0, 0.0, 1.0, 0.0,
                    0.0, 0.0, 1.0, 0.0,
                    0.0, 0.0, 1.0, 0.0,
                    0.0, 0.0, 1.0, 0.0,
                    0.0, 0.0, 1.0, 0.0 }, new List<double> { 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 }));
            ret.Add(new TrainingData(new List<double> {  0.0, 1.0, 1.0, 0.0,
                    1.0, 0.0, 0.0, 1.0,
                    0.0, 0.0, 0.0, 1.0,
                    0.0, 0.0, 1.0, 0.0,
                    0.0, 1.0, 0.0, 0.0,
                    1.0, 1.0, 1.0, 1.0}, new List<double> { 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 }));
            ret.Add(new TrainingData(new List<double> { 1.0, 1.0, 1.0, 0.0,
                    0.0, 0.0, 0.0, 1.0,
                    0.0, 1.0, 1.0, 1.0,
                    0.0, 0.0, 0.0, 1.0,
                    0.0, 0.0, 0.0, 1.0,
                    1.0, 1.0, 1.0, 1.0 }, new List<double> { 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 }));
            ret.Add(new TrainingData(new List<double> {  1.0, 0.0, 1.0, 0.0,
                    1.0, 0.0, 1.0, 0.0,
                    1.0, 0.0, 1.0, 0.0,
                    1.0, 1.0, 1.0, 1.0,
                    0.0, 0.0, 1.0, 0.0,
                    0.0, 0.0, 1.0, 0.0}, new List<double> { 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0 }));
            ret.Add(new TrainingData(new List<double> { 1.0, 1.0, 1.0, 1.0,
                    1.0, 0.0, 0.0, 0.0,
                    1.0, 1.0, 1.0, 0.0,
                    0.0, 0.0, 0.0, 1.0,
                    0.0, 0.0, 0.0, 1.0,
                    1.0, 1.0, 1.0, 0.0}, new List<double> { 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0, 0.0 }));
            ret.Add(new TrainingData(new List<double> { 0.0, 1.0, 1.0, 0.0,
                    1.0, 0.0, 0.0, 0.0,
                    1.0, 1.0, 1.0, 0.0,
                    1.0, 0.0, 0.0, 1.0,
                    1.0, 0.0, 0.0, 1.0,
                    1.0, 1.0, 1.0, 1.0}, new List<double> { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0, 0.0 }));
            ret.Add(new TrainingData(new List<double> { 1.0, 1.0, 1.0, 1.0,
                    0.0, 0.0, 0.0, 1.0,
                    0.0, 0.0, 0.0, 1.0,
                    0.0, 0.0, 1.0, 0.0,
                    0.0, 1.0, 0.0, 0.0,
                    1.0, 0.0, 0.0, 0.0}, new List<double> { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0, 0.0 }));
            ret.Add(new TrainingData(new List<double>
            {
                    0.0, 1.0, 1.0, 0.0,
                    1.0, 0.0, 0.0, 1.0,
                    0.0, 1.0, 1.0, 0.0,
                    1.0, 0.0, 0.0, 1.0,
                    1.0, 0.0, 0.0, 1.0,
                    0.0, 1.0, 1.0, 0.0 }, new List<double> { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0, 0.0 }));
            ret.Add(new TrainingData(new List<double> { 0.0, 1.0, 1.0, 0.0,
                    1.0, 0.0, 0.0, 1.0,
                    0.0, 1.0, 1.0, 1.0,
                    0.0, 0.0, 0.0, 1.0,
                    0.0, 0.0, 1.0, 0.0,
                    1.0, 1.0, 0.0, 0.0 }, new List<double> { 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 1.0 }));

            return ret;
        }
    }
}
