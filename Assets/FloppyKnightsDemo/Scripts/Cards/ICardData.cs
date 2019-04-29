﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RCG.Attributes;

namespace RCG.Demo.FloppyKnights.Cards
{
    public interface ICardData : IDescribable
    {
        List<AbstractCardAction> CardActions { get; }
    }
}
