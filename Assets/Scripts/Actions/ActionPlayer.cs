using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RCG.Actions
{
    public class ActionPlayer : AbstractAction, IActionEnumerator
    {

        private ParallelActionEnumerator parallelActionEnumerator;
        private ParallelActionEnumerator ParallelActionEnumerator
        {
            get
            {
                if (parallelActionEnumerator == null)
                {
                    ParallelActionEnumerator newParallelActionEnumerator = new ParallelActionEnumerator();
                    newParallelActionEnumerator.Parent = this;
                    parallelActionEnumerator = newParallelActionEnumerator;
                }
                return parallelActionEnumerator;
            }
        }

        protected List<SerialActionEnumerator> layers = new List<SerialActionEnumerator>();

        protected int loopCount;
        public int LoopCount
        {
            get { return loopCount; }
            set { loopCount = value; }
        }

        protected int currentLoop = 0;
        public int CurrentLoop
        {
            get { return currentLoop; }
        }

        public int LayersCount
        {
            get { return layers.Count; }
        }

        public void AddAction(IAction action)
        {
            AddAction(action, -1);
        }
        public void AddAction(IAction action, int index)
        {
            AddAction(action, -1, -1);
        }
        public void AddAction(IAction action, int index, int layer)
        {
            if (layer < 0)
            {
                if (LayersCount <= 0)
                {
                    SerialActionEnumerator newLayer = new SerialActionEnumerator();
                    ParallelActionEnumerator.AddAction(newLayer);
                    layers.Add(newLayer);
                }
                layer = LayersCount - 1;
            }

            if (layer < LayersCount)
            {
                IActionEnumerator actionQueue = layers[layer];
                actionQueue.AddAction(action, index);
            }
        }

        public void RemoveAction(IAction action)
        {
            foreach (SerialActionEnumerator layer in layers)
            {
                int index = layer.IndexOfAction(action);
                if (index >= 0)
                {
                    layer.RemoveAction(action);
                    break;
                }
            }
        }
        public void RemoveAction(IAction action, int layer)
        {
            IActionEnumerator selectedLayer = layers[layer];
            selectedLayer.RemoveAction(action);
        }

        public IActionEnumerator CreateLayer()
        {
            IActionEnumerator layer = CreateLayer(0);
            return layer;
        }
        public IActionEnumerator CreateLayer(int loopCount)
        {
            SerialActionEnumerator layer = new SerialActionEnumerator();
            layer.LoopCount = loopCount;
            ParallelActionEnumerator.AddAction(layer);
            layers.Add(layer);
            return layer;
        }

        public void DestroyLayer(int layer)
        {
            if (layer < LayersCount)
            {
                SerialActionEnumerator selectedLayer = layers[layer];
                ParallelActionEnumerator.RemoveAction(selectedLayer);
                layers.Remove(selectedLayer);
                selectedLayer.Destroy();
            }
        }

        public void CompleteAction(IAction action)
        {
            if (IsCompleted == false)
            {
                if (action == ParallelActionEnumerator)
                {
                    if (currentLoop <= 0 && loopCount < 0)
                    {
                        Start();
                    }
                    else if (currentLoop < loopCount - 1)
                    {
                        int nextLoop = currentLoop + 1;
                        Start();
                        currentLoop = nextLoop;
                    }
                    else
                    {
                        Complete();
                    }
                }
            }
        }

        override public void Start()
        {
            base.Start();
            currentLoop = 0;
            IsCompleted = false;
            ParallelActionEnumerator.Start();
        }

        override public void Stop()
        {
            ParallelActionEnumerator.Stop();
            base.Stop();
        }

        override public void Destroy()
        {
            ParallelActionEnumerator.Destroy();
            if (layers != null)
            {
                layers.Clear();
                layers = null;
            }
        }
    }
}