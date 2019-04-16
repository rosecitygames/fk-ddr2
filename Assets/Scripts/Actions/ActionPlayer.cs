using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace RCG.Actions
{
    public class ActionPlayer : AbstractAction, IActionEnumerator
    {

        private IActionEnumerator parallelActionEnumerator;
        private IActionEnumerator ParallelActionEnumerator
        {
            get
            {
                if (parallelActionEnumerator == null)
                {
                    parallelActionEnumerator = new ParallelActionEnumerator();
                    parallelActionEnumerator.Parent = this;
                }
                return parallelActionEnumerator;
            }
        }

        protected List<IActionEnumerator> layers = new List<IActionEnumerator>();

        protected int loopCount;
        int IActionEnumerator.LoopCount
        {
            get { return loopCount; }
            set { loopCount = value; }
        }

        protected int currentLoop = 0;
        int IActionEnumerator.CurrentLoop
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
            foreach (IActionEnumerator layer in layers)
            {
                int index = layer.GetIndexOfAction(action);
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

        int IActionEnumerator.GetIndexOfAction(IAction action)
        {
            foreach(IActionEnumerator layer in layers)
            {
                int index = layer.GetIndexOfAction(action);
                if (index > 0)
                {
                    return index;
                }
            }
            return -1;
        }

        public IActionEnumerator CreateLayer()
        {
            IActionEnumerator layer = CreateLayer(0);
            return layer;
        }
        public IActionEnumerator CreateLayer(int loopCount)
        {
            IActionEnumerator layer = new SerialActionEnumerator();
            layer.LoopCount = loopCount;
            ParallelActionEnumerator.AddAction(layer);
            layers.Add(layer);
            return layer;
        }

        public void DestroyLayer(int layer)
        {
            if (layer < LayersCount)
            {
                IActionEnumerator selectedLayer = layers[layer];
                ParallelActionEnumerator.RemoveAction(selectedLayer);
                layers.Remove(selectedLayer);
                selectedLayer.Destroy();
            }
        }

        public void HandleCompletedAction(IAction action)
        {
            if (isCompleted == false)
            {
                if (action == ParallelActionEnumerator)
                {
                    if (currentLoop <= 0 && loopCount < 0)
                    {
                        OnStart();
                    }
                    else if (currentLoop < loopCount - 1)
                    {
                        int nextLoop = currentLoop + 1;
                        OnStart();
                        currentLoop = nextLoop;
                    }
                    else
                    {
                        Complete();
                    }
                }
            }
        }

        override protected void OnStart()
        {
            currentLoop = 0;
            isCompleted = false;
            ParallelActionEnumerator.Start();
        }

        override protected void OnStop()
        {
            ParallelActionEnumerator.Stop();
        }

        override protected void OnDestroy()
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