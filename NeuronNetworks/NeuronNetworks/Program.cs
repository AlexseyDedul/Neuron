using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NeuronNetworks
{
    class Program
    {
        public static void DatasetTest()
        {
            var outputs = new List<double>();
            var inputs = new List<double[]>();
            using (var sr = new StreamReader("heart.csv"))
            {
                var header = sr.ReadLine();

                while (!sr.EndOfStream)
                {
                    var row = sr.ReadLine();
                    var temp = row.Split(',');
                    var values = temp.Select(v => Convert.ToDouble(v.Replace(".", ","))).ToList();
                    var output = values.Last();
                    var input = values.Take(values.Count - 1).ToArray();

                    outputs.Add(output);
                    inputs.Add(input);
                }
            }

            var inputSignals = new double[inputs.Count, inputs[0].Length];
            for (int i = 0; i < inputSignals.GetLength(0); i++)
            {
                for (var j = 0; j < inputSignals.GetLength(1); j++)
                {
                    inputSignals[i, j] = inputs[i][j];
                }
            }

            var topology = new Topology(outputs.Count, 1, 0.1, outputs.Count / 2);
            var neuralNetwork = new NeuralNetwork(topology);
            var differense = neuralNetwork.Learn(outputs.ToArray(), inputSignals, 1000);

            var results = new List<double>();

            for (int i = 0; i < outputs.Count; i++)
            {
                results.Add(neuralNetwork.Predict(inputs[i]).Output);
            }

            for (int i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(outputs[i], 2);
                var actual = Math.Round(results[i], 2);
                Console.WriteLine(expected + "   " + actual);

            }
        }

        public static void ConverterPicture()
        {
            var converter = new PictureConverter();

            var inputs = converter.Convert(@"Images\Uninfected.png");
            converter.Save("f:\\image.png", inputs);

        }

        public static void RecognizeImage()
        {
            var size = 1000;
            var parasitizedPath = @"Parasitized\";
            var unparasitizedPath = @"Uninfected\";

            var converter = new PictureConverter();
            var testParasitizedImageInput = converter.Convert(@"Images\Parasitized.png");
            var testUnparasitizedImageInput = converter.Convert(@"Images\Uninfected.png");

            var topology = new Topology(testParasitizedImageInput.Count, 1, 0.1, testParasitizedImageInput.Count / 2);
            var neuralNetwork = new NeuralNetwork(topology);

            double[,] parasitizedInputs = GetData(parasitizedPath, converter, testParasitizedImageInput, size);
            neuralNetwork.Learn(new double[] { 1 }, parasitizedInputs, 1);

            double[,] unparasitizedInputs = GetData(unparasitizedPath, converter, testUnparasitizedImageInput, size);
            neuralNetwork.Learn(new double[] { 0 }, unparasitizedInputs, 1);

            var par = neuralNetwork.Predict(testParasitizedImageInput.Select(t => (double)t).ToArray());
            var unpar = neuralNetwork.Predict(testUnparasitizedImageInput.Select(t => (double)t).ToArray());

            Console.WriteLine(1 + Math.Round(par.Output, 2));
            Console.WriteLine(0 + Math.Round(unpar.Output, 2));
        }

        private static double[,] GetData(string parasitizedPath, PictureConverter converter, List<int> testImageInput, int size)
        {
            var images = Directory.GetFiles(parasitizedPath);
            var result = new double[size, testImageInput.Count];
            for (int i = 0; i < size; i++)
            {
                var image = converter.Convert(images[i]);
                for (int j = 0; j < image.Count; j++)
                {
                    result[i, j] = image[j];
                }
            }

            return result;
        }

        public static void Main(string[] args)
        {
            RecognizeImage();
            //ConverterPicture();
            //DatasetTest();
            /*var outputs = new double[] { 0, 0, 1, 0, 0, 0, 1, 0, 1, 1, 1, 1, 1, 0, 1, 1 };
            var inputs = new double[,]
            {
                // Результат пациента - пациент болен - 1
                //                      пациент здоров - 0
                // Неправильная температура Т
                // Хороший возраст А
                // курит S 
                // правильно питается F
                //T  A  S  F
                { 0, 0, 0, 0 },
                { 0, 0, 0, 1 },
                { 0, 0, 1, 0 },
                { 0, 0, 1, 1 },
                { 0, 1, 0, 0 },
                { 0, 1, 0, 1 },
                { 0, 1, 1, 0 },
                { 0, 1, 1, 1 },
                { 1, 0, 0, 0 },
                { 1, 0, 0, 1 },
                { 1, 0, 1, 0 },
                { 1, 0, 1, 1 },
                { 1, 1, 0, 0 },
                { 1, 1, 0, 1 },
                { 1, 1, 1, 0 },
                { 1, 1, 1, 1 }
            };

            var topology = new Topology(4, 1, 0.1, 2);
            var neuralNetwork = new NeuralNetwork(topology);
            //neuralNetwork.Layers[1].Neurons[0].SetWeights(0.5, -0.1, 0.3, -0.1);
            //neuralNetwork.Layers[1].Neurons[1].SetWeights(0.1, -0.3, 0.7, -0.3);
            //neuralNetwork.Layers[2].Neurons[0].SetWeights(1.2, 0.8);
            var differense = neuralNetwork.Learn(outputs, inputs, 10000);

            var results = new List<double>();

            for(int i = 0; i < outputs.Length; i++)
            {
                var row = NeuralNetwork.GetRow(inputs, i);
                results.Add(neuralNetwork.FeedForward(row).Output);
            }

            for(int  i = 0; i < results.Count; i++)
            {
                var expected = Math.Round(outputs[i], 2);
                var actual = Math.Round(results[i], 2);
                Console.WriteLine(expected + "   " + actual);
                
            }*/



            Console.ReadKey();
        }

       
    }
}
