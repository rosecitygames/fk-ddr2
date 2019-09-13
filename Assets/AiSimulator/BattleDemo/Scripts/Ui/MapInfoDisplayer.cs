using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using IndieDevTools.Maps;

namespace IndieDevTools.Demo.BattleSimulator
{
    public class MapInfoDisplayer : MonoBehaviour
    {
        [SerializeField]
        AbstractMap map;
        IMap Map => map;

    }
}
