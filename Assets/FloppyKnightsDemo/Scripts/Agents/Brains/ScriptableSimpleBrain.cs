using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FloppyKnights.Agents
{
    [CreateAssetMenu(fileName = "SimpleBrain", menuName = "Floppy Knights/Brains/Simple Brain")]
    public class ScriptableSimpleBrain : ScriptableBrain
    {
        protected override IBrain GetBrain()
        {
            return new SimpleBrain();
        }
    }
}

