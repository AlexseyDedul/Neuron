using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetworks
{
    class Program
    {
        static void Main()
        {
            var dataset = new List<Tuple<double, double[]>>
            {
                // Результат пациента - пациент болен - 1
                //                      пациент здоров - 0
                // Неправильная температура Т
                // Хороший возраст А
                // курит S 
                // правильно питается F
                //                                             T  A  S  F
                new Tuple<double, double[]> (0, new double[] { 0, 0, 0, 0 }),
                new Tuple<double, double[]> (0, new double[] { 0, 0, 0, 1 }),
                new Tuple<double, double[]> (1, new double[] { 0, 0, 1, 0 }),
                new Tuple<double, double[]> (0, new double[] { 0, 0, 1, 1 }),
                new Tuple<double, double[]> (0, new double[] { 0, 1, 0, 0 }),
                new Tuple<double, double[]> (0, new double[] { 0, 1, 0, 1 }),
                new Tuple<double, double[]> (1, new double[] { 0, 1, 1, 0 }),
                new Tuple<double, double[]> (0, new double[] { 0, 1, 1, 1 }),
                new Tuple<double, double[]> (1, new double[] { 1, 0, 0, 0 }),
                new Tuple<double, double[]> (1, new double[] { 1, 0, 0, 1 }),
                new Tuple<double, double[]> (1, new double[] { 1, 0, 1, 0 }),
                new Tuple<double, double[]> (1, new double[] { 1, 0, 1, 1 }),
                new Tuple<double, double[]> (1, new double[] { 1, 1, 0, 0 }),
                new Tuple<double, double[]> (0, new double[] { 1, 1, 0, 1 }),
                new Tuple<double, double[]> (1, new double[] { 1, 1, 1, 0 }),
                new Tuple<double, double[]> (1, new double[] { 1, 1, 1, 1 })

            };

            var topology = new Topology(4, 1, 0.1, 2);
            var neuralNetwork = new NeuralNetwork(topology);
            //neuralNetwork.Layers[1].Neurons[0].SetWeights(0.5, -0.1, 0.3, -0.1);
            //neuralNetwork.Layers[1].Neurons[1].SetWeights(0.1, -0.3, 0.7, -0.3);
            //neuralNetwork.Layers[2].Neurons[0].SetWeights(1.2, 0.8);
            var differense = neuralNetwork.Learn(dataset, 10000);

            var results = new List<double>();
            foreach(var data in dataset)
            {
                results.Add(neuralNetwork.FeedForward(data.Item2).Output);
            }

            for(int  i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(dataset[i].Item1, 3);
                var actual = Math.Round(results[i], 3);
                Console.WriteLine(expected + "   " + actual);
                
            }
            Console.ReadKey();
        }
    }
}
