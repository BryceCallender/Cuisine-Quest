using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AreaAbstract : MonoBehaviour
{

    public AreaScriptable MyArea;
    private List<GameObject> loaded = new List<GameObject>();
    public List<SpawningObject> ToLoad = new List<SpawningObject>();

    public void LoadArea()
    {
        foreach (SpawningObject so in ToLoad)
        {
            //LoadObj(so.Prefab, so.MyLocation);
            LoadObj(so);
        }
    }

    public void LoadObj(SpawningObject objOrigin)
    {
        GameObject prefab = objOrigin.Prefab;
        Vector2 spawnLocation = objOrigin.MyLocation;
        GameObject newObj = Instantiate(prefab, spawnLocation + MyArea.Location, Quaternion.identity);
        newObj.GetComponent<SpawnObject>().SetMyOrigin(objOrigin);
        //newObj.SendMessage("SetMyOrigin", objOrigin);
        loaded.Add(newObj);
    }

    public abstract void DestroyObj(GameObject obj); //object killed or permanently removed
    public abstract void RemoveObj(GameObject obj); //object removed until area reload

    public void removeObj(GameObject obj)
    {
        loaded.Remove(obj);
        Destroy(obj);
    }

    public void DeLoadArea()
    {
        while (loaded.Count > 0)
        {
            GameObject obj = loaded[0];
            removeObj(obj);
        }
    }

    public void AddObj(GameObject obj)
    {
        loaded.Add(obj);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().StartMoveArea(this);
        }

    }

    //public SpawningObject GetSpawingObject(GameObject obj)
    //{
    //    SpawningObject returnObj = null;
    //    for(int i = 0; i < ToLoad.Count; i += 1)
    //    {
    //        //needs to be filled in
    //    }
    //    return returnObj;
    //}
}

[System.Serializable]
public class SpawningObject
{
    public GameObject Prefab;
    public Vector2 MyLocation;
}