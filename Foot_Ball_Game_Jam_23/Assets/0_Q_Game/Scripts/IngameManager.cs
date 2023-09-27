using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadLevel()
    {
        Timer.Schedule(this, 1, () =>
        {

        });

        // ảnh 100 * 100: 
    }    
}
