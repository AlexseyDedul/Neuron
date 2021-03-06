﻿using System.Collections.Generic;

namespace NeuronNetworks
{
    public class Topology
    {
        public int InputCount { get; } //количество входных слоев
        public int OutputCount { get; } //кол вых слоев
        public double LearningRate { get; }
        public List<int> HiddenLayers { get; } // хранение количества нейронов на скрытом слое

        public Topology(int inputCount,  int outputCount, double learningRate, params int[] layers)
        {
            InputCount = inputCount;
            OutputCount = outputCount;
            LearningRate = learningRate;
            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(layers);
        }
    }
}
