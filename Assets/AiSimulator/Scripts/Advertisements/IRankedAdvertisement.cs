using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace IndieDevTools.Advertisements
{
    public interface IRankedAdvertisement : IAdvertisement
    {
        int Rank { get; set; }
    }
}
