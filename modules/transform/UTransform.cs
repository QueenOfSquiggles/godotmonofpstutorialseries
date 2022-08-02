using Godot;

namespace transform
{
    public class UTransform
    {
        public Vector3 Forward {
            get {
                return -transform.basis.z;
            }
        }

        public Vector3 Right {
            get{
                return transform.basis.x;
            }
        }

        public Vector3 Up {
            get {
                return transform.basis.y;
            }
        }

        private Transform transform;

        public UTransform(Transform n_transform)
        {
            this.transform = n_transform;
        }
    }
}