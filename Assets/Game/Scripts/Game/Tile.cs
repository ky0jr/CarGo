using UnityEngine;

namespace CarGo.Game
{
    public class Tile : MonoBehaviour, ITile
    {
        public Vector3 Position => transform.position;
    }
}
