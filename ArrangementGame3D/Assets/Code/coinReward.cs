using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class coinReward : MonoBehaviour
{
    private GameData gameData;
    public GameObject coinPrefab; 
    public Transform coinParent; 
    public TMP_Text coinText; 

    public int coinCount = 10; 
    public float coinSpeed = 10f; // Koinlerin hareket hızı
    
    
    


    private SpawnManager _spawnManager;

    private void Start()
    {
        coinCount += 10;
        AnimationCoinStart();
        
    }

    void AnimationCoinStart()
    {
         coinText.text = coinCount.ToString();
                        
                 for (int i = 0; i < coinCount; i++)
                 {
                     GameObject coin = Instantiate(coinPrefab, coinParent);
                     coin.SetActive(false);
                        
                     float delay = Random.Range(0.5f, 2.0f);
                     StartCoroutine(AnimateCoin(coin, delay));
                 }

                 gameData.goldGame += 10;
                 Debug.Log(gameData.goldGame);
    }

    private IEnumerator AnimateCoin(GameObject coin, float delay)
    {
        yield return new WaitForSeconds(delay);

        coin.SetActive(true);
        Vector3 targetPosition = coinText.transform.position;

        while (coin.transform.position != targetPosition)
        {
            coin.transform.position = Vector3.MoveTowards(coin.transform.position, targetPosition, coinSpeed * Time.deltaTime);
            yield return null;
        }

        coinCount--;
        coinText.text = coinCount.ToString();

        Destroy(coin);
    }
}