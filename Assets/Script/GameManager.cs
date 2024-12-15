using UnityEngine;

/// <summary>
/// マネージャークラス
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] BackgroundData _backgroundData;
    private CsvReader _csvReader;
    private UIController _uiController;

    private void Awake()
    {
        _csvReader = GetComponent<CsvReader>();
        _uiController = GetComponent<UIController>();
        
        _csvReader.OpenCsv(Application.dataPath + "/Resource/CSV/text.csv");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            CsvReader.DialogueEntry? entry = _csvReader.ReadNextEntry();
            if (entry.Value.Tag == "#line")
            {
                _uiController.ShowTextWindow(entry.Value.Content);
            }
            else if (entry.Value.Tag == "#nar")
            {
                _uiController.ShowOverlay(entry.Value.Content);
            }
            
            if (entry.HasValue)
            {
                Debug.Log($"Tag: {entry.Value.Tag}, Content: {entry.Value.Content}");
            }

        }
    }
}
