using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

public class ObjectManager : MonoBehaviour
{
    // used this class as a game manager. could have pulled some things out like the file stuff
    // but since this takes care of the spawn and the file stuff does to combined for now.
    // somethings are hard coded best pactrice would be to make those configuable 
    public GameObject botPrefab;
    public GameObject itemPrefab;

    public Color botBaseColor;
    public Color botHighlightColor;
    public Color itemBaseColor;
    public Color itemHighlightColor;

    [SerializeField] private List<Bot> bots = new List<Bot>();
    [SerializeField] private List<Item> items = new List<Item>();

    private string filePath;
    private ObjectBase closestObject;

    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "ObjectsStateData.xml");
    }

    public void Start()
    {
        foreach (Bot bot in bots)
        {
            bot.Initialize(this, botBaseColor, botHighlightColor);
        }

        foreach (Item item in items)
        {
            item.Initialize(this, itemBaseColor, itemHighlightColor);
        }
        Load();
    }

    /// <summary>
    /// Spawn a bot object in the scene
    /// </summary>
    public void SpawnBot()
    {
        GameObject botObject = Instantiate(botPrefab, GetRandomPosition(), Quaternion.identity);
        Bot bot = botObject.GetComponent<Bot>();
        bot.Initialize(this, botBaseColor, botHighlightColor);
        bots.Add(bot);
    }

    /// <summary>
    /// Spawn a item object in scene
    /// </summary>
    public void SpawnItem()
    {
        GameObject itemObject = Instantiate(itemPrefab, GetRandomPosition(), Quaternion.identity);
        Item item = itemObject.GetComponent<Item>();
        item.Initialize(this, itemBaseColor, itemHighlightColor);
        items.Add(item);
    }

    /// <summary>
    /// Actually highlights the closests object
    /// </summary>
    /// <param name="playerPosition"></param>
    public void HighlightClosestObject(Vector3 playerPosition)
    {
        if (closestObject != null)
            closestObject.SetHighlight(false);

        closestObject = GetClosestObject(playerPosition);

        if (closestObject != null)
            closestObject.SetHighlight(true);
    }

    /// <summary>
    /// Save the objects position
    /// </summary>
    public void Save()
    {
        ObjectManagerData data = new ObjectManagerData();
        foreach (Bot bot in bots)
        {
            ObjectPostionData objectData = new ObjectPostionData();
            objectData.x = bot.position.x;
            objectData.y = bot.position.y;
            objectData.z = bot.position.z;
            data.botsData.Add(objectData);
        }
        foreach (Item item in items)
        {
            ObjectPostionData objectData = new ObjectPostionData();
            objectData.x = item.position.x;
            objectData.y = item.position.y;
            objectData.z = item.position.z;
            data.itemsData.Add(objectData);
        }

        XmlSerializer serializer = new XmlSerializer(typeof(ObjectManagerData));
        using (StreamWriter streamWriter = new StreamWriter(filePath))
        {
            serializer.Serialize(streamWriter, data);
        }
    }

    /// <summary>
    /// Load the position of objects
    /// </summary>
    public void Load()
    {
        if (File.Exists(filePath))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(ObjectManagerData));
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                ObjectManagerData data = (ObjectManagerData)serializer.Deserialize(streamReader);

                // Clear existing objects
                ClearObjects();

                // Create new objects from data
                foreach (ObjectPostionData objectData in data.botsData)
                {
                    Vector3 position = new Vector3(objectData.x, objectData.y, objectData.z);
                    GameObject botObject = Instantiate(botPrefab, position, Quaternion.identity);
                    Bot bot = botObject.GetComponent<Bot>();
                    bot.Initialize(this, botBaseColor, botHighlightColor);
                    bots.Add(bot);
                }
                foreach (ObjectPostionData objectData in data.itemsData)
                {
                    Vector3 position = new Vector3(objectData.x, objectData.y, objectData.z);
                    GameObject itemObject = Instantiate(itemPrefab, position, Quaternion.identity);
                    Item item = itemObject.GetComponent<Item>();
                    item.Initialize(this, itemBaseColor, itemHighlightColor);
                    items.Add(item);
                }
            }
        }
        else
        {
            Debug.LogWarning("Object manager state file not found: " + filePath);
        }
    }

    /// <summary>
    /// Remove object to save new ones
    /// </summary>
    private void ClearObjects()
    {
        foreach (Bot bot in bots)
        {
            Destroy(bot.gameObject);
        }
        foreach (Item item in items)
        {
            Destroy(item.gameObject);
        }
        bots.Clear();
        items.Clear();
    }

    /// <summary>
    /// Get closests bot ot item object 
    /// </summary>
    /// <param name="playerPosition"></param>
    /// <returns></returns>
    public ObjectBase GetClosestObject(Vector3 playerPosition)
    {
        ObjectBase closestObject = null;
        float closestDistance = Mathf.Infinity;

        foreach (Bot bot in bots)
        {
            float distance = Vector3.Distance(bot.transform.position, playerPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = bot;
            }
        }

        foreach (Item item in items)
        {
            float distance = Vector3.Distance(item.transform.position, playerPosition);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = item;
            }
        }

        return closestObject;
    }

    /// <summary>
    /// Get random postion for new spaw object
    /// </summary>
    /// <returns></returns>
    private Vector3 GetRandomPosition()
    {
        float x = Random.Range(-10f, 10f);
        float z = Random.Range(-10f, 10f);
        return new Vector3(x, 0f, z);
    }

    [System.Serializable]
    public class ObjectManagerData
    {
        public List<ObjectPostionData> botsData = new List<ObjectPostionData>();
        public List<ObjectPostionData> itemsData = new List<ObjectPostionData>();
    }

    [System.Serializable]
    public class ObjectPostionData
    {
        public float x;
        public float y;
        public float z;
    }
}

