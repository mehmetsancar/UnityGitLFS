using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
   private SpawnManager spawnManager = new SpawnManager();

   private void Update()
   {
      if (Input.GetMouseButtonDown(0))
      {
         Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
         RaycastHit hit;
         Debug.Log("hit");
         if (Physics.Raycast(ray, out hit))
         {
            GameObject clickedObject = hit.transform.gameObject;
            Debug.Log("hit.transform.gameObject");
            // Tıklanan nesnenin listede olup olmadığını kontrol etme
            for (int i = 0; i < spawnManager._gameObjects.Count; i++)
            {
               if (clickedObject == spawnManager._gameObjects[i])
               {
                  Destroy(clickedObject); // Nesneyi yok etme
                  spawnManager._gameObjects[i] = null; // Nesneyi listeden çıkarma
                  break;
               }
            }
         }
      }
   }
}
   

