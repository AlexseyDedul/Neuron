﻿using System;
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

        public double FeedForward(List<double> inputs)
        {
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

        public void SetWeights(params double[] weights)
        {

            //TODO: удалить после добавления возможности обучения сети
            for(int i = 0; i < weights.Length; i++)
            {
                Weights[i] = weights[i];
            }
        }

        public override string ToString()
        {
            return Output.ToString();
        }
    }
}
