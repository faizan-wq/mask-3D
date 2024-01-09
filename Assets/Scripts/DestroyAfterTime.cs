using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{
    public float Time = 0.4f;
    public Vector3 Offest;
    public GameObject ContainerSpawner;
    void Start()
    {
        if(ContainerSpawner == null)
        {
            PlayerPrefs.SetInt("Cash", PlayerPrefs.GetInt("Cash") + 20);
        }
        StartCoroutine(LoadingInactive());
    }
    private void Update()
    {
        transform.position = Offest;
    }
    IEnumerator LoadingInactive()
    {
        if(ContainerSpawner != null)
            (Instantiate(ContainerSpawner, transform.position, transform.rotation) as GameObject).transform.SetParent(transform.parent);
        yield return new WaitForSeconds(Time);
        Destroy(this.gameObject);
    }
}
