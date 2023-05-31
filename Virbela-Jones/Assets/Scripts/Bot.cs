using UnityEngine;
using Zenject;

public class Bot : ObjectBase
{
    public override void Initialize(ObjectManager manager, Color baseColor, Color highlightColor)
    {
        base.Initialize(manager, baseColor, highlightColor);
    }

    // Factory class for creating Bot instances
    public class Factory : PlaceholderFactory<Bot>
    {
    }
}
