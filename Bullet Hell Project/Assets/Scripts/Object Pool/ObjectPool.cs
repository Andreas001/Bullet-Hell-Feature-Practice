using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [Header("Object Prefab")]
    [SerializeField] private GameObject prefab;

    [Header("Objects in Pool")]
    [SerializeField] List<GameObject> objects;

    void Awake() {
        objects = new List<GameObject>();

        foreach (Transform child in transform) {
            objects.Add(child.gameObject);
        }
    }

    public GameObject GetObject() {
        if (objects.Count > 0) {
            foreach (GameObject bullet in objects) {
                if (!bullet.activeInHierarchy) {
                    return bullet;
                }
            }
        }

        //If code reaches here it means there are not enough bullets so more will be instantiated
        prefab.SetActive(false);
        GameObject newObject = Instantiate(prefab);
        newObject.SetActive(false);
        newObject.transform.parent = gameObject.transform;
        objects.Add(newObject);
        return newObject;
    }
}
