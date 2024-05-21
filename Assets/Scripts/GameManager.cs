using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _txtScore;
    [SerializeField] TextMeshProUGUI _txtMultiplier;
    [SerializeField] RectTransform _gameOverPanel;
    Background _bg;
    int _score;
    [HideInInspector] int _scoreMultiplier = 1;
    Coroutine _scoreMultiplierCoroutine;
    Color colorOrange;

    void Start()
    {
        _gameOverPanel.gameObject.SetActive(false);
        _bg = GameObject.Find("Background").GetComponent<Background>();
        ColorUtility.TryParseHtmlString("#FF7F00", out colorOrange);
        _score = 0;
        ShowScore();
    }

    void Update()
    {

    }

    void ShowScore() 
    {
        _txtScore.text = "Score: " + _score;
        _txtMultiplier.text = " ";
        if (_scoreMultiplier > 1)
        {
            _txtMultiplier.text = _scoreMultiplier + "x";

            if (_scoreMultiplier < 10) _txtMultiplier.color = Color.yellow;
            else if (_scoreMultiplier >= 10 && _scoreMultiplier < 30) _txtMultiplier.color = colorOrange;
            else if (_scoreMultiplier >= 30) _txtMultiplier.color = Color.red;
        }
    }

    public void GameOver()
    {
        Time.timeScale = 0.0f;
        _bg._flowRate = 0.0f;
        _gameOverPanel.gameObject.SetActive(true);
    }

    public void IncreaseScore(Points point)
    {
        _score += (int)point * _scoreMultiplier;

        if (_scoreMultiplierCoroutine != null)
        {
            StopCoroutine(_scoreMultiplierCoroutine);
        }
        _scoreMultiplier++;
        ShowScore();
        _scoreMultiplierCoroutine = StartCoroutine(ResetScoreMultiplierCoroutine());
    }

    IEnumerator ResetScoreMultiplierCoroutine()
    {
        yield return new WaitForSeconds(2f);
        _scoreMultiplier = 1;
        ShowScore();
    }

    public void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void BackToMenu()
    {
        SceneManager.LoadSceneAsync("MainMenu");
        Time.timeScale = 1f;
    }
}
