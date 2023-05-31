using UnityEngine;
using Zenject;

public class Item : ObjectBase
{
    public override void Initialize(ObjectManager manager, Color baseColor, Color highlightColor)
    {
        base.Initialize(manager, baseColor, highlightColor);
    }

    public class Factory : PlaceholderFactory<Item>
    {
    }
}
