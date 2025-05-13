using System;
using UnityEngine;

namespace Rewrite {
    public enum Direction {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
    }

    public static class CellFunctions {
        public static int GetDir(Direction dir) {
            return (int)dir;
        }
    }
}