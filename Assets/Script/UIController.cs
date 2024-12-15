using TMPro;
using UnityEngine;

/// <summary>
/// UIの切り替えを行います
/// </summary>
public class UIController : MonoBehaviour
{
    [SerializeField] private GameObject _overlay, _textWindow;
    [SerializeField] private TMP_Text _narText, _lineText;

    /// <summary>
    /// セリフ用のテキストウィンドウを表示
    /// </summary>
    public void ShowTextWindow(string text)
    {
        _overlay.SetActive(false);
        _textWindow.SetActive(true);
        _lineText.text = text;
    }

    /// <summary>
    /// 地の文用のオーバーレイを表示
    /// </summary>
    public void ShowOverlay(string text)
    {
        _textWindow.SetActive(false);
        _overlay.SetActive(true);
        _narText.text = text;
    }
}
