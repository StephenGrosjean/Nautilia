using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleContainer : MonoBehaviour
{
    private string parent;
    private ParticleSystem[] system;
    private string firstParent;

    private bool called;

    void Start()
    {
        system = GetComponentsInChildren<ParticleSystem>();
        firstParent = transform.parent.name;
    }

    void Update()
    {
        if (parent != "None") {
            if (transform.parent) {
                parent = transform.parent.name;
            }
            else {
                parent = "None";
            }
        }

        if (parent != firstParent && !called) {
            called = true;
            Destroy(gameObject, 5);
            foreach(ParticleSystem syst in system) {
                syst.Stop();
            }
        }
    }
}
