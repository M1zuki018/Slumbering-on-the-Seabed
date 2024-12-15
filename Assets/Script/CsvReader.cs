using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 1行ずつテキストデータを読み込んでいく
/// </summary>
public class CsvReader : MonoBehaviour
{
    public struct DialogueEntry
    {
        public string Tag;
        public string Content;
    } //分割されたデータ
    
    private StreamReader fileReader;
    
    public List<DialogueEntry> Entries = new List<DialogueEntry>(); //読み込んだ全て
    private List<string> NarrativeLines = new List<string>(); //1行だけ表示する地の文

    /// <summary>
    /// CSVファイルを開く
    /// </summary>
    public void OpenCsv(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Debug.LogError($"File not found at: {filePath}");
            return;
        }

        fileReader = new StreamReader(filePath);
    }
    
    /// <summary>
    /// CSVファイルを閉じる
    /// </summary>
    public void CloseCsv()
    {
        if (fileReader != null)
        {
            fileReader.Close();
            fileReader = null;
        }
    }
    
    /// <summary>
    /// 次のエントリーを読みます
    /// </summary>
    public DialogueEntry? ReadNextEntry()
    {
        if (fileReader == null)
        {
            Debug.LogError("fileReaderが開かれていません");
            return null;
        }

        string line;
        while ((line = fileReader.ReadLine()) != null)
        {
            string[] columns = line.Split(',');
            if (columns.Length < 2)
            {
                Debug.LogWarning($"無効な行です: {line}");
                continue;
            }

            //余計な空白などを削除
            string tag = columns[0].Trim();
            string content = columns[1].Trim();
            
            return new DialogueEntry { Tag = tag, Content = content };
        }

        return null; //最後の行
    }
    
    /// <summary>
    /// #nar と #endの間の地の文を読み込みます
    /// </summary>
    public List<string> ReadNarrativeBlock()
    {
        if (fileReader == null)
        {
            Debug.LogError("fileReaderが開かれていません");
            return null;
        }

        List<string> narrativeLines = new List<string>();
        string line;
        bool isNarrative = false;

        while ((line = fileReader.ReadLine()) != null)
        {
            string[] columns = line.Split(',');
            if (columns.Length < 2)
            {
                Debug.LogWarning($"無効な行です: {line}");
                continue;
            }

            //余計な空白などを削除
            string tag = columns[0].Trim();
            string content = columns[1].Trim();

            //タグの判定
            switch (tag)
            {
                case "#nar":
                    isNarrative = true;
                    break;

                case "#end":
                    if (isNarrative)
                    {
                        return narrativeLines; //Listを返す
                    }
                    break;

                default:
                    if (isNarrative)
                    {
                        narrativeLines.Add(content);
                    }
                    break;
            }
        }
        
        if (isNarrative)
        {
            Debug.LogWarning("fillReaderを閉じずにファイルの終わりに到達しました");
        }

        return narrativeLines;
    }
}
