using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    private SpawningObject myOrigin;

    public void SetMyOrigin(SpawningObject myOrigin)
    {
        this.myOrigin = myOrigin;
    }

    public SpawningObject GetMyOrigin()
    {
        return myOrigin;
    }
}
