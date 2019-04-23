using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Advertisements
{
    public interface IRankedAdvertisement : IAdvertisement
    {
        int Rank { get; set; }
    }
}
