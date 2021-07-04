using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main : MonoBehaviour
{
    public GameObject modalAlert, modalExit;
    int countAnswer = 0;
    public Text scoreNumber, scorePosition,messageAlert;
    public Text[] inputNumber;

    string message = "จำนวนที่เดาทั้งหมด X ครั้ง \n ต้องการเล่นต่อหรือไม่";
    int[] randomNumber = { -1,-1,-1,-1};
    int[] arrInputNumber = { -1, -1, -1, -1 };
    // Start is called before the first frame update
    void Start()
    {
        RandomNumberHandler();
        // ======= show random number ==============
        //foreach (var value in randomNumber)
        //{
        //    Debug.Log("rendom Number : " + value);
        //}
        // ======= show random number ==============

    }

    public void RestartGame() {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }



    public void CalInputNumber() {
        countAnswer++;
        int countScoreNumber = 0;
        int countScorePosition = 0;
        int i =  0;
        int lengthRandomNumber = randomNumber.Length;
      
        
        foreach (var value in inputNumber) {
            int numberInput = int.Parse(value.text);
            arrInputNumber[i] = numberInput;
            var result = FindRandomNumberArray(randomNumber, numberInput);
            if (result) {
                countScoreNumber++;
            }
            int arrIndex = Array.FindIndex(randomNumber, 0, lengthRandomNumber, index => index == numberInput);
            if (i == arrIndex) {
                countScorePosition++;
            }
            i++;
        }

        int numberInputDist = arrInputNumber.Distinct().Count();
        if (numberInputDist != lengthRandomNumber) {
            if (countScoreNumber>1) {
                countScoreNumber = countScoreNumber - (lengthRandomNumber - numberInputDist);
            }
            
        }
        scoreNumber.text = "" + countScoreNumber;
        scorePosition.text = "" + countScorePosition;

        if (countScoreNumber == lengthRandomNumber && countScorePosition == lengthRandomNumber) {
            message = message.Replace("X", "" + countAnswer);
            messageAlert.text = message;
            modalAlert.SetActive(true);
        }
    }

    void ClearRandomNumber() {
        for (int i = 0; i < randomNumber.Length; i++) {
            randomNumber[i] = -1;
        }
    }

    bool FindRandomNumberArray(Array data,int numberFind) {
        bool result = false;
        foreach (var value in randomNumber) {
            if (value == numberFind)
            {
                return true;
            }
        }
        return result;
    }

    void RandomNumberHandler() {
        ClearRandomNumber();
        for (int i = 0; i < randomNumber.Length; i++) {
            bool result = true;
            while (result) {
                int numberRandom = RandomNumber(0, 9);
                result = FindRandomNumberArray(randomNumber, numberRandom);
                if (!result) {
                    randomNumber[i] = numberRandom;
                }
            } 
        }

    }

    public int RandomNumber(int min, int max)
    {
        return UnityEngine.Random.Range(min, max);
    }

    public void OnExit() {
        #if UNITY_EDITOR
                // Application.Quit() does not work in the editor so
                // UnityEditor.EditorApplication.isPlaying need to be set to false to end the game
                UnityEditor.EditorApplication.isPlaying = false;
        #else
                    Application.Quit();
        #endif
    }
}
