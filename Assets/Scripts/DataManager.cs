using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    private string _filePath;
    private char _slashSymbol = '/';
    private string _saveFileName = "score";

    [SerializeField] private Player _player;
    [SerializeField] private PauseMenu _pauseMenu;

    [System.Serializable]
    public class SaveScoreParameters
    {
        public SaveScoreParameters(string name, int score)
        {
            Name = name;
            Score = score;
        }
        public string Name;
        public int Score;
    }

    private void OnEnable()
    {
        if(_player)
            _player.Died += SaveGame;

        if(_pauseMenu)
            _pauseMenu.OnGameEnded += SaveGame;

    }

    private void OnDisable()
    {
        if (_player)
            _pauseMenu.OnGameEnded -= SaveGame;

        if (_pauseMenu)
            _player.Died += SaveGame;
    }

    private void Awake()
    {
        _filePath = Application.persistentDataPath;
    }

    public void SaveGame()
    {
        string savePath = _filePath + _slashSymbol + _saveFileName;
        
        XDocument xDoc = null;
        
        bool isFileExists = IsSaveExists(_saveFileName);
        if (!isFileExists)
        {
            xDoc = new XDocument();
            XElement scoreElement = SaveScore(ref xDoc, isFileExists);
            xDoc.Add(scoreElement);
        }
        else
        {
            xDoc = XDocument.Load(savePath);
            if (xDoc == null)
                return;
            SaveScore(ref xDoc, isFileExists);
        }

        xDoc.Save(savePath);
    }

    private bool IsSaveExists(string saveName)
    {
        if (File.Exists(_filePath + _slashSymbol + saveName))
            return true;
        return false;
    }

    private XElement SaveScore(ref XDocument document, bool fileExists)
    {
        SaveScoreParameters formatToSave = new SaveScoreParameters(GameInstance.CurrentPlayerName, _player.Score);
        XElement saveGameElement = null;
        XElement scoreBoardElement = null;

        if (!fileExists)
        {
            saveGameElement = new XElement("SaveGame");
            scoreBoardElement = new XElement("ScoreBoardParameters");
        }
        if (fileExists)
        {
            saveGameElement = document.Element("SaveGame");
            scoreBoardElement = saveGameElement.Element("ScoreBoardParameters");
        }

        XElement playerScoreElement = new XElement("PlayerScore");
        XAttribute scoreFormatSaveAttribute = new XAttribute("PlayerScoreData", JsonUtility.ToJson(formatToSave));

        if (!fileExists)
        {
            playerScoreElement.Add(scoreFormatSaveAttribute);
            scoreBoardElement.Add(playerScoreElement);
            saveGameElement.Add(scoreBoardElement);
        }
        else if(fileExists && IsNewPlayerScoreSave(document)) 
        {

            playerScoreElement.Add(scoreFormatSaveAttribute);
            scoreBoardElement.Add(playerScoreElement);
        }
        else if(fileExists && !IsNewPlayerScoreSave(document) && IsNeedChangeSaveScore(document, out XAttribute attributeToChange) && attributeToChange != null)
        {
            attributeToChange.Value = JsonUtility.ToJson(formatToSave);
        }

        return saveGameElement;
    }

    private bool IsNewPlayerScoreSave(XDocument document)
    {
        foreach(XElement playerScore in document.Element("SaveGame").Element("ScoreBoardParameters").Elements("PlayerScore"))
        {
            XAttribute playerScoreAttribute = playerScore.Attribute("PlayerScoreData");
            SaveScoreParameters formatToSave = JsonUtility.FromJson<SaveScoreParameters>(playerScoreAttribute.Value);
            if (formatToSave.Name == GameInstance.CurrentPlayerName)
                return false;
        }
        return true;
    }

    private bool IsNeedChangeSaveScore(XDocument document, out XAttribute attributeToChange)
    {
        foreach (XElement playerScore in document.Element("SaveGame").Element("ScoreBoardParameters").Elements("PlayerScore"))
        {
            XAttribute playerScoreAttribute = playerScore.Attribute("PlayerScoreData");
            SaveScoreParameters formatToSave = JsonUtility.FromJson<SaveScoreParameters>(playerScoreAttribute.Value);

            if (formatToSave.Name == GameInstance.CurrentPlayerName && formatToSave.Score < _player.Score)
            {
                attributeToChange = playerScoreAttribute;
                return true;
            }
        }

        attributeToChange = null;
        return false;
    }
    public void LoadScore(out List<SaveScoreParameters> scoreParameters)
    {
        scoreParameters = new List<SaveScoreParameters>();

        string savePath = _filePath + _slashSymbol + _saveFileName;
        if (!IsSaveExists(_saveFileName))
            return;

        XDocument xDoc = XDocument.Load(savePath);
        if (xDoc == null)
            return;

        XElement saveGameElement = xDoc.Element("SaveGame");
        XElement scoreBoardElement = saveGameElement.Element("ScoreBoardParameters");
        foreach (XElement playerScore in scoreBoardElement.Elements("PlayerScore"))
        {
            XAttribute playerScoreAttribute = playerScore.Attribute("PlayerScoreData");
            SaveScoreParameters formatToSave = JsonUtility.FromJson<SaveScoreParameters>(playerScoreAttribute.Value);

            scoreParameters.Add(formatToSave);
        }


    }
}
