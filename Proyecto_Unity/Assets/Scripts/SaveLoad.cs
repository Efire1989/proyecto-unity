using UnityEngine;

using System.Text;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using System;
using System.Runtime.Serialization;
using System.Reflection;

using System.Collections;
using System.Collections.Generic;

//Clase contenedora del savegame ------------------------------------------------------------------------------------------------------

[System.Serializable]
public class SaveData {

	//VARIABLES A SALVAR
	//Textura heightmap
	public int heightmapW;
	public int heightmapH;
	public float[] heightmapData;
	//Textura elementos
	public int elementosW;
	public int elementosH;
	public float[] elementosData;
	//Textura plantas
	public int plantasW;
	public int plantasH;
	public float[] plantasData;
	//Textura habitats
	public int habitatsW;
	public int habitatsH;
	public float[] habitatsData;
	//Textura habitats estetica
	public int esteticaW;
	public int esteticaH;
	public float[] esteticaData;
	//Vida
	public VidaSerializable vidaData;
	//Mesh roca
	public float[] rocaVertices;
	public float[] rocaNormals;
	public float[] rocaUVs;
	public int[] rocaTriangulos;
	//Mesh agua
	public float[] aguaVertices;
	public float[] aguaNormals;
	public float[] aguaUVs;
	public int[] aguaTriangulos;
	//Variables de control del planeta
	public float nivelAgua;
	public float tamanoPlaya;


  	public SaveData () {}

}

//Clase contenedora de la información de la clase Vida ----------------------------------------------------------------------------------
[System.Serializable]
public class VidaSerializable {

//	public Texture2D texturaPlantas;
//	private Transform objetoRoca;
	public CasillaSerializable[,] tablero;
	public List<EspecieSerializable> especies;
	public List<EspecieVegetalSerializable> especiesVegetales;
	public List<EspecieAnimalSerializable> especiesAnimales;
	public List<TipoEdificioSerializable> tiposEdificios;
	public List<SerSerializable> seres;
	public List<VegetalSerializable> vegetales;
	public List<AnimalSerializable> animales;
	public List<EdificioSerializable> edificios;
	
	public int numEspecies;
	public int numEspeciesVegetales;
	public int numEspeciesAnimales;
	public int numTiposEdificios;
	
	public int idActualVegetal;
	public int idActualAnimal;
	public int idActualEdificio;
	
	public int contadorPintarTexturaPlantas;
	public bool texturaPlantasModificado;
	
	public List<Tupla<int,int>> posicionesColindantes;
	public int[,] matrizRadio3Circular;
	public int[,] matrizRadio4Circular;
	public int[,] matrizRadio5Circular;
	public int[,] matrizRadio6Circular;
	
	public VidaSerializable() {}
}

//Clase contenedora de la información de la clase Casilla ----------------------------------------------------------------------------------
[System.Serializable]
public class CasillaSerializable {

	public T_habitats habitat;
	public T_elementos elementos;
	public float coordsTexX;
	public float coordsTexY;
	public float coordsVertX;
	public float coordsVertY;
	public float coordsVertZ;
	public VegetalSerializable vegetal;
	public AnimalSerializable animal;
	public EdificioSerializable edificio;
	public float[] pinceladas;
	
	public CasillaSerializable() {}
}

//Clase contenedora de la información de la clase Especie ----------------------------------------------------------------------------------
[System.Serializable]
public class EspecieSerializable {

	public int idEspecie;
	public string nombre;
	public int numMaxSeresEspecie;
	public int numSeresEspecie;	
	public int[] modelos;
	
	public EspecieSerializable() {}
}

//Clase contenedora de la información de la clase Ser ----------------------------------------------------------------------------------
[System.Serializable]
public class SerSerializable {

	public int idSer;
	public int posX;
	public int posY;
	
	public SerSerializable() {}
}

//Clase contenedora de la información de la clase Vegetal ----------------------------------------------------------------------------------
[System.Serializable]
public class VegetalSerializable {

	public EspecieVegetalSerializable especie;
	public int numVegetales;
	public List<float> habitabilidad;
	public int indiceHabitat;
	public int turnosEvolucion;	
	public int idSer;
	public int posX;
	public int posY;
	public int modelo;				//El entero que corresponde al indice del modelo
	
	public VegetalSerializable() {}
}

//Clase contenedora de la información de la clase Animal ----------------------------------------------------------------------------------
[System.Serializable]
public class AnimalSerializable {

	public EspecieAnimalSerializable especie;
	public int reserva;
	public int turnosParaReproduccion;
	public int aguante;	
	public tipoEstadoAnimal estado;	
	public int idSer;
	public int posX;
	public int posY;
	public int modelo;				//El entero que corresponde al indice del modelo
	
	public AnimalSerializable() {}
}

//Clase contenedora de la información de la clase Edificio ----------------------------------------------------------------------------------
[System.Serializable]
public class EdificioSerializable {

	public TipoEdificioSerializable tipo;
	public int energiaConsumidaPorTurno;
	public int compBasConsumidosPorTurno;
	public int compAvzConsumidosPorTurno;
	public int matBioConsumidoPorTurno;
	public int energiaProducidaPorTurno;
	public int compBasProducidosPorTurno;
	public int compAvzProducidosPorTurno;
	public int matBioProducidoPorTurno;
	public float eficiencia;	
	public int numMetales;
	public List<Tupla<int,int,bool>> matrizRadioAccion;
	public int matBioSinProcesar;
	public int radioAccion;
	public int idSer;
	public int posX;
	public int posY;
	public int modelo;
	
	public EdificioSerializable() {}
}

//Clase contenedora de la información de la clase EspecieVegetal ----------------------------------------------------------------------------------
[System.Serializable]
public class EspecieVegetalSerializable {
	public int numMaxVegetales;
	public int numIniVegetales;
	public float capacidadMigracionLocal;
	public float capacidadMigracionGlobal;
	public int radioMigracion;
	public int turnosEvolucionInicial;
	public float evolucion;
	public List<float> habitabilidadInicial;
	public int idTextura;
	public int numMaxModelos;
	
	
	public int idEspecie;
	public string nombre;
	public int numMaxSeresEspecie;
	public int numSeresEspecie;	
	public int[] modelos;
	
	public EspecieVegetalSerializable() {}
}

//Clase contenedora de la información de la clase EspecieAnimal ----------------------------------------------------------------------------------
[System.Serializable]
public class EspecieAnimalSerializable {
	public int consumo;
	public int reservaMaxima;
	public int alimentoMaxTurno;
	public int aguanteInicial;
	public int reproductibilidad;
	public tipoAlimentacionAnimal tipo;
	public List<T_habitats> habitats;
	
	public int idEspecie;
	public string nombre;
	public int numMaxSeresEspecie;
	public int numSeresEspecie;	
	public int[] modelos;
	
	public EspecieAnimalSerializable() {}
}

//Clase contenedora de la información de la clase TipoEdificio ----------------------------------------------------------------------------------
[System.Serializable]
public class TipoEdificioSerializable {
	public int idTipoEdificio;
	public string nombre;	
	public List<T_habitats> habitats;
	public int energiaConsumidaAlCrear;
	public int compBasConsumidosAlCrear;
	public int compAvzConsumidosAlCrear;
	public int matBioConsumidoAlCrear;
	public T_elementos elemNecesarioAlConstruir;
	public int energiaConsumidaPorTurnoMax;
	public int compBasConsumidosPorTurnoMax;
	public int compAvzConsumidosPorTurnoMax;
	public int matBioConsumidoPorTurnoMax;
	public int energiaProducidaPorTurnoMax;
	public int compBasProducidosPorTurnoMax;
	public int compAvzProducidosPorTurnoMax;
	public int matBioProducidoPorTurnoMax;
	public int[] modelos;
	
	public TipoEdificioSerializable() {}
}

//Clase contenedora del archivo de indices ------------------------------------------------------------------------------------------------------
[System.Serializable]
public class SaveIndices {

	//Variables a salvar
	public int[] indices;

  	public SaveIndices() {}

}

//Clase con los métodos a llamar para crear el savegame y otros archivos ---------------------------------------------------------------
public class SaveLoad {
	
	public static ModelosAnimales modelosAnimales;
	public static ModelosEdificios modelosEdificios;
	public static ModelosVegetales modelosVegetales;
	public static string currentFileName = "SaveGame.hur";						
	public static string currentFilePath = Application.persistentDataPath + "/Saves/";
	
	//Guardar el archivo con el avance en el juego
	public static void Save (ValoresCarga contenedor) {					//Sobrecargado
		Save(currentFilePath + currentFileName, contenedor);
	}
	
  	public static void Save (string filePath, ValoresCarga contenedor)
  	{ 
	    SaveData save = generarValores(contenedor);
	    FileStream stream = new FileStream(filePath, FileMode.Create);
		try {
		    BinaryFormatter bformatter = new BinaryFormatter();
		    bformatter.Serialize(stream, save);
		}
		catch (SerializationException e) {
			Debug.LogError("Excepcion al serializar el savegame. Datos: " + e.Message);
		}
		finally {
	    	stream.Close();
		}
  	}
	
	//Cargar el archivo con el avance del juego
  	public static SaveData Load ()  {									//Sobrecargado
		return Load(currentFilePath + currentFileName);
	} 
	
  	public static SaveData Load(string filePath) 
  	{
	    SaveData data = new SaveData ();
	    FileStream stream = new FileStream(filePath, FileMode.Open);
		try {
		    BinaryFormatter bformatter = new BinaryFormatter();
		    data = (SaveData)bformatter.Deserialize(stream);
		}
		catch (SerializationException e) {
			Debug.LogError("Excepcion al deserializar el savegame. Datos: " + e.Message);
		}
		finally {
			stream.Close();
		}
		return data;
	}
	
	/* Genera un archivo SaveData (serializable) a partir de un conjunto de datos
	 * representativo del estado del juego pero no serializable.
	 */
	private static SaveData generarValores(ValoresCarga contenedor) {
		SaveData resultado = new SaveData();
		//Textura Heightmap (Textura_Planeta)
		resultado.heightmapW = contenedor.texturaBase.width;
		resultado.heightmapH = contenedor.texturaBase.height;
		Color[] temp1 = contenedor.texturaBase.GetPixels();
		resultado.heightmapData = new float[temp1.Length];
		for (int i = 0; i < temp1.Length; i++) {
			resultado.heightmapData[i] = temp1[i].r;
		}
		//Textura elementos (Textura_Recursos)
		resultado.elementosW = contenedor.texturaElementos.width;
		resultado.elementosH = contenedor.texturaElementos.height;
		Color[] temp2 = contenedor.texturaElementos.GetPixels();
		resultado.elementosData = new float[temp2.Length * 4];
		for (int i = 0; i < temp2.Length; i++) {
			resultado.elementosData[i * 4] = temp2[i].r;
			resultado.elementosData[i * 4 + 1] = temp2[i].g;
			resultado.elementosData[i * 4 + 2] = temp2[i].b;
			resultado.elementosData[i * 4 + 3] = temp2[i].a;
		}
		//Textura plantas (Textura_planta)
		resultado.plantasW = contenedor.texturaPlantas.width;
		resultado.plantasH = contenedor.texturaPlantas.height;
		Color[] temp3 = contenedor.texturaPlantas.GetPixels();
		resultado.plantasData = new float[temp3.Length * 4];
		for (int i = 0; i < temp3.Length; i++) {
			resultado.plantasData[i * 4] = temp3[i].r;
			resultado.plantasData[i * 4 + 1] = temp3[i].g;
			resultado.plantasData[i * 4 + 2] = temp3[i].b;
			resultado.plantasData[i * 4 + 3] = temp3[i].a;
		}
		//Textura habitats (Textura_Habitats)
		resultado.habitatsW = contenedor.texturaHabitats.width;
		resultado.habitatsH = contenedor.texturaHabitats.height;
		Color[] temp4 = contenedor.texturaHabitats.GetPixels();
		resultado.habitatsData = new float[temp4.Length * 4];
		for (int i = 0; i < temp4.Length; i++) {
			resultado.habitatsData[i * 4] = temp4[i].r;
			resultado.habitatsData[i * 4 + 1] = temp4[i].g;
			resultado.habitatsData[i * 4 + 2] = temp4[i].b;
			resultado.habitatsData[i * 4 + 3] = temp4[i].a;
		}
		//Textura habitats estetica (Textura_Habitats_Estetica)
		resultado.esteticaW = contenedor.texturaHabsEstetica.width;
		resultado.esteticaH = contenedor.texturaHabsEstetica.height;
		Color[] temp5 = contenedor.texturaHabsEstetica.GetPixels();
		resultado.esteticaData = new float[temp5.Length * 4];
		for (int i = 0; i < temp5.Length; i++) {
			resultado.esteticaData[i * 4] = temp5[i].r;
			resultado.esteticaData[i * 4 + 1] = temp5[i].g;
			resultado.esteticaData[i * 4 + 2] = temp5[i].b;
			resultado.esteticaData[i * 4 + 3] = temp5[i].a;
		}
		//Clase Vida
		generarVidaSerializable(contenedor.vida, ref resultado.vidaData);
		//Mesh Roca
		Vector3[] temp6 = contenedor.roca.vertices;
		Vector3[] temp7 = contenedor.roca.normals;
		resultado.rocaVertices = new float[temp6.Length * 3];
		resultado.rocaNormals = new float[temp7.Length * 3];
		for (int i = 0; i < temp6.Length; i++) {
			resultado.rocaVertices[i * 3] = temp6[i].x;
			resultado.rocaVertices[i * 3 + 1] = temp6[i].y;
			resultado.rocaVertices[i * 3 + 2] = temp6[i].z;
			resultado.rocaNormals[i * 3] = temp7[i].x;
			resultado.rocaNormals[i * 3 + 1] = temp7[i].y;
			resultado.rocaNormals[i * 3 + 2] = temp7[i].z;
		}
		Vector2[] temp8 = contenedor.roca.uv;
		resultado.rocaUVs = new float[temp8.Length * 2];
		for (int i = 0; i < temp8.Length; i++) {
			resultado.rocaUVs[i * 2] = temp8[i].x;
			resultado.rocaUVs[i * 2 + 1] = temp8[i].y;
		}
		int[] temp9 = contenedor.roca.triangles;
		resultado.rocaTriangulos = new int[temp9.Length];
		for (int i = 0; i < temp9.Length; i++) {
			resultado.rocaTriangulos[i] = temp9[i];
		}
		//Mesh Agua
		Vector3[] temp10 = contenedor.agua.vertices;
		Vector3[] temp11 = contenedor.agua.normals;
		resultado.aguaVertices = new float[temp10.Length * 3];
		resultado.aguaNormals = new float[temp11.Length * 3];
		for (int i = 0; i < temp10.Length; i++) {
			resultado.aguaVertices[i * 3] = temp10[i].x;
			resultado.aguaVertices[i * 3 + 1] = temp10[i].y;
			resultado.aguaVertices[i * 3 + 2] = temp10[i].z;
			resultado.aguaNormals[i * 3] = temp11[i].x;
			resultado.aguaNormals[i * 3 + 1] = temp11[i].y;
			resultado.aguaNormals[i * 3 + 2] = temp11[i].z;
		}
		Vector2[] temp12 = contenedor.agua.uv;
		resultado.aguaUVs = new float[temp12.Length * 2];
		for (int i = 0; i < temp12.Length; i++) {
			resultado.aguaUVs[i * 2] = temp12[i].x;
			resultado.aguaUVs[i * 2 + 1] = temp12[i].y;
		}
		int[] temp13 = contenedor.agua.triangles;
		resultado.aguaTriangulos = new int[temp13.Length];
		for (int i = 0; i < temp13.Length; i++) {
			resultado.aguaTriangulos[i] = temp13[i];
		}
		//Otras variables
		resultado.nivelAgua = contenedor.nivelAgua;
		resultado.tamanoPlaya = contenedor.tamanoPlaya;
		//Fin
		return resultado;
	}
	
	/*Construye un conjunto de datos representativos del estado del juego a partir de
	 * unos datos serializables.
	 */
	public static void rehacerScript(SaveData save, ref ValoresCarga contenedor) {
		//Textura Heightmap (Textura_Planeta)
		Texture2D temp1 = new Texture2D(save.heightmapW, save.heightmapH);
		Color[] temp1a = new Color[save.heightmapData.Length];
		for (int i = 0; i < save.heightmapData.Length; i++) {
			temp1a[i].r = save.heightmapData[i];
			temp1a[i].g = save.heightmapData[i];
			temp1a[i].b = save.heightmapData[i];
		}
		temp1.SetPixels(temp1a);
		temp1.Apply();
		contenedor.texturaBase = temp1;
		//Textura elementos (Textura_Recursos)
		Texture2D temp2 = new Texture2D(save.elementosW, save.elementosH);
		Color[] temp2a = new Color[save.elementosData.Length / 4];
		for (int i = 0; i < temp2a.Length; i++) {
			temp2a[i].r = save.elementosData[i * 4];
			temp2a[i].g = save.elementosData[i * 4 + 1];
			temp2a[i].b = save.elementosData[i * 4 + 2];
			temp2a[i].a = save.elementosData[i * 4 + 3];
		}
		temp2.SetPixels(temp2a);
		temp2.Apply();
		contenedor.texturaElementos = temp2;
		//Textura plantas (Textura_planta)
		Texture2D temp3 = new Texture2D(save.plantasW, save.plantasH);
		Color[] temp3a = new Color[save.plantasData.Length / 4];
		for (int i = 0; i < temp3a.Length; i++) {
			temp3a[i].r = save.plantasData[i * 4];
			temp3a[i].g = save.plantasData[i * 4 + 1];
			temp3a[i].b = save.plantasData[i * 4 + 2];
			temp3a[i].a = save.plantasData[i * 4 + 3];
		}
		temp3.SetPixels(temp3a);
		temp3.Apply();
		contenedor.texturaPlantas = temp3;
		//Textura habitats (Textura_Habitats)		
		Texture2D temp4 = new Texture2D(save.habitatsW, save.habitatsH);
		Color[] temp4a = new Color[save.habitatsData.Length / 4];
		for (int i = 0; i < temp4a.Length; i++) {
			temp4a[i].r = save.plantasData[i * 4];
			temp4a[i].g = save.plantasData[i * 4 + 1];
			temp4a[i].b = save.plantasData[i * 4 + 2];
			temp4a[i].a = save.plantasData[i * 4 + 3];
		}
		temp4.SetPixels(temp4a);
		temp4.Apply();
		contenedor.texturaHabitats = temp4;
		//Textura habitats estetica (Textura_Habitats_Estetica)		
		Texture2D temp5 = new Texture2D(save.esteticaW, save.esteticaH);
		Color[] temp5a = new Color[save.esteticaData.Length / 4];
		for (int i = 0; i < temp5a.Length; i++) {
			temp5a[i].r = save.esteticaData[i * 4];
			temp5a[i].g = save.esteticaData[i * 4 + 1];
			temp5a[i].b = save.esteticaData[i * 4 + 2];
			temp5a[i].a = save.esteticaData[i * 4 + 3];
		}
		temp5.SetPixels(temp5a);
		temp5.Apply();
		contenedor.texturaHabsEstetica = temp5;
		//Clase Vida
		rehacerVida(ref contenedor.vida, save.vidaData);
		contenedor.vida.texturaPlantas = temp3;
		//Mesh Roca
		Mesh temp6 = new Mesh();
		Vector3[] temp6v = new Vector3[save.rocaVertices.Length / 3];
		Vector3[] temp6n = new Vector3[save.rocaNormals.Length / 3];
		for (int i = 0; i < temp6v.Length; i++) {
			temp6v[i].x = save.rocaVertices[i * 3];
			temp6v[i].y = save.rocaVertices[i * 3 + 1];
			temp6v[i].z = save.rocaVertices[i * 3 + 2];
			temp6n[i].x = save.rocaNormals[i * 3];
			temp6n[i].y = save.rocaNormals[i * 3 + 1];
			temp6n[i].z = save.rocaNormals[i * 3 + 2];
		}
		temp6.vertices = temp6v;
		temp6.normals = temp6n;
		Vector2[] temp6u = new Vector2[save.rocaUVs.Length / 2];
		for (int i = 0; i < temp6u.Length; i++) {
			temp6u[i].x = save.rocaUVs[i * 2];
			temp6u[i].y = save.rocaUVs[i * 2 + 1];
		}
		temp6.uv = temp6u;
		int[] temp6t = new int[save.rocaTriangulos.Length];
		for (int i = 0; i < temp6t.Length; i++) {
			temp6t[i] = save.rocaTriangulos[i];
		}
		temp6.triangles = temp6t;
		contenedor.roca = temp6;
		//Mesh Agua
		Mesh temp7 = new Mesh();
		Vector3[] temp7v = new Vector3[save.aguaVertices.Length / 3];
		Vector3[] temp7n = new Vector3[save.aguaNormals.Length / 3];
		for (int i = 0; i < temp7v.Length; i++) {
			temp7v[i].x = save.aguaVertices[i * 3];
			temp7v[i].y = save.aguaVertices[i * 3 + 1];
			temp7v[i].z = save.aguaVertices[i * 3 + 2];
			temp7n[i].x = save.aguaNormals[i * 3];
			temp7n[i].y = save.aguaNormals[i * 3 + 1];
			temp7n[i].z = save.aguaNormals[i * 3 + 2];
		}
		temp7.vertices = temp7v;
		temp7.normals = temp7n;
		Vector2[] temp7u = new Vector2[save.aguaUVs.Length / 2];
		for (int i = 0; i < temp7u.Length; i++) {
			temp7u[i].x = save.aguaUVs[i * 2];
			temp7u[i].y = save.aguaUVs[i * 2 + 1];
		}
		temp7.uv = temp7u;
		int[] temp7t = new int[save.aguaTriangulos.Length];
		for (int i = 0; i < temp7t.Length; i++) {
			temp7t[i] = save.aguaTriangulos[i];
		}
		temp7.triangles = temp7t;
		contenedor.agua = temp7;
		//Otras variables
		contenedor.nivelAgua = save.nivelAgua;
		contenedor.tamanoPlaya = save.tamanoPlaya;
		//Fin
	}
	
	/* Construye un archivo con los datos de la clase Vida que es serializable 
	 * a partir de unos datos de Vida no serializables.
	 */
	private static void generarVidaSerializable(Vida vida, ref VidaSerializable vidaSerializable) {
		vidaSerializable = new VidaSerializable();
		vidaSerializable.animales = new List<AnimalSerializable>();
		for (int i = 0; i < vida.animales.Count; i++) {
			vidaSerializable.animales.Add(getAnimalSerializable(vida.animales[i]));
		}
		vidaSerializable.vegetales = new List<VegetalSerializable>();
		for (int i = 0; i < vida.vegetales.Count; i++) {
			vidaSerializable.vegetales.Add(getVegetalSerializable(vida.vegetales[i]));
		}
		vidaSerializable.edificios = new List<EdificioSerializable>();
		for (int i = 0; i < vida.edificios.Count; i++) {
			vidaSerializable.edificios.Add(getEdificioSerializable(vida.edificios[i]));
		}
		vidaSerializable.seres = new List<SerSerializable>();
		for (int i = 0; i < vida.seres.Count; i++) {
			vidaSerializable.seres.Add(getSerSerializable(vida.seres[i]));
		}
		vidaSerializable.especies = new List<EspecieSerializable>();
		for (int i = 0; i < vida.especies.Count; i++) {
			vidaSerializable.especies.Add(getEspecieSerializable(vida.especies[i]));
		}
		vidaSerializable.especiesAnimales = new List<EspecieAnimalSerializable>();
		for (int i = 0; i < vida.especiesAnimales.Count; i++) {
			vidaSerializable.especiesAnimales.Add(getEspecieAniSerializable(vida.especiesAnimales[i]));
		}
		vidaSerializable.especiesVegetales = new List<EspecieVegetalSerializable>();
		for (int i = 0; i < vida.especiesVegetales.Count; i++) {
			vidaSerializable.especiesVegetales.Add(getEspecieVegSerializable(vida.especiesVegetales[i]));
		}
		vidaSerializable.tiposEdificios = new List<TipoEdificioSerializable>();
		for (int i = 0; i < vida.tiposEdificios.Count; i++) {
			vidaSerializable.tiposEdificios.Add(getTipoEdifSerializable(vida.tiposEdificios[i]));
		}
		vidaSerializable.contadorPintarTexturaPlantas = vida.contadorPintarTexturaPlantas;
		vidaSerializable.idActualAnimal = vida.idActualAnimal;
		vidaSerializable.idActualEdificio = vida.idActualEdificio;
		vidaSerializable.idActualVegetal = vida.idActualVegetal;
		vidaSerializable.numEspecies = vida.numEspecies;
		vidaSerializable.numEspeciesAnimales = vida.numEspeciesAnimales;
		vidaSerializable.numEspeciesVegetales = vida.numEspeciesVegetales;
		vidaSerializable.numTiposEdificios = vida.numTiposEdificios;
		vidaSerializable.texturaPlantasModificado = vida.texturaPlantasModificado;
		
		vidaSerializable.posicionesColindantes = new List<Tupla<int,int>>();
		for (int i = 0; i < vida.posicionesColindantes.Count; i++) {
			Tupla<int,int> tupla = new Tupla<int, int>(vida.posicionesColindantes[i].e1, vida.posicionesColindantes[i].e2);
			vidaSerializable.posicionesColindantes.Add(tupla);
		}
		vidaSerializable.matrizRadio3Circular = new int[vida.matrizRadio3Circular.GetLength(0), vida.matrizRadio3Circular.GetLength(1)];
		for (int i = 0; i < vida.matrizRadio3Circular.GetLength(0); i++) {
			for (int j = 0; j < vida.matrizRadio3Circular.GetLength(1); j++) {
				vidaSerializable.matrizRadio3Circular[i,j] = vida.matrizRadio3Circular[i,j];
			}
		}
		vidaSerializable.matrizRadio4Circular = new int[vida.matrizRadio4Circular.GetLength(0), vida.matrizRadio4Circular.GetLength(1)];
		for (int i = 0; i < vida.matrizRadio4Circular.GetLength(0); i++) {
			for (int j = 0; j < vida.matrizRadio4Circular.GetLength(1); j++) {
				vidaSerializable.matrizRadio4Circular[i,j] = vida.matrizRadio4Circular[i,j];
			}
		}
		vidaSerializable.matrizRadio5Circular = new int[vida.matrizRadio5Circular.GetLength(0), vida.matrizRadio5Circular.GetLength(1)];
		for (int i = 0; i < vida.matrizRadio5Circular.GetLength(0); i++) {
			for (int j = 0; j < vida.matrizRadio5Circular.GetLength(1); j++) {
				vidaSerializable.matrizRadio5Circular[i,j] = vida.matrizRadio5Circular[i,j];
			}
		}
		vidaSerializable.matrizRadio6Circular = new int[vida.matrizRadio6Circular.GetLength(0), vida.matrizRadio6Circular.GetLength(1)];
		for (int i = 0; i < vida.matrizRadio6Circular.GetLength(0); i++) {
			for (int j = 0; j < vida.matrizRadio6Circular.GetLength(1); j++) {
				vidaSerializable.matrizRadio6Circular[i,j] = vida.matrizRadio6Circular[i,j];
			}
		}
		
		generarTableroSerializable(vida.tablero, ref vidaSerializable.tablero);

	}
	
	/* Construye un archivo con los datos del tablero Vida a partir de
	 * unos datos serializables.
	 */
	private static void rehacerVida(ref Vida vida, VidaSerializable vidaSerializable) {
		//Variables de Vida
		vida.animales = new List<Animal>();
		for (int i = 0; i < vidaSerializable.animales.Count; i++) {
			vida.animales.Add(getAnimalNoSerializable(vidaSerializable.animales[i]));
		}
		vida.vegetales = new List<Vegetal>();
		for (int i = 0; i < vidaSerializable.vegetales.Count; i++) {
			vida.vegetales.Add(getVegetalNoSerializable(vidaSerializable.vegetales[i]));
		}
		vida.edificios = new List<Edificio>();
		for (int i = 0; i < vidaSerializable.edificios.Count; i++) {
			vida.edificios.Add(getEdificioNoSerializable(vidaSerializable.edificios[i]));
		}
		vida.seres = new List<Ser>();
		for (int i = 0; i < vidaSerializable.seres.Count; i++) {
			vida.seres.Add(getSerNoSerializable(vidaSerializable.seres[i]));
		}
		vida.especies = new List<Especie>();
		for (int i = 0; i < vidaSerializable.especies.Count; i++) {
			vida.especies.Add(getEspecieNoSerializable(vidaSerializable.especies[i]));
		}
		vida.especiesAnimales = new List<EspecieAnimal>();
		for (int i = 0; i < vidaSerializable.especiesAnimales.Count; i++) {
			vida.especiesAnimales.Add(getEspecieAniNoSerializable(vidaSerializable.especiesAnimales[i]));
		}
		vida.especiesVegetales = new List<EspecieVegetal>();
		for (int i = 0; i < vidaSerializable.especiesVegetales.Count; i++) {
			vida.especiesVegetales.Add(getEspecieVegNoSerializable(vidaSerializable.especiesVegetales[i]));
		}
		vida.tiposEdificios = new List<TipoEdificio>();
		for (int i = 0; i < vidaSerializable.tiposEdificios.Count; i++) {
			vida.tiposEdificios.Add(getTipoEdifNoSerializable(vidaSerializable.tiposEdificios[i]));
		}
		vida.contadorPintarTexturaPlantas = vidaSerializable.contadorPintarTexturaPlantas;
		vida.idActualAnimal = vidaSerializable.idActualAnimal;
		vida.idActualEdificio = vidaSerializable.idActualEdificio;
		vida.idActualVegetal = vidaSerializable.idActualVegetal;
		vida.numEspecies = vidaSerializable.numEspecies;
		vida.numEspeciesAnimales = vidaSerializable.numEspeciesAnimales;
		vida.numEspeciesVegetales = vidaSerializable.numEspeciesVegetales;
		vida.numTiposEdificios = vidaSerializable.numTiposEdificios;
		vida.texturaPlantasModificado = vidaSerializable.texturaPlantasModificado;
		
		vida.posicionesColindantes = new List<Tupla<int,int>>();
		for (int i = 0; i < vidaSerializable.posicionesColindantes.Count; i++) {
			Tupla<int,int> tupla = new Tupla<int, int>(vidaSerializable.posicionesColindantes[i].e1, vidaSerializable.posicionesColindantes[i].e2);
			vida.posicionesColindantes.Add(tupla);
		}
		vida.matrizRadio3Circular = new int[vidaSerializable.matrizRadio3Circular.GetLength(0), vidaSerializable.matrizRadio3Circular.GetLength(1)];
		for (int i = 0; i < vidaSerializable.matrizRadio3Circular.GetLength(0); i++) {
			for (int j = 0; j < vidaSerializable.matrizRadio3Circular.GetLength(1); j++) {
				vida.matrizRadio3Circular[i,j] = vidaSerializable.matrizRadio3Circular[i,j];
			}
		}
		vida.matrizRadio4Circular = new int[vidaSerializable.matrizRadio4Circular.GetLength(0), vidaSerializable.matrizRadio4Circular.GetLength(1)];
		for (int i = 0; i < vidaSerializable.matrizRadio4Circular.GetLength(0); i++) {
			for (int j = 0; j < vidaSerializable.matrizRadio4Circular.GetLength(1); j++) {
				vida.matrizRadio4Circular[i,j] = vidaSerializable.matrizRadio4Circular[i,j];
			}
		}
		vida.matrizRadio5Circular = new int[vidaSerializable.matrizRadio5Circular.GetLength(0), vidaSerializable.matrizRadio5Circular.GetLength(1)];
		for (int i = 0; i < vidaSerializable.matrizRadio5Circular.GetLength(0); i++) {
			for (int j = 0; j < vidaSerializable.matrizRadio5Circular.GetLength(1); j++) {
				vida.matrizRadio5Circular[i,j] = vidaSerializable.matrizRadio5Circular[i,j];
			}
		}
		vida.matrizRadio6Circular = new int[vidaSerializable.matrizRadio6Circular.GetLength(0), vidaSerializable.matrizRadio6Circular.GetLength(1)];
		for (int i = 0; i < vidaSerializable.matrizRadio6Circular.GetLength(0); i++) {
			for (int j = 0; j < vidaSerializable.matrizRadio6Circular.GetLength(1); j++) {
				vida.matrizRadio6Circular[i,j] = vidaSerializable.matrizRadio6Circular[i,j];
			}
		}
		
		rehacerTablero(vidaSerializable.tablero, ref vida.tablero);

	}
	
	/* Genera un archivo con los datos del tablero de la partida que es serializable
	 * a partir de un tablero no serializable.
	 */
	private static void generarTableroSerializable(Casilla[,] tablero, ref CasillaSerializable[,] tableroSerializable){
		tableroSerializable = new CasillaSerializable[tablero.GetLength(0), tablero.GetLength(1)];
		for (int i = 0; i < tablero.GetLength(0); i++) {
			for (int j = 0; j < tablero.GetLength(1); j++) {
				CasillaSerializable temp = new CasillaSerializable();
				if (tablero[i,j].animal != null)
					temp.animal = getAnimalSerializable(tablero[i,j].animal);
				else
					temp.animal = null;
				if (tablero[i,j].edificio != null)
					temp.edificio = getEdificioSerializable(tablero[i,j].edificio);
				else
					temp.edificio = null;
				if(tablero[i,j].vegetal != null)
					temp.vegetal = getVegetalSerializable(tablero[i,j].vegetal);
				else
					temp.vegetal = null;
				temp.coordsTexX = tablero[i,j].coordsTex.x;
				temp.coordsTexY = tablero[i,j].coordsTex.y;
				temp.coordsVertX = tablero[i,j].coordsVert.x;
				temp.coordsVertY = tablero[i,j].coordsVert.y;
				temp.coordsVertZ = tablero[i,j].coordsVert.z;
				temp.elementos = tablero[i,j].elementos;
				temp.habitat = tablero[i,j].habitat;
				if (tablero[i,j].pinceladas != null) {
					temp.pinceladas = new float[tablero[i,j].pinceladas.Length * 2];
					for (int k = 0; k < tablero[i,j].pinceladas.Length; k++) {
						temp.pinceladas[k * 2] = tablero[i,j].pinceladas[k].x;
						temp.pinceladas[k * 2 + 1] = tablero[i,j].pinceladas[k].y;
					}
				}				
				tableroSerializable[i,j] = temp;
			}
		}
	}
	
	/* Construye un tablero usable con los datos del tablero serializado.
	 */
	private static void rehacerTablero(CasillaSerializable[,] tableroSerializable, ref Casilla[,] tablero) {
		tablero = new Casilla[tableroSerializable.GetLength(0), tableroSerializable.GetLength(1)];
		for (int i = 0; i < tablero.GetLength(0); i++) {
			for (int j = 0; j < tablero.GetLength(1); j++) {			
				Casilla temp = new Casilla();
				if (tableroSerializable[i,j].animal != null)
					temp.animal = getAnimalNoSerializable(tableroSerializable[i,j].animal);
				Vector2 vect = new Vector2(tableroSerializable[i,j].coordsTexX, tableroSerializable[i,j].coordsTexY);
				temp.coordsTex = vect;
				Vector3 vect3 = new Vector3(tableroSerializable[i,j].coordsVertX, tableroSerializable[i,j].coordsVertY, tableroSerializable[i,j].coordsVertZ);
				temp.coordsVert = vect3;
				if (tableroSerializable[i,j].edificio != null)
					temp.edificio = getEdificioNoSerializable(tableroSerializable[i,j].edificio);
				temp.elementos = tableroSerializable[i,j].elementos;
				temp.habitat = tableroSerializable[i,j].habitat;
				if (tableroSerializable[i,j].pinceladas != null) {
					temp.pinceladas = new Vector2[tableroSerializable[i,j].pinceladas.Length / 2];
					for (int k = 0; k < tableroSerializable[i,j].pinceladas.Length; k++) {
						Vector2 vect2 = new Vector2(tableroSerializable[i,j].pinceladas[k * 2], tableroSerializable[i,j].pinceladas[k * 2 + 1]);
						temp.pinceladas[k] = vect2;
					}
				}
				if (tableroSerializable[i,j].vegetal != null)
					temp.vegetal = getVegetalNoSerializable(tableroSerializable[i,j].vegetal);
				tablero[i,j] = temp;
			}
		}
	}
	
	private static SerSerializable getSerSerializable(Ser ser) {
		SerSerializable resultado = new SerSerializable();
		resultado.idSer = ser.idSer;
		resultado.posX = ser.posX;
		resultado.posY = ser.posY;
		return resultado;
	}
	
	private static Ser getSerNoSerializable(SerSerializable ser) {
		Ser resultado = new Ser();
		resultado.idSer = ser.idSer;
		resultado.posX = ser.posX;
		resultado.posY = ser.posY;
		return resultado;
	}
	
	private static VegetalSerializable getVegetalSerializable(Vegetal veg) {
		VegetalSerializable resultado = new VegetalSerializable();
		resultado.idSer = veg.idSer;
		resultado.numVegetales = veg.numVegetales;
		resultado.posX = veg.posX;
		resultado.posY = veg.posY;
		resultado.modelo = getModeloSerializable(veg.especie);
		resultado.especie = getEspecieVegSerializable(veg.especie);
		resultado.indiceHabitat = veg.indiceHabitat;
		resultado.turnosEvolucion = veg.turnosEvolucion;
		resultado.habitabilidad = new List<float>();
		for (int i = 0; i < veg.habitabilidad.Count; i++) {
			resultado.habitabilidad.Add(veg.habitabilidad[i]);
		}
		return resultado;
	}
	
	private static Vegetal getVegetalNoSerializable(VegetalSerializable veg) {
		int idSer = veg.idSer;
		EspecieVegetal especieVeg = getEspecieVegNoSerializable(veg.especie);
		int posX = veg.posX;
		int posY = veg.posY;
		List<GameObject> modelo = new List<GameObject>();
		for (int i = 0; i < 4; i++) {
			modelo.Add(getModeloNoSerializable(veg.modelo, i));
		}
		int indiceHabitat = veg.indiceHabitat;
		int turnosEvolucion = veg.turnosEvolucion;
		int numVeg = veg.numVegetales;
		List<float> habitabilidad = new List<float>();
		for (int i = 0; i < veg.habitabilidad.Count; i++) {
			habitabilidad.Add(veg.habitabilidad[i]);
		}
		return new Vegetal(idSer, especieVeg, posX, posY, habitabilidad, indiceHabitat, modelo, numVeg, turnosEvolucion);
	}
	
	private static AnimalSerializable getAnimalSerializable(Animal ani) {
		AnimalSerializable resultado = new AnimalSerializable();
		resultado.idSer = ani.idSer;
		resultado.posX = ani.posX;
		resultado.posY = ani.posY;
		resultado.reserva = ani.reserva;
		resultado.aguante = ani.aguante;
		resultado.estado = ani.estado;
		resultado.turnosParaReproduccion = ani.turnosParaReproduccion;
		resultado.modelo = getModeloSerializable(ani.especie);
		resultado.especie = getEspecieAniSerializable(ani.especie);
		return resultado;
	}
	
	private static Animal getAnimalNoSerializable(AnimalSerializable ani) {
		int idSer = ani.idSer;
		EspecieAnimal especieAni = getEspecieAniNoSerializable(ani.especie);
		int posX = ani.posX;
		int posY = ani.posY;
		GameObject modelo = getModeloNoSerializable(ani.modelo);
		int res = ani.reserva;
		int repr = ani.turnosParaReproduccion;
		int aguante = ani.aguante;
		tipoEstadoAnimal estado = ani.estado;
		return new Animal(idSer, especieAni, posX, posY, res, repr, modelo, aguante, estado);
	}
	
	private static EdificioSerializable getEdificioSerializable(Edificio edi) {
		EdificioSerializable resultado = new EdificioSerializable();
		resultado.compAvzConsumidosPorTurno = edi.compAvzConsumidosPorTurno;
		resultado.compAvzProducidosPorTurno = edi.compAvzProducidosPorTurno;
		resultado.compBasConsumidosPorTurno = edi.compBasConsumidosPorTurno;
		resultado.compBasProducidosPorTurno = edi.compBasProducidosPorTurno;
		resultado.energiaConsumidaPorTurno = edi.energiaConsumidaPorTurno;
		resultado.energiaProducidaPorTurno = edi.energiaProducidaPorTurno;
		resultado.matBioConsumidoPorTurno = edi.matBioConsumidoPorTurno;
		resultado.matBioProducidoPorTurno = edi.matBioProducidoPorTurno;
		resultado.idSer = edi.idSer;
		//resultado.matrizRadioAccion = edi.matrizRadioAccion;
		resultado.posX = edi.posX;
		resultado.posY = edi.posY;
		resultado.radioAccion = edi.radioAccion;
		resultado.modelo = getModeloSerializable(edi.tipo);
		resultado.tipo = getTipoEdifSerializable(edi.tipo);
		resultado.eficiencia = edi.eficiencia;
		resultado.numMetales = edi.numMetales;
		resultado.matBioSinProcesar = edi.matBioSinProcesar;
		resultado.radioAccion = edi.radioAccion;
		resultado.matrizRadioAccion = new List<Tupla<int, int, bool>>();
		for (int i = 0; i < edi.matrizRadioAccion.Count; i++) {
			Tupla<int, int, bool> tupla = new Tupla<int, int, bool>();
			tupla.e1 = edi.matrizRadioAccion[i].e1;
			tupla.e2 = edi.matrizRadioAccion[i].e2;
			tupla.e3 = edi.matrizRadioAccion[i].e3;
			resultado.matrizRadioAccion.Add(tupla);
		}
		return resultado;
	}
	
	private static Edificio getEdificioNoSerializable(EdificioSerializable edi) {
		int idSer = edi.idSer;
		TipoEdificio tipoEdif = getTipoEdifNoSerializable(edi.tipo);
		int posX = edi.posX;
		int posY = edi.posY;
		int eneCons = edi.energiaConsumidaPorTurno;
		int compBaCons = edi.compBasConsumidosPorTurno;
		int compAvCons = edi.compAvzConsumidosPorTurno;
		int matBioCons = edi.matBioConsumidoPorTurno;
		int eneProd = edi.energiaProducidaPorTurno;
		int compBaProd = edi.compBasProducidosPorTurno;
		int compAvProd = edi.compAvzProducidosPorTurno;
		int matBioProd = edi.matBioProducidoPorTurno;
		float eficiencia = edi.eficiencia;
		int numMetales = edi.numMetales;
		int matBioSin = edi.matBioSinProcesar;
		int radioAccion = edi.radioAccion;
		List<Tupla<int, int, bool>> matrizRadioAccion = new List<Tupla<int, int, bool>>();
		for (int i = 0; i < edi.matrizRadioAccion.Count; i++) {
			Tupla<int, int, bool> tupla = new Tupla<int, int, bool>();
			tupla.e1 = edi.matrizRadioAccion[i].e1;
			tupla.e2 = edi.matrizRadioAccion[i].e2;
			tupla.e3 = edi.matrizRadioAccion[i].e3;
			matrizRadioAccion.Add(tupla);
		}
		GameObject modelo = getModeloNoSerializable(edi.modelo);
		return new Edificio(idSer, tipoEdif, posX, posY, eficiencia, numMetales, matrizRadioAccion, radioAccion, modelo, eneCons, compBaCons, compAvCons, matBioCons, eneProd, compBaProd, compAvProd, matBioProd, matBioSin);
	}
	
	private static EspecieSerializable getEspecieSerializable(Especie esp) {
		EspecieSerializable resultado = new EspecieSerializable();
		resultado.idEspecie = esp.idEspecie;
		resultado.nombre = esp.nombre;
		resultado.numMaxSeresEspecie = esp.numMaxSeresEspecie;
		resultado.numSeresEspecie = esp.numSeresEspecie;
		resultado.modelos = new int[esp.modelos.Count];
		for (int i = 0; i < esp.modelos.Count; i++) {
			resultado.modelos[i] = getModeloSerializable(esp);
		}
		return resultado;
	}
	
	private static Especie getEspecieNoSerializable(EspecieSerializable esp) {
		Especie resultado = new Especie();
		resultado.idEspecie = esp.idEspecie;
		resultado.nombre = esp.nombre;
		resultado.numMaxSeresEspecie = esp.numMaxSeresEspecie;
		resultado.numSeresEspecie = esp.numSeresEspecie;
		resultado.modelos = new List<GameObject>();
		for (int i = 0; i < esp.modelos.Length; i++) {
			resultado.modelos.Add(getModeloNoSerializable(esp.modelos[i]));
		}
		return resultado;
	}
	
	private static EspecieVegetal getEspecieVegNoSerializable(EspecieVegetalSerializable veg) {
		string nombre = veg.nombre;
		int numMaxVegetales = veg.numMaxVegetales;
		int numIniVegetales = veg.numIniVegetales;
		float capacidadMigracionLocal = veg.capacidadMigracionLocal;
		float capacidadMigracionGlobal = veg.capacidadMigracionGlobal;
		int radioMigracion = veg.radioMigracion;
		int idTextura = veg.idTextura;
		List<GameObject> modelos = new List<GameObject>();
		for (int i = 0; i < veg.modelos.Length; i++)
			modelos.Add(getModeloNoSerializable(veg.modelos[i], i));
		int turnosEvolucion = veg.turnosEvolucionInicial;
		float evolucion = veg.evolucion;
		int numMaxModelos = veg.numMaxModelos;
		int idEspecie = veg.idEspecie;
		int numMaxSeresEspecie = veg.numMaxSeresEspecie;
		int numSeresEspecie = veg.numSeresEspecie;
		List<float> habitabilidad = new List<float>();
		for (int i = 0; i < veg.habitabilidadInicial.Count; i++) {
			habitabilidad.Add(veg.habitabilidadInicial[i]);
		}		
		return new EspecieVegetal(nombre, numMaxSeresEspecie, numMaxVegetales, numIniVegetales, capacidadMigracionLocal, capacidadMigracionGlobal, radioMigracion, turnosEvolucion, evolucion, habitabilidad, idTextura, numMaxModelos, modelos, numSeresEspecie, idEspecie);//return new EspecieVegetal(nombre, num numMaxVegetales, numIniVegetales, capacidadReproductiva, capacidadMigracionLocal, capacidadMigracionGlobal, radioMigracion, habitats, idTextura, modelos);
	}
	
	private static EspecieVegetalSerializable getEspecieVegSerializable(EspecieVegetal veg) {
		EspecieVegetalSerializable resultado = new EspecieVegetalSerializable();
		resultado.capacidadMigracionGlobal = veg.capacidadMigracionGlobal;
		resultado.capacidadMigracionLocal = veg.capacidadMigracionLocal;
		resultado.idEspecie = veg.idEspecie;
		resultado.idTextura = veg.idTextura;
		resultado.nombre = veg.nombre;
		resultado.numIniVegetales = veg.numIniVegetales;
		resultado.numMaxVegetales = veg.numMaxVegetales;
		resultado.radioMigracion = veg.radioMigracion;
		resultado.turnosEvolucionInicial = veg.turnosEvolucionInicial;
		resultado.evolucion = veg.evolucion;
		resultado.numMaxModelos = veg.numMaxModelos;
		resultado.habitabilidadInicial = new List<float>();
		for (int i = 0; i < veg.habitabilidadInicial.Count; i++) {
			resultado.habitabilidadInicial.Add(veg.habitabilidadInicial[i]);
		}
		resultado.numMaxSeresEspecie = veg.numMaxSeresEspecie;
		resultado.numSeresEspecie = veg.numSeresEspecie;
		resultado.modelos = new int[veg.modelos.Count];
		for (int i = 0; i < veg.modelos.Count; i++) {
			resultado.modelos[i] = getModeloSerializable(veg);
		}
		return resultado;
	}
	
	private static EspecieAnimal getEspecieAniNoSerializable(EspecieAnimalSerializable ani) {
		string nombre = ani.nombre;
		int consumo = ani.consumo;
		int reservaMax = ani.reservaMaxima;
		int alimentoMaxTurno = ani.alimentoMaxTurno;
		int aguanteInicial = ani.aguanteInicial;
		int repro = ani.reproductibilidad;
		tipoAlimentacionAnimal tipoAlim = ani.tipo;
		List<T_habitats> habitats = ani.habitats;
		int idEspecie = ani.idEspecie;
		int numMaxSeresEspecie = ani.numMaxSeresEspecie;
		int numSeresEspecie = ani.numSeresEspecie;
		List<GameObject> modelos = new List<GameObject>();		
		for (int i = 0; i < ani.modelos.Length; i++)
			modelos.Add(getModeloNoSerializable(ani.modelos[i], i));
		return new EspecieAnimal(nombre, numMaxSeresEspecie, consumo, reservaMax, alimentoMaxTurno, aguanteInicial, repro, tipoAlim, habitats, modelos, numSeresEspecie, idEspecie);
	}
	
	private static EspecieAnimalSerializable getEspecieAniSerializable(EspecieAnimal ani) {
		EspecieAnimalSerializable resultado = new EspecieAnimalSerializable();
		resultado.nombre = ani.nombre;
		resultado.consumo = ani.consumo;
		resultado.reservaMaxima = ani.reservaMaxima;
		resultado.aguanteInicial = ani.aguanteInicial;
		resultado.alimentoMaxTurno = ani.alimentoMaxTurno;
		resultado.reproductibilidad = ani.reproductibilidad;
		resultado.tipo = ani.tipo;
		resultado.habitats = ani.habitats;
		resultado.modelos = new int[ani.modelos.Count];
		for (int i = 0; i < ani.modelos.Count; i++) {
			resultado.modelos[i] = getModeloSerializable(ani);
		}
		resultado.idEspecie = ani.idEspecie;
		resultado.numMaxSeresEspecie = ani.numMaxSeresEspecie;
		resultado.numSeresEspecie = ani.numSeresEspecie;
		return resultado;
	}
	
	private static TipoEdificio getTipoEdifNoSerializable(TipoEdificioSerializable edi) {
		int idTipoEdificio = edi.idTipoEdificio;
		string nombre = edi.nombre;
		List<T_habitats> habitats = edi.habitats;
		int eneCons = edi.energiaConsumidaAlCrear;
		int compBaCons = edi.compBasConsumidosAlCrear;
		int compAvCons = edi.compAvzConsumidosAlCrear;
		int matBioCons = edi.matBioConsumidoAlCrear;
		T_elementos elemNeces = edi.elemNecesarioAlConstruir;
		int eneConsMax = edi.energiaConsumidaPorTurnoMax;
		int compBaConsMax = edi.compBasConsumidosPorTurnoMax;
		int compAvConsMax = edi.compAvzConsumidosPorTurnoMax;
		int matBioConsMax = edi.matBioConsumidoPorTurnoMax;		
		int eneProdMax = edi.energiaProducidaPorTurnoMax;
		int compBaProdMax = edi.compBasProducidosPorTurnoMax;
		int compAvProdMax = edi.compAvzProducidosPorTurnoMax;
		int matBioProdMax = edi.matBioProducidoPorTurnoMax;
		List<GameObject> modelos = new List<GameObject>();
		for (int i = 0; i < edi.modelos.Length; i++)
			modelos.Add(getModeloNoSerializable(edi.modelos[i], i));
		return new TipoEdificio(nombre, habitats, eneCons, compBaCons, compAvCons, matBioCons, elemNeces, eneConsMax, compBaConsMax, compAvConsMax, matBioConsMax, eneProdMax, compBaProdMax, compAvProdMax, matBioProdMax, modelos, idTipoEdificio);
	}
	
	private static TipoEdificioSerializable getTipoEdifSerializable(TipoEdificio edi) {
		TipoEdificioSerializable resultado = new TipoEdificioSerializable();
		resultado.idTipoEdificio = edi.idTipoEdificio;
		resultado.nombre = edi.nombre;
		resultado.habitats = edi.habitats;
		resultado.energiaConsumidaAlCrear = edi.energiaConsumidaAlCrear;
		resultado.compBasConsumidosAlCrear = edi.compBasConsumidosAlCrear;
		resultado.compAvzConsumidosAlCrear = edi.compAvzConsumidosAlCrear;
		resultado.matBioConsumidoAlCrear = edi.matBioConsumidoAlCrear;
		resultado.elemNecesarioAlConstruir = edi.elemNecesarioAlConstruir;
		
		resultado.energiaConsumidaPorTurnoMax = edi.energiaConsumidaPorTurnoMax;
		resultado.compBasConsumidosPorTurnoMax = edi.compBasConsumidosPorTurnoMax;
		resultado.compAvzConsumidosPorTurnoMax = edi.compAvzConsumidosPorTurnoMax;
		resultado.matBioConsumidoPorTurnoMax = edi.matBioConsumidoPorTurnoMax;
		resultado.energiaProducidaPorTurnoMax = edi.energiaProducidaPorTurnoMax;
		resultado.compBasProducidosPorTurnoMax = edi.compBasProducidosPorTurnoMax;
		resultado.compAvzProducidosPorTurnoMax = edi.compAvzProducidosPorTurnoMax;
		resultado.matBioProducidoPorTurnoMax = edi.matBioProducidoPorTurnoMax;
		
		resultado.modelos = new int[edi.modelos.Count];
		for (int i = 0; i < edi.modelos.Count; i++) {
			resultado.modelos[i] = getModeloSerializable(edi);
		}
		return resultado;
	}
	
	private static GameObject getModeloNoSerializable(int referencia) {
		if (modelosAnimales == null || modelosEdificios == null || modelosVegetales == null) {
			if (!iniciaVariablesEstaticas()) {
				Debug.LogError("Error al iniciar variables estaticas. Puede estar en una escena incorrecta?");
				return null;
			}
		}
		switch (referencia) {
			//Edificios [20-24]
		case 20:		//Fabrica componentes basicos
			return GameObject.Instantiate(modelosEdificios.fabCompBas) as GameObject;
		case 21:		//Central de energia
			return GameObject.Instantiate(modelosEdificios.centralEnergia) as GameObject;
		case 22:		//Granja
			return GameObject.Instantiate(modelosEdificios.granja) as GameObject;
		case 23:		//Fabrica componentes avanzados
			return GameObject.Instantiate(modelosEdificios.fabCompAdv) as GameObject;
		case 24:		//Central energia avanzada
			return GameObject.Instantiate(modelosEdificios.centralEnergiaAdv) as GameObject;
		//Animales [10-19]
		case 10:		//Conejo
			return GameObject.Instantiate(modelosAnimales.herbivoro1[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 11:		//Camello
			return GameObject.Instantiate(modelosAnimales.herbivoro2[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 12:		//Tortuga
			return GameObject.Instantiate(modelosAnimales.herbivoro3[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 13:		//Ciervo
			return GameObject.Instantiate(modelosAnimales.herbivoro4[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 14:		//Salamandra
			return GameObject.Instantiate(modelosAnimales.herbivoro5[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 15:		//Zorro
			return GameObject.Instantiate(modelosAnimales.carnivoro1[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 16:		//Lobo
			return GameObject.Instantiate(modelosAnimales.carnivoro2[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 17:		//Serpiente
			return GameObject.Instantiate(modelosAnimales.carnivoro3[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 18:		//Tigre
			return GameObject.Instantiate(modelosAnimales.carnivoro4[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 19:		//Velociraptor
			return GameObject.Instantiate(modelosAnimales.carnivoro5[UnityEngine.Random.Range(0,4)]) as GameObject;
		//Vegetales [0-9]
		case 0:	//Seta
			return GameObject.Instantiate(modelosVegetales.setas[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 1:	//Flor
			return GameObject.Instantiate(modelosVegetales.flores[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 2:	//Caña
			return GameObject.Instantiate(modelosVegetales.canas[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 3:	//Arbusto
			return GameObject.Instantiate(modelosVegetales.arbustos[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 4:	//Estromatolito
			return GameObject.Instantiate(modelosVegetales.estromatolitos[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 5:	//Cactus
			return GameObject.Instantiate(modelosVegetales.cactus[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 6:	//Palmera
			return GameObject.Instantiate(modelosVegetales.palmeras[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 7:	//Pino
			return GameObject.Instantiate(modelosVegetales.pinos[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 8:	//Cipres
			return GameObject.Instantiate(modelosVegetales.cipreses[UnityEngine.Random.Range(0,4)]) as GameObject;
		case 9:	//Pino alto
			return GameObject.Instantiate(modelosVegetales.pinosAltos[UnityEngine.Random.Range(0,4)]) as GameObject;
		default:
			Debug.LogError("La referencia de entrada al metodo getModeloNoSerializable(int) no es valida! Num = " + referencia.ToString());
			return GameObject.CreatePrimitive(PrimitiveType.Cube);
		}
	}
	
	private static GameObject getModeloNoSerializable(int referencia, int numero) {
		if (modelosAnimales == null || modelosEdificios == null || modelosVegetales == null) {
			if (!iniciaVariablesEstaticas()) {
				Debug.LogError("Error al iniciar variables estaticas. Puede estar en una escena incorrecta?");
				return null;
			}
		}
		switch (referencia) {
			//Edificios [20-24]
		case 20:		//Fabrica componentes basicos
			return GameObject.Instantiate(modelosEdificios.fabCompBas) as GameObject;
		case 21:		//Central de energia
			return GameObject.Instantiate(modelosEdificios.centralEnergia) as GameObject;
		case 22:		//Granja
			return GameObject.Instantiate(modelosEdificios.granja) as GameObject;
		case 23:		//Fabrica componentes avanzados
			return GameObject.Instantiate(modelosEdificios.fabCompAdv) as GameObject;
		case 24:		//Central energia avanzada
			return GameObject.Instantiate(modelosEdificios.centralEnergiaAdv) as GameObject;
		//Animales [10-19]
		case 10:		//Conejo
			return GameObject.Instantiate(modelosAnimales.herbivoro1[numero]) as GameObject;
		case 11:		//Camello
			return GameObject.Instantiate(modelosAnimales.herbivoro2[numero]) as GameObject;
		case 12:		//Tortuga
			return GameObject.Instantiate(modelosAnimales.herbivoro3[numero]) as GameObject;
		case 13:		//Ciervo
			return GameObject.Instantiate(modelosAnimales.herbivoro4[numero]) as GameObject;
		case 14:		//Salamandra
			return GameObject.Instantiate(modelosAnimales.herbivoro5[numero]) as GameObject;
		case 15:		//Zorro
			return GameObject.Instantiate(modelosAnimales.carnivoro1[numero]) as GameObject;
		case 16:		//Lobo
			return GameObject.Instantiate(modelosAnimales.carnivoro2[numero]) as GameObject;
		case 17:		//Serpiente
			return GameObject.Instantiate(modelosAnimales.carnivoro3[numero]) as GameObject;
		case 18:		//Tigre
			return GameObject.Instantiate(modelosAnimales.carnivoro4[numero]) as GameObject;
		case 19:		//Velociraptor
			return GameObject.Instantiate(modelosAnimales.carnivoro5[numero]) as GameObject;
		//Vegetales [0-9]
		case 0:	//Seta
			return GameObject.Instantiate(modelosVegetales.setas[numero]) as GameObject;
		case 1:	//Flor
			return GameObject.Instantiate(modelosVegetales.flores[numero]) as GameObject;
		case 2:	//Caña
			return GameObject.Instantiate(modelosVegetales.canas[numero]) as GameObject;
		case 3:	//Arbusto
			return GameObject.Instantiate(modelosVegetales.arbustos[numero]) as GameObject;
		case 4:	//Estromatolito
			return GameObject.Instantiate(modelosVegetales.estromatolitos[numero]) as GameObject;
		case 5:	//Cactus
			return GameObject.Instantiate(modelosVegetales.cactus[numero]) as GameObject;
		case 6:	//Palmera
			return GameObject.Instantiate(modelosVegetales.palmeras[numero]) as GameObject;
		case 7:	//Pino
			return GameObject.Instantiate(modelosVegetales.pinos[numero]) as GameObject;
		case 8:	//Cipres
			return GameObject.Instantiate(modelosVegetales.cipreses[numero]) as GameObject;
		case 9:	//Pino alto
			return GameObject.Instantiate(modelosVegetales.pinosAltos[numero]) as GameObject;
		default:
			Debug.LogError("La referencia de entrada al metodo getModeloNoSerializable(int) no es valida! Num = " + referencia.ToString());
			return GameObject.CreatePrimitive(PrimitiveType.Cube);
		}
	}
	
	private static int getModeloSerializable(Especie esp) {
		if (esp.nombre.Equals("Caracol") || esp.nombre.Equals("Conejo") || esp.nombre.Equals("Vaca") || esp.nombre.Equals("Jirafa") || esp.nombre.Equals("Estegosaurio") || esp.nombre.Equals("Rata") || esp.nombre.Equals("Lobo") || esp.nombre.Equals("Tigre") || esp.nombre.Equals("Oso") || esp.nombre.Equals("Tiranosaurio")) {
			return esp.idEspecie + 10;
		}
		else
			return esp.idEspecie;
	}
	
	private static int getModeloSerializable(EspecieVegetal esp) {		//Sobrecargado
		return esp.idEspecie;
	}
	
	private static int getModeloSerializable(EspecieAnimal esp) {		//Sobrecargado
		return esp.idEspecie + 10;
	}
	
	private static int getModeloSerializable(TipoEdificio tipo) {
		return tipo.idTipoEdificio + 20;
	}
	
	private static bool iniciaVariablesEstaticas() {
		GameObject temp = GameObject.FindGameObjectWithTag("ModelosAnimales");
		modelosAnimales = temp.GetComponent<ModelosAnimales>();
		temp = GameObject.FindGameObjectWithTag("ModelosVegetales");
		modelosVegetales = temp.GetComponent<ModelosVegetales>();
		temp = GameObject.FindGameObjectWithTag("ModelosEdificios");
		modelosEdificios = temp.GetComponent<ModelosEdificios>();
		if (modelosAnimales == null || modelosEdificios == null || modelosVegetales == null)
			return false;
		else 
			return true;
	}
	
//A partir de aqui es codigo para guardar los indices de mapear los UVs a las casillas -------------------------------------------------------------------------
	
	//Objeto con los indices
	public static void SaveIndices (int[] indices)
  	{ 
	    SaveIndices save = new SaveIndices();
		save.indices = indices;
	    FileStream stream = new FileStream(Application.dataPath + "/Indices.bin", FileMode.Create);
		Debug.Log("Archivo de indices guardado en: " + Application.dataPath);
		try {
		    BinaryFormatter bformatter = new BinaryFormatter();
		    bformatter.Serialize(stream, save);
		}
		catch (SerializationException e) {
			Debug.LogError("Excepcion al serializar el archivo de indices. Datos: " + e.Message);
		}
		finally {
	    	stream.Close();
		}
  	}

  	public static SaveIndices LoadIndices ()  {									//Sobrecargado
		return LoadIndices(Application.dataPath + "/Indices.bin");
	} 
	
  	public static SaveIndices LoadIndices(string filePath) 
  	{
	    SaveIndices data = new SaveIndices();
	    FileStream stream = new FileStream(filePath, FileMode.Open);
		try {
		    BinaryFormatter bformatter = new BinaryFormatter();
		    data = (SaveIndices)bformatter.Deserialize(stream);
		}
		catch (SerializationException e) {
			Debug.LogError("Excepcion al deserializar el savegame. Datos: " + e.Message);
		}
		finally {
			stream.Close();
		}
		return data;
	}
	
	//Utilidades para las rutas y los directorios --------------------------------------------------------------------------------------------------------------------------------
	
	public static int FileCount () {
		DirectoryInfo d = new DirectoryInfo(currentFilePath);
		FileInfo[] ret = d.GetFiles();
		int num = ret.GetLength(0); 
		return num; 
	}
	
	public static string[] getFileNames() {
		DirectoryInfo d = new DirectoryInfo(currentFilePath);
		FileInfo[] ret = d.GetFiles();
		int num = ret.GetLength(0);
		string[] str = new string[num];
		for (int i = 0; i < num; i++) {
			str[i] = ret[i].Name;
		}
		return str;
	}
	
	public static bool existeFile(string nombre) {
		if (File.Exists(currentFilePath + nombre))
            return true;
		else 
			return false;
	}
	
	public static void compruebaRuta() {
		if (!Directory.Exists(currentFilePath))	{
			Directory.CreateDirectory(currentFilePath);	
		}
	}
	
	public static void cambiaFileName(string nuevo) {
		currentFileName = nuevo;
	}

}

