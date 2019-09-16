using System.Collections.Generic;

namespace IndieDevTools.Traits
{
    public interface ITraitCollection : ICopyable<ITraitCollection>
    {
        List<ITrait> Traits { get; }
        ITrait GetTrait(string id);

        void AddTrait(ITrait attribute);
        void RemoveTrait(ITrait attribute);
        void RemoveTrait(string id);
        void Clear();
    }
}
