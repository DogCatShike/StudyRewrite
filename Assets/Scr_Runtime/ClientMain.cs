using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RD = System.Random; // 第一次用这种写法(避免和UnityEngine.Random混淆?)

namespace Rewrite {
    public class ClientMain : MonoBehaviour {
        public int width;
        public int height;
        public int seed; // 随机数种子

        int[] cells;
        RD rd;

        Dictionary<int, Color> colors = new Dictionary<int, Color>() {
            { 0, Color.black },
            { 1, Color.white },
            { 2, Color.blue },
            { 3, Color.green },
        };

        void Start() {
            rd = new RD(seed);
            cells = new int[width * height];

            CellAlgorithm.Fill(cells, 0);
            CellAlgorithm.Replace_OneCell(rd, cells, 0, 1);
        }

        void Update() {
            if (Input.GetKeyDown(KeyCode.Space)) {
                CellAlgorithm.WB_to_WW_Loop_Once(cells, width, height, new int[] { 1, 0 }, new int[] { 1, 1 });
            }
        }

        void OnDrawGizmos() {
            if (cells == null) return;

            for (int i = 0; i < cells.Length; i++) {
                int x = i % width;
                int y = i / width;
                int value = cells[i];

                // 取色
                colors.TryGetValue(value, out Color color);
                Gizmos.color = color;

                // 画
                Vector3 size = new Vector3(1, 1, 0);
                Gizmos.DrawCube(new Vector3(x, y, 0), size);
            }

        }
    }
}