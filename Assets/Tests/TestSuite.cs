using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;

public class TestSuite
{
    private GameObject gameManagerObj;
    private GameManager gameManager;

    [SetUp]
    public void Setup()
    {
        PlayerPrefs.DeleteKey("PlayerScore");

        gameManagerObj = new GameObject();
        gameManager = gameManagerObj.AddComponent<GameManager>();

        var timerObj = new GameObject();
        gameManager.timerText = timerObj.AddComponent<UnityEngine.UI.Text>();

        var countdownObj = new GameObject();
        gameManager.countDownTxt = countdownObj.AddComponent<UnityEngine.UI.Text>();

        var scoreManagerObj = new GameObject("ScoreManager");
        scoreManagerObj.AddComponent<ScoreManager>();
    }

    [TearDown]
    public void Teardown()
    {
        Object.DestroyImmediate(gameManagerObj);
    }

    [Test]
    public void TimerStartsAtZero()
    {
        Assert.AreEqual(0f, gameManager.GetElapsedTime());
    }

    [UnityTest]
    public IEnumerator TimerCountsUpOverTime()
    {
        var timerRoutine = gameManager.StartCoroutine("GameTimer");

        yield return new WaitForSeconds(1f);

        Assert.Greater(gameManager.GetElapsedTime(), 0f);

        gameManager.StopCoroutine(timerRoutine);
    }

    [UnityTest]
    public IEnumerator TimerStopsOnGameOver()
    {
        var timerRoutine = gameManager.StartCoroutine("GameTimer");

        yield return new WaitForSeconds(0.5f);

        gameManager.TriggerGameOver();
        float timeAtGameOver = gameManager.GetElapsedTime();

        yield return new WaitForSeconds(1f);

        Assert.AreEqual(timeAtGameOver, gameManager.GetElapsedTime());
    }

    [UnityTest]
    public IEnumerator TimerStopsOnWinScene()
    {
        var timerRoutine = gameManager.StartCoroutine("GameTimer");

        yield return new WaitForSeconds(0.5f);

        gameManager.TriggerWinScene();
        float timeAtWin = gameManager.GetElapsedTime();

        yield return new WaitForSeconds(1f);

        Assert.AreEqual(timeAtWin, gameManager.GetElapsedTime());
    }

    public class ScoreManagerTests
    {
        private GameObject scoreManagerObj;
        private ScoreManager scoreManager;

        [SetUp]
        public void Setup()
        {
            PlayerPrefs.DeleteKey("PlayerScore");

            scoreManagerObj = new GameObject("ScoreManager");
            scoreManager = scoreManagerObj.AddComponent<ScoreManager>();
        }

        [TearDown]
        public void Teardown()
        {
            Object.DestroyImmediate(scoreManagerObj);
        }

        [Test]
        public void ScoreStartsAtZero()
        {
            Assert.AreEqual(0, scoreManager.GetScore());
        }

        [Test]
        public void AddPointIncreasesScore()
        {
            scoreManager.AddPoint();
            Assert.AreEqual(1, scoreManager.GetScore());
        }

        [Test]
        public void ResetScoreSetsToZero()
        {
            scoreManager.AddPoint();
            scoreManager.AddPoint();
            Assert.AreEqual(2, scoreManager.GetScore());

            scoreManager.ResetScore();
            Assert.AreEqual(0, scoreManager.GetScore());
        }

        [Test]
        public void ScorePersistsInPlayerPrefs()
        {
            scoreManager.AddPoint();
            scoreManager.AddPoint();
            PlayerPrefs.Save();

            Object.DestroyImmediate(scoreManagerObj);
            scoreManagerObj = new GameObject("ScoreManager");
            scoreManager = scoreManagerObj.AddComponent<ScoreManager>();

            Assert.AreEqual(2, scoreManager.GetScore());
        }
    }
}