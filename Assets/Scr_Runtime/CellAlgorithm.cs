using System;
using UnityEngine;
using RD = System.Random;

namespace Rewrite {
    public static class CellAlgorithm {
        public static void Fill(int[] cells, int value) {
            int len = cells.Length;
            for (int i = 0; i < len; i++) {
                cells[i] = value;
            }
        }

        public static void Replace_OneCell(RD random, int[] cells, int fromValue, int toValue) {
            int failedTimes = cells.Length;
            int index;

            do {
                index = random.Next(cells.Length);
                if (cells[index] == fromValue) {
                    cells[index] = toValue;
                    break;
                } else {
                    failedTimes--;
                }
            } while (failedTimes > 0);
        }
    }
}
