using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
[RequireComponent(typeof(SpriteRenderer))]
public class SpriteDuplication : MonoBehaviour
{
    [Range(1, 20)]
    public int count = 1;

    SpriteRenderer sprite;
    float offset = 0;

    private void Start()
    {
        if (Application.IsPlaying(this))
        {
            Destroy(this);
        }
    }

    private void Update()
    {
        RepeatedObject();
    }

    private void RepeatedObject()
    {
        offset = GetComponent<SpriteRenderer>().bounds.extents.x * 2;

        if (transform.childCount < count)
        {
            for (int i = transform.childCount; i < count; i++)
            {
                Transform instantiate = Instantiate(transform.GetChild(0), transform);
                instantiate.transform.position = new Vector3(transform.position.x + i*offset, transform.position.y, transform.position.z);
            }
        }
        else if (count < transform.childCount)
        {
            for (int i = transform.childCount-1; i >= count; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }
    }
}

