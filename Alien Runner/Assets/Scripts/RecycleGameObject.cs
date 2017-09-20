using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleGameObject : MonoBehaviour , IRecyclable{

    public void Restart()
    {

    }

    public void Shutdown()
    {
        gameObject.SetActive(false);
    }
}
