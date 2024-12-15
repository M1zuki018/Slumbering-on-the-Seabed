using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 背景データを格納
/// </summary>
[CreateAssetMenu(fileName = "BackgroundData", menuName = "SeaBed/BackgroundData")]
public class BackgroundData : ScriptableObject
{
    [SerializeField] private List<Sprite> Backgrounds;

    /// <summary>
    /// 背景画像を返します
    /// </summary>
    public Sprite BackgroundChange(int id)
    {
        return Backgrounds[id];
    }
}
