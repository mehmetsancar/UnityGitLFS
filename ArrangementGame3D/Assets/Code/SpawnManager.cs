using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    private TargetController targetController;
    private coinReward _coinReward;
    
    
    public List<GameObject> _gameObjects;
    public List<GameObject> _ClickedObjectsList;

    public List<Vector3> _gameObjectPosition;
    [SerializeField] private GameObject effectSucces;
    [SerializeField] private GameObject effectUnSucces;
    
    public bool isGameOverSuccesfully;
    [SerializeField] private GameObject NextLevelCanvas;



    //CONTROL
    private int gameOverControl = 0;
    public bool positionBack = false;

    
    void Start()
    {
        targetController = FindObjectOfType<TargetController>();
        SpawnObject();
    }

    
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            
            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.transform.gameObject;
                
               
                _gameObjectPosition.Add(clickedObject.transform.position);
                ClickedObjectControl(clickedObject);
                
                targetController.ClickedObjectMove(clickedObject);
            }
        }

        isGameOver();
        if (isGameOverSuccesfully == true)
        {
            NextLevelCanvas.SetActive(true);
        }
    }

    public void NextLevelButton()
    {
        SceneManager.LoadScene("2");
        NextLevelCanvas.SetActive(false);
    }

    public void ClickedObjectControl(GameObject _clickedGameObject)
    {
        _ClickedObjectsList.Add(_clickedGameObject);
        if (_ClickedObjectsList.Count == 3)
        {
            Invoke("ClickedObjectCheckAndDestroy",1);
            
        }
    }

    void ClickedObjectCheckAndDestroy()
    {
        if (_ClickedObjectsList.Count == 0)
        {
            return;
        }

        string firstClickTag = _ClickedObjectsList[0].tag;
        bool isSameTag = true;

        foreach (var gameObject in _ClickedObjectsList)
        {
            if (gameObject.tag != firstClickTag)
            {
                
                isSameTag = false;
                positionBack = true;
                effectUnSucces.SetActive(true);
                //_ClickedObjectsList.Clear();
                break;
            }

            
            

           
        }
        if (positionBack == true)
        {
            Invoke("ReturnObjectBack",0.5f);
            Invoke("DisableUnSuccesEffect", 0.5f);
            Debug.Log("invoke");
            
        }

         

        
        if (isSameTag)
        {
            
            gameOverControl++;
            effectSucces.SetActive(true);
            CombineGameObject();
            _coinReward.coinCount += 10;
            _coinReward.coinText.text = _coinReward.coinCount.ToString();
            //effect.SetActive(false);
            

            Debug.Log("buradayım");
            _gameObjectPosition.Clear();
        }
    }

   void DisableUnSuccesEffect() 
   {
       effectUnSucces.SetActive(false);  
    }

    void CombineGameObject()
    {
        
        for (int i = 0; i < 3; i++)
        {
            _ClickedObjectsList[i].transform.DOMove(targetController._gameObjectTargetPosition[1].position, 0.5f); 
        }
        
        StartCoroutine(DelayDestroyTime());
        
        


    }

    private IEnumerator DelayDestroyTime()
    {
        yield return new WaitForSeconds(0.7f);
        foreach (var gameObject in _ClickedObjectsList)
        {
            gameObject.SetActive(false);
            Debug.Log("same forec");
                
        }
        //_coinReward.AnimateCoin();
        effectSucces.SetActive(false);
        
        _ClickedObjectsList.Clear();
        
    }

    void ReturnObjectBack()
    {
        
            for (int i = 0; i < 3; i++)
            {
                _ClickedObjectsList[i].transform.DOMove(_gameObjectPosition[i], 1); 
            }
            _ClickedObjectsList.Clear();
            _gameObjectPosition.Clear();
            positionBack = false;

    }
    
    void SpawnObject()
    {
        foreach (var gameObject in _gameObjects)
        {
            for (int i = 0; i < 2; i++)
             {
                 float randomX = Random.Range(-4, 4);
                 float randomZ = Random.Range(-4, 4);
                 Vector3 randomPosition = new Vector3(randomX, 0, randomZ);
                 Instantiate(gameObject, randomPosition, Quaternion.identity);
             }
        }
       
    }

    public bool isGameOver()
    {
        if (_gameObjects.Count == gameOverControl)
        {
            Debug.Log("GAME OVER");
            isGameOverSuccesfully = true;

        }

        return true;
    }
    
    

   
}
