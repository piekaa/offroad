using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drop : MonoBehaviour
{

    [SerializeField]
    public GameObject SpawnObject;

    public GameObject Spawnable;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = SpawnObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.clear;
    }

    public void Spawn()
    {
        Spawnable.transform.position = spriteRenderer.transform.position;
        Spawnable.transform.rotation = transform.rotation = Quaternion.identity;

    }
}
