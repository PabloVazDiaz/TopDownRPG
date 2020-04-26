using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {

        ParticleSystem ps;
        // Start is called before the first frame update
        void Start()
        {
            ps = GetComponent<ParticleSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            if (!ps.IsAlive())
                Destroy(gameObject);
        }
    }

}