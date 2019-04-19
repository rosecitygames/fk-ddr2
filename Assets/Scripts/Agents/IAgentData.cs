using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG
{
    public interface IAgentData : IDescribable, IStatsCollection, IDesiresCollection
    {
        IAgentData Copy();
    }
}
