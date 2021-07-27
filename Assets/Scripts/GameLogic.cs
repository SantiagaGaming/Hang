using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameLogic : MonoBehaviour
{
    [SerializeField] GameObject[] mistakes;
    [SerializeField] GameObject[] letters;
    [SerializeField] GameObject gameOver;
    [SerializeField] GameObject keyboard;
    [SerializeField] Text endText;
    [SerializeField] Text rightText;
    [SerializeField] Text wrongText;

    private Words words;
    private Player player;
    private List<string> wordsList;
    private string answer;
    private int currentMistake;
    private string randomWord;
    char[] answerC;

    void Start()
    {    
        words = new Words();
        player = new Player();
        wordsList = new List<string>();
        InitWords();
        GameStart();

    }
    private void GameStart()
    {
        currentMistake = 0;
        NextLetter();
        answer = NextLetter();
        print(answer);
        WordHolder(answer.ToCharArray());
    }

    private void Mistake()
    {
        mistakes[currentMistake].SetActive(true);
        currentMistake++;
        if (currentMistake >= 6)
        {
            Lose();
        }
    }
    private string NextLetter()
    {
        int rnd = Random.Range(0, wordsList.Count);
         randomWord = wordsList[rnd]; 
        if (wordsList.Count <= 1)
        {
            InitWords();
        }
        
        return randomWord;
    }

    public void OnButtonClick(Button button)
    {
        char letter = button.GetComponentInChildren<Text>().text.ToCharArray()[0];

        if (answer.Contains(letter))
        {
            UpdateAnswerText(letter);
            CheckGameStatus();
        }
        else { Mistake();
        }
    }
    private void WordHolder(char[] wordLenght)
    {
        for (int i = 0; i < wordLenght.Length; i++)
        {
            letters[i].SetActive(true);
        }
    }
    private void UpdateAnswerText(char letter)
    {
        answerC = answer.ToArray();
        for (int i = 0; i <= answerC.Length - 1; i++)
        {
            if (answerC[i].Equals(letter))
            { letters[i].GetComponentInChildren<Text>().text = letter.ToString(); }
        }
    }
    private void CheckGameStatus()
    {
        string answer2 = "";
        for (int i = 0; i <= answerC.Length; i++)
        {
            if (letters[i].GetComponentInChildren<Text>().text != "")
            { answer2 += letters[i].GetComponentInChildren<Text>().text; }
        }
        if (answer2.Equals(answer))
        {
            NextWord("Победа!!!");
            player.rightAnswer++;
            UpdatePlayerText(player.rightAnswer, rightText);
        }
    }
    private void Lose()
    {
        NextWord("Они повесили Кучерявого Бобби!");
        player.wrongAnswer++;
        UpdatePlayerText(player.wrongAnswer, wrongText);
    }
    public void RestartGame()
    {
        for (int i = 0; i < mistakes.Length; i++)
        { mistakes[i].SetActive(false); }
        {
            for (int i = 0; i < letters.Length; i++)
            {
                letters[i].GetComponentInChildren<Text>().text = "";
                letters[i].SetActive(false);
            }
            gameOver.SetActive(false);
            keyboard.SetActive(true);
            endText.text = "";
        }
        GameStart();
        wordsList.Remove(randomWord);
    }
    private void UpdatePlayerText(int count, Text text)
    {
        text.text = count.ToString();
    }
    private void NextWord(string str)
    {
        gameOver.SetActive(true);
        keyboard.SetActive(false);
        endText.text = str;
     }

    private void InitWords()
    {
        for (int i = 0; i < words.gameWords.Length; i++)
        {
            wordsList.Add(words.gameWords[i]);
        }
    }
}
