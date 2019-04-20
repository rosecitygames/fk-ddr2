using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RCG.Advertisements
{
    public class Advertiser : IAdvertiser
    {
        void IAdvertiser.PublishAdvertisement(IAdvertisement advertisement)
        {
            PublishAdvertisement(advertisement);
        }

        protected void PublishAdvertisement(IAdvertisement advertisement)
        {

        }

        public static IAdvertiser Create()
        {
            return new Advertiser();
        }
    }
}
