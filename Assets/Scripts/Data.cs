using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Firebase.Firestore;
using Firebase.Extensions;
using UnityEditor;
using Unity.VisualScripting;

[Serializable]

public class dataToSave
{
    public int totalCoins;
}



public class Data : MonoBehaviour
{
    public long monedas;


    void Start()
    {
        ObtenerDocumento();
    }

    async void ObtenerDocumento()
    {
        // Se obtiene la instancia, la coleccion y el documento
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("Juego2D").Document("totalMonedas");

        try
        {
            // realiza la solicitud a la DB
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();

            // verificacion si existe la informacion
            if (snapshot.Exists)
            {
                Debug.Log($"Documento encontrado: {snapshot.Id}");
                Dictionary<string, object> datos = snapshot.ToDictionary();

                // guarda la informacion de la cantidad de monedas en una variable
                monedas = datos.ContainsKey("monedas") ? (long)datos["monedas"] : 0;
                Debug.Log($"Total de monedas: {monedas}");

                // manda a llamar al proceso de actualizacion del documento
                ActualizarDatosUsuario();
            }
            else
            {
                Debug.Log($"El documento con ID {snapshot.Id} no existe.");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError($"Error al obtener el documento: {ex.Message}");
        }
    }
    


    async void ActualizarDatosUsuario()
    {
        // Se obtiene la instancia, la coleccion y el documento
        FirebaseFirestore db = FirebaseFirestore.DefaultInstance;
        DocumentReference docRef = db.Collection("Juego2D").Document("totalMonedas");

        // se modifica el campo de monedas, tomando como total el valor original y le sumamos 1
        Dictionary<string, object> actualizaciones = new Dictionary<string, object>
        {
            { "monedas", monedas +1 }
        };

        try
        {
            // se ejecuta la actualizacion
            await docRef.UpdateAsync(actualizaciones);

            Debug.Log("¡Documento actualizado con éxito!");
        }
        catch (Exception ex)
        {
            // Si falla, manda el mensaje de error a la consola
            Debug.LogError($"Error al actualizar el documento: {ex.Message}");
        }
    }



}
