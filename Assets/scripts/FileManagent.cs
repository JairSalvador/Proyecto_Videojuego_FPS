using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class FileManagent : MonoBehaviour{
	public string nombreArchivo = "JuegoGuardado";
	public string nombreDirectorio = "Partidas";
	public GameData datosjuego;
	public static int record;
	public static string nombreR;
	
	
	
	private void Start(){
		LoadFile();
	}
	
	public void SaveFile(){
		if(!Directory.Exists(nombreDirectorio)) Directory.CreateDirectory(nombreDirectorio);
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream saveFile = File.Create(nombreDirectorio + "/" + nombreArchivo + ".bin");
		GameData datosjuego = new GameData(ManagerDisparo.puntosPlayer,5.1f, Navegacion.nombreJugador);
		formatter.Serialize(saveFile, datosjuego);
		saveFile.Close();
		Debug.Log("Guardado en " + Directory.GetCurrentDirectory().ToString() + "/Saves/" + nombreArchivo + ".bin");
	}
	
	public void LoadFile(){
		BinaryFormatter formatter = new BinaryFormatter();
		FileStream saveFile = File.Open(nombreDirectorio + "/" + nombreArchivo + ".bin", FileMode.Open);
		GameData loadData = (GameData)formatter.Deserialize(saveFile);
		Debug.Log("Datos cargados *********");
		Debug.Log("Nombre " + loadData.nombre);
		Debug.Log("Puntos Record " + loadData.puntos);
		Debug.Log("Tiempo " + loadData.tiempo);
		record = loadData.puntos;
		nombreR = loadData.nombre;
	}
}
