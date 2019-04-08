using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AreaAbstract : MonoBehaviour {

    public AreaScriptable MyArea;
    private List<GameObject> loaded = new List<GameObject>();
    public SpawningObject[] ToLoad;

	public void LoadArea()
    {
        foreach(SpawningObject so in ToLoad)
        {
            LoadObj(so.Prefab, so.MyLocation);
        }
    }

    public void LoadObj(GameObject prefab, Vector2 spawnLocation)
    {
        GameObject newObj = Instantiate(prefab, spawnLocation + MyArea.Location, Quaternion.identity);
        loaded.Add(newObj);
    }

    public void RemoveObj(GameObject obj)
    {
        loaded.Remove(obj);
        Destroy(obj);
    }

    public void DeLoadArea()
    {
        while(loaded.Count > 0)
        {
            GameObject obj = loaded[0];
            RemoveObj(obj);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            GameObject.FindGameObjectWithTag("LevelHandler").GetComponent<LevelHandler>().StartMoveArea(this);
        }
        
    }
}

[System.Serializable]
public class SpawningObject
{
    public GameObject Prefab;
    public Vector2 MyLocation;
}
