using UnityEngine;

internal class Util
{
    public static GameObject FindByName(string name)
    {
        foreach (var go in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
        {
            if (go.name == name)
                return go as GameObject;
        }
        return null;
    }

    public static string GameObjectPath(Transform transform)
    {
        string path = transform.name;
        while (transform.parent != null)
        {
            transform = transform.parent;
            path = transform.name + "/" + path;
        }
        return path;
    }
}