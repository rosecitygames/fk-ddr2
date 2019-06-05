using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Demo.BattleSimulator
{
    [System.Serializable]
    public class MapGeneratorElementData
    {
        [SerializeField]
        GameObject prefab = null;
        public GameObject Prefab => prefab;

        [SerializeField]
        [Range(0.0f, 1.0f)]
        float perecentage = 0.0f;
        public float Percentage => perecentage;
    }
}
