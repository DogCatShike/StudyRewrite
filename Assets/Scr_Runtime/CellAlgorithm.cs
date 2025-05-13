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

        #region 规则替换
        // 01 => 11
        public static void WB_to_WW_Loop_Once(int[] cells, int width, int height, int[] fromValue, int[] toValue) {
            bool isSucc = false;

            Span<int> directions = stackalloc int[4] { // stackalloc: 在栈上分配内存，不会产生GC
                width,  // up
                1,      // right
                -width, // down
                -1,     // left
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
                    if (i == 1 || i == 3) { // Left and Right
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

        public static void WB_to_WW_Loop_ToEnd(int[] cells, int width, int height, int[] fromValue, int[] toValue, int count) {
            for (int i = 0; i < count; i++) {
                WB_to_WW_Loop_Once(cells, width, height, fromValue, toValue);
            }
        }
        #endregion

        #region 直线
        public static void Line_Loop_Once(int[] cells, int width, int height, ref int fromIndex, int dir, int toValue) {
            Span<int> directions = stackalloc int[4] {
                width,  // up
                1,      // right
                -width, // down
                -1,     // left
            };

            int nextIndex = fromIndex + directions[dir];

            if (nextIndex < 0 || nextIndex >= cells.Length) { // Up and Down
                return;
            }
            if (dir == 1 || dir == 3) { // Left and Right
                if (nextIndex / width != fromIndex / width) {
                    return;
                }
            }

            cells[nextIndex] = toValue;
            fromIndex = nextIndex;
        }

        public static void Line_Loop_ToEnd(int[] cells, int width, int height, int fromIndex, int dir, int toValue, int count) {
            for (int i = 0; i < count; i++) {
                Line_Loop_Once(cells, width, height, ref fromIndex, dir, toValue);
            }
        }
        #endregion
    }
}
