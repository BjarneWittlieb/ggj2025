using System;

namespace Models
{
    [Serializable]
    public class Vector2IntData
    {
        public int x;
        public int y;

        public Vector2IntData(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
}