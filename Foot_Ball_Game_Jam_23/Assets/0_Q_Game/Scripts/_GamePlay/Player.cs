using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    public Transform tf_Ball_In;

    [Tooltip("Speed của cầu thủ sút")]
    public float force_Kick;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
    }
    
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(IE_Regist_Manager());
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    IEnumerator IE_Regist_Manager()
    {
        yield return new WaitUntil( ()=> IngameManager.ins != null );
        IngameManager.ins.list_Player_Inlevel.Add(this);
    }
}
