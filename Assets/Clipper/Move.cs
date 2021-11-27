using UnityEngine;

namespace theZinnZ.NClipper
{
    public class Move : MonoBehaviour
    {
        public float smoothing;

        private Camera _main;
        private void Awake()
        {
            _main = Camera.main;
        }

        public Vector3 GetWorldPosition()
        {
            var input = Input.mousePosition;
            input.z = -_main.transform.position.z;

            return _main.ScreenToWorldPoint(input);
        }

        private void Update()
        {
            transform.position = Vector3.Lerp(transform.position, GetWorldPosition(), smoothing * Time.deltaTime);
        }
    }

}
