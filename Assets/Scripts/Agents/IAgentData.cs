using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Agents
{
    public interface IAgentData : IDescribable, IStatsCollection, IDesiresCollection
    {
        IAgentData Copy();
    }
}
