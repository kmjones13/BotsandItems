using UnityEngine;
using Zenject;

public class ObjectManagerInstaller : MonoInstaller
{
    [SerializeField] private ObjectManager objectManager;

    public override void InstallBindings()
    {
        GameObject botPrefab = objectManager.botPrefab;
        GameObject itemPrefab = objectManager.itemPrefab;


        Color botBaseColor = objectManager.botBaseColor;
        Color botHighlightColor = objectManager.botHighlightColor;
        Color itemBaseColor = objectManager.itemBaseColor;
        Color itemHighlightColor = objectManager.itemHighlightColor;

        // Bind the prefabs and colors to their respective interfaces
        Container.BindFactory<Bot, Bot.Factory>()
            .FromComponentInNewPrefab(botPrefab)
            .WithGameObjectName("BotPrefab");


        Container.BindFactory<Item, Item.Factory>()
            .FromComponentInNewPrefab(itemPrefab)
            .WithGameObjectName("ItemPrefab");

        Container.Bind<Color>().WithId("BotBaseColor").FromInstance(botBaseColor);
        Container.Bind<Color>().WithId("BotHighlightColor").FromInstance(botHighlightColor);
        Container.Bind<Color>().WithId("ItemBaseColor").FromInstance(itemBaseColor);
        Container.Bind<Color>().WithId("ItemHighlightColor").FromInstance(itemHighlightColor);
    }
}
