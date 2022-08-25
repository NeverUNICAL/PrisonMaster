using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ForCreo
{
    public class CellsConteiner : MonoBehaviour
    {
        private Cell[] _cells;

        public Cell[] Cells => _cells;

        private void Awake()
        {
            _cells = new Cell[transform.childCount];
            for (int i = 0; i < _cells.Length; i++)
                _cells[i] = transform.GetChild(i).GetComponent<Cell>();
        }
    }
}