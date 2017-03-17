using UnityEngine;
using System.Collections;

public class CustomTransform : MonoBehaviour {

    [SerializeField]
    public GameObject artificialParent;

    private Transform toFollow;
    private Vector3 offset;

    void Start()
    {
        toFollow = artificialParent.transform;
        offset = toFollow.position - transform.position;
    }
    void Update()
    {
        transform.position = toFollow.position - offset;
        transform.rotation = toFollow.rotation;
    }
}
