using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    private SpawnManager _spawnManager;
    public List<Transform> _gameObjectTargetPosition;
    private int ListCount = 0;
    

    private void Start()
    {
        _spawnManager = FindObjectOfType<SpawnManager>();
    }

    public void ClickedObjectMove(GameObject clickedObject)
    {
        
            clickedObject.transform.DOMove(_gameObjectTargetPosition[ListCount].position, 0.8f);
            ListCount++;
            if (ListCount == 3)
            {
                
                
                ListCount = 0;
            }
           
        
        
    }
    
    

   
    
    
}
