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

        // 01 => 11
        public static void WB_to_WW_Loop_Once(int[] cells, int width, int height, int[] fromValue, int[] toValue) {
            bool isSucc = false;

            Span<int> directions = stackalloc int[4] { // stackalloc: 在栈上分配内存，不会产生GC
                -width, // down
                width,  // up
                -1,     // left
                1,      // right
            };

            for (int currentIndex = 0; currentIndex < cells.Length; currentIndex++) {
                int currentValue = cells[currentIndex];
                if (currentValue != fromValue[0]) {
                    continue;
                }

                int nextIndex;
                int nextValue;

                for (int i = 0; i < directions.Length; i++) {
                    nextIndex = currentIndex + directions[i];

                    if (nextIndex < 0 || nextIndex >= cells.Length) { // Up and Down
                        continue;
                    }
                    if (i == 2 || i == 3) { // Left and Right
                        if (nextIndex / width != currentIndex / width) {
                            continue;
                        }
                    }

                    nextValue = cells[nextIndex];
                    if (nextValue != fromValue[1]) {
                        continue;
                    }

                    cells[currentIndex] = toValue[0];
                    cells[nextIndex] = toValue[1];
                    isSucc = true;
                    break;
                }

                if (isSucc) {
                    break;
                }
            }
        }
    }
}
