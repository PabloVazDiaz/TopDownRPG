
using UnityEngine;

namespace RPG.Core
{
    public class CameraFacing : MonoBehaviour
    {
        Camera camera;

        private void Start()
        {
            camera = Camera.main;
        }


        // Update is called once per frame
        void LateUpdate()
        {
            transform.forward = camera.transform.forward;
        }
    }

}