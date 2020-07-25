using System;
using System.Collections.Generic;

namespace NeuronNetworks
{
    public class Neuron
    {
        public List<double> Weights { get; } //Лист весов состояний
        public NeuronType NeuronType { get; } //тип
        public double Output{ get; private set; } //сохранение результата

        public Neuron(int inputCount, NeuronType type = NeuronType.Normal)
        {
            NeuronType = type;
            Weights = new List<double>();

            for(int i = 0; i < inputCount; i++)
            {
                Weights.Add(1);
            }
        }

        public double FeesForward(List<double> inputs)
        {
            //TODO: проверка на корректность входных данных
            var sum = 0.0;
            for(int i = 0; i < inputs.Count; i++)
            {
                sum += inputs[i] * Weights[i];
            }

            Output = Sigmoid(sum);
            return Output;
        }

        private double Sigmoid(double x)
        {
            var result = 1.0 / (1.0 + Math.Pow(Math.E, -x));
            return result;
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
