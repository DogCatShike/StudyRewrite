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

        int fromIndex;
        public Direction lineDir;

        int riverCheck;

        Dictionary<int, Color> colors = new Dictionary<int, Color>() {
            { 0, Color.black },
            { 1, Color.white },
            { 2, Color.red },
            { 3, Color.yellow },
            { 4, Color.blue },
            { 5, Color.green },
        };

        void Start() {
            rd = new RD(seed);
            cells = new int[width * height];
            CellAlgorithm.Fill(cells, 0);

            // StudyStart();
            RiverStart();
            CellAlgorithm.River_Loop_ToEnd(cells, width, height, riverCheck, 200);
        }

        void Update() {
            // StudyUpdate();
            RiverUpdate();
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

        #region 基础学习
        void StudyStart() {
            CellAlgorithm.Replace_OneCell(rd, cells, 0, 1);

            // CellAlgorithm.WB_to_WW_Loop_ToEnd(cells, width, height, new int[] { 1, 0 }, new int[] { 1, 1 }, 10);

            fromIndex = rd.Next(0, cells.Length);
            // int dir = CellFunctions.GetDir(lineDir);
            // CellAlgorithm.Line_Loop_ToEnd(cells, width, height, fromIndex, dir, 2, 10);
        }

        void StudyUpdate() {
            if (Input.GetKeyDown(KeyCode.R)) {
                CellAlgorithm.Fill(cells, 0);
                CellAlgorithm.Replace_OneCell(rd, cells, 0, 1);
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                CellAlgorithm.WB_to_WW_Loop_Once(cells, width, height, new int[] { 1, 0 }, new int[] { 1, 1 });
            }

            if (Input.GetKeyDown(KeyCode.L)) {
                int dir = CellFunctions.GetDir(lineDir);
                CellAlgorithm.Line_Loop_Once(cells, width, height, ref fromIndex, dir, 2);
            }
        }
        #endregion

        #region River
        void RiverStart() {
            CellAlgorithm.Replace_TwoCell(rd, cells, 0, 2, 3);
        }

        void RiverUpdate() {
            if (Input.GetKeyDown(KeyCode.R)) {
                CellAlgorithm.Fill(cells, 0);
                CellAlgorithm.Replace_TwoCell(rd, cells, 0, 2, 3);
            }

            if (Input.GetKeyDown(KeyCode.Space)) {
                CellAlgorithm.River_Loop_Once(cells, width, height, ref riverCheck);
            }
        }
        #endregion
    }
}