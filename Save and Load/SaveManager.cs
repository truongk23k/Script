using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
	public static SaveManager instance;

	[SerializeField] string fileName;
	[SerializeField] bool encryptData;
	GameData gameData;
	List<ISaveManager> saveManagers;
	FileDataHandler dataHandler;

	[ContextMenu("Delete saved file")]
	private void DeleteSavedData()
	{
		dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
		dataHandler.Delete();
	}

	private void Awake()
	{
		if (instance != null)
			Destroy(instance.gameObject);
		else
			instance = this;
	}

	private void Start()
	{
		dataHandler = new FileDataHandler(Application.persistentDataPath, fileName, encryptData);
		saveManagers = FindAllSaveManages();
		LoadGame();

	}

	public void NewGame()
	{
		gameData = new GameData();	
	}

	public void LoadGame()
	{
		//gameData = data from data handler
		gameData = dataHandler.Load();

		if(this.gameData == null)
		{
			Debug.Log("No saved data found!");
			NewGame();
		}

		foreach (ISaveManager saveManager in saveManagers)
		{
			saveManager.LoadData(gameData);
		}

	}

	public void SaveGame()
	{
		foreach (ISaveManager saveManager in saveManagers)
		{
			saveManager.SaveData(ref gameData);
		}

		//data handler save gameData
		dataHandler.Save(gameData);

	}

	private void OnApplicationQuit()
	{
		SaveGame();
	}

	private List<ISaveManager> FindAllSaveManages()
	{
		IEnumerable<ISaveManager> saveManagers = Resources.FindObjectsOfTypeAll<MonoBehaviour>().OfType<ISaveManager>();
		return new List<ISaveManager>(saveManagers);
	}

 	public bool HasSavedData()
	{
		if(dataHandler.Load() != null)
			return true;

		return false;
	}
}
