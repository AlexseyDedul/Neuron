using System.Collections.Generic;

namespace NeuronNetworks
{
    public class Topology
    {
        public int inputCount { get; } //количество входных слоев
        public int OutputCount { get; } //кол вых слоев
        public List<int> HiddenLayers { get; } // хранение количества нейронов на скрытом слое

        public Topology(int inputCount,  int OutputCount, params int[] layers)
        {
            inputCount = inputCount;
            OutputCount = OutputCount;
            HiddenLayers = new List<int>();
            HiddenLayers.AddRange(layers);
        }
    }
}
