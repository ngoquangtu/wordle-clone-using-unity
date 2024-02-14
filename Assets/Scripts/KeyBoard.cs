using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyBoard : MonoBehaviour
{
    private static readonly KeyCode[] SUPPORTED_KEYS = new KeyCode[] {
        KeyCode.Q, KeyCode.W, KeyCode.E, KeyCode.R, KeyCode.T, KeyCode.Y,
        KeyCode.U, KeyCode.I, KeyCode.O, KeyCode.P, KeyCode.A, KeyCode.S,
        KeyCode.D, KeyCode.F, KeyCode.G, KeyCode.H, KeyCode.J, KeyCode.K,
        KeyCode.L, KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.V, KeyCode.B,
        KeyCode.N, KeyCode.M,
    };
    [SerializeField] private int k;

    public void keyCode()
    {
        if (Board.Instance.columnIndex < Board.Instance.currentRow.tiles.Length)
        {
            Board.Instance.currentRow.tiles[Board.Instance.columnIndex].SetLetter((char)SUPPORTED_KEYS[k]);
            Board.Instance.currentRow.tiles[Board.Instance.columnIndex].SetState(Board.Instance.occupiedState);
            Board.Instance.columnIndex++;
        }
    }
    public void enterCode()
    {
        if (Board.Instance.columnIndex >= Board.Instance.currentRow.tiles.Length)
        {
            Board.Instance.SubmitRow(Board.Instance.currentRow);
        }
    }
    public void deleteButton()
    {
        Board.Instance.columnIndex = Mathf.Max(Board.Instance.columnIndex - 1, 0);

        Board.Instance.currentRow.tiles[Board.Instance.columnIndex].SetLetter('\0');
        Board.Instance.currentRow.tiles[Board.Instance.columnIndex].SetState(Board.Instance.emptyState);

        Board.Instance.invalidWordText.SetActive(false);
    }
}
