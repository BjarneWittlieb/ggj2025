using UnityEngine;

namespace Models
{
    public interface ISelectionOverlay
    {
        void Render(Vector2Int gridPosition);

        void Destroy();
    }
}