using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
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
        if (Board.Instance.columnIndex < Board.Instance.currentRow.tiles.Length && Board.Instance.enabled)
        {
            Board.Instance.currentRow.tiles[Board.Instance.columnIndex].SetLetter((char)SUPPORTED_KEYS[k]);
            Board.Instance.currentRow.tiles[Board.Instance.columnIndex].SetState(Board.Instance.occupiedState);
            Board.Instance.columnIndex++;
        }
    }
    public void enterCode()
    {
        if (Board.Instance.columnIndex >= Board.Instance.currentRow.tiles.Length && !Board.Instance.HasWon(Board.Instance.currentRow) && Board.Instance.enabled)
        {
            Board.Instance.SubmitRow(Board.Instance.currentRow);
            SoundManager.instance.playSound(SoundManager.instance.enterSound);
        }
        string remaining = Board.Instance.word;
        for (int i = 0; i < Board.Instance.currentRow.tiles.Length; i++)
        {
            Tile tile = Board.Instance.currentRow.tiles[i];
            

            if (tile.letter == Board.Instance.word[i])
            {
                tile.SetState(Board.Instance.correctState);


                remaining = remaining.Remove(i, 1);
                remaining = remaining.Insert(i, " ");
            }
            else if (!Board.Instance.word.Contains(tile.letter))
            {
                tile.SetState(Board.Instance.incorrectState);
            }
        }
        for (int i = 0; i < Board.Instance.currentRow.tiles.Length; i++)
        {
            Tile tile = Board.Instance.currentRow.tiles[i];

            if (tile.state != Board.Instance.correctState && tile.state != Board.Instance.incorrectState)
            {
                if (remaining.Contains(tile.letter))
                {
                    tile.SetState(Board.Instance.wrongSpotState);

                    int index = remaining.IndexOf(tile.letter);
                    remaining = remaining.Remove(index, 1);
                    remaining = remaining.Insert(index, " ");
                }
                else
                {
                    tile.SetState(Board.Instance.incorrectState);
                }
            }
        }
    }
    public void deleteButton()
    {
        if (!Board.Instance.HasWon(Board.Instance.currentRow) && Board.Instance.enabled)
        {
            Board.Instance.columnIndex = Mathf.Max(Board.Instance.columnIndex - 1, 0);

            Board.Instance.currentRow.tiles[Board.Instance.columnIndex].SetLetter('\0');
            Board.Instance.currentRow.tiles[Board.Instance.columnIndex].SetState(Board.Instance.emptyState);

            Board.Instance.invalidWordText.SetActive(false);
        }

    }
}
