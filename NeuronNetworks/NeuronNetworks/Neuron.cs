using System;
using System.Collections.Generic;

namespace NeuronNetworks
{
    public class Neuron
    {
        public List<double> Weights { get; } //Лист весов состояний
        public List<double> Input { get; } //входные сигналы
        public NeuronType NeuronType { get; } //тип
        public double Output{ get; private set; } //сохранение результата
        public double Delta { get; private set; }

        public Neuron(int inputCount, NeuronType type = NeuronType.Normal)
        {
            NeuronType = type;
            Weights = new List<double>();
            Input = new List<double>();

            InitWeightRandomValue(inputCount);
        }

        private void InitWeightRandomValue(int inputCount)
        {
            var rnd = new Random();

            for (int i = 0; i < inputCount; i++)
            {
                if(NeuronType == NeuronType.Input)
                {
                    Weights.Add(1); 
                }
                else
                {
                    Weights.Add(rnd.NextDouble());
                }
                Input.Add(0);
            }
        }

        public double FeedForward(List<double> inputs)
        {
            for(int i = 0; i < inputs.Count; i++)
            {
                Input[i] = inputs[i];
            }

            //TODO: проверка на корректность входных данных
            var sum = 0.0;
            for(int i = 0; i < inputs.Count; i++)
            {
                sum += inputs[i] * Weights[i];
            }

            if (NeuronType != NeuronType.Input)
            {
                Output = Sigmoid(sum);
            }
            else
            {
                Output = sum;
            }
            return Output;
        }

        private double Sigmoid(double x)
        {
            var result = 1.0 / (1.0 + Math.Pow(Math.E, -x));
            return result;
        }

        private double SigmoidDx(double x)
        {
            var sigmoid = Sigmoid(x);
            var result = sigmoid / (1 - sigmoid);
            return result;
        }

        public void Learn(double error, double learningRate)
        {
            if(NeuronType == NeuronType.Input)
            {
                return;
            }

            Delta = error * SigmoidDx(Output);

            for(int i = 0; i < Weights.Count; i++)
            {
                var weight = Weights[i];
                var input = Input[i];

                var newWeigth = weight - input * Delta * learningRate;
                Weights[i] = newWeigth;    
            }
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
