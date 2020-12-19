using UnityEngine;

namespace Title.Game
{
    public class Tile : MonoBehaviour, ITile
    {
        public Vector3 Position => transform.position;
    }
}
