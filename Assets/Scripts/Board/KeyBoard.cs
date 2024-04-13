
using UnityEngine;
using UnityEngine.UI;


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
    private Button keyRenderer;

    private void Awake()
    {
        keyRenderer = GetComponent<Button>();
    }
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
        // Check if Board.Instance is not null and currentRow is assigned
        if (Board.Instance != null && Board.Instance.currentRow != null && Board.Instance.columnIndex >= Board.Instance.currentRow.tiles.Length && !Board.Instance.HasWon(Board.Instance.currentRow) && Board.Instance.enabled)
        {
            // Submit the row if conditions are met
            Board.Instance.SubmitRow(Board.Instance.currentRow);
            // Make sure SoundManager.instance is not null before accessing it
            if (SoundManager.instance != null)
            {
                SoundManager.instance.playSound(SoundManager.instance.enterSound);
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
    public char GetLetter()
    {
        return (char)SUPPORTED_KEYS[k];
    }
    public void SetColor(Color color)
    {
        ColorBlock cb = keyRenderer.colors;
        cb.normalColor = color;
        cb.highlightedColor = color;
        cb.pressedColor = color;
        cb.disabledColor = color;
        cb.selectedColor = color;
        keyRenderer.colors = cb;
    }
}
