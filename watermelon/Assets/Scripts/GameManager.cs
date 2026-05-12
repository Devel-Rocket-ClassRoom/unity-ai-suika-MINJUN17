using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("Container")]
    public float minX = -3.8f;
    public float maxX =  3.8f;
    public float dropY = 4.5f;

    [Header("UI")]
    public Text scoreText;
    public GameObject gameOverPanel;
    public Text gameOverScoreText;

    int score;
    int nextLevel;
    bool canDrop = true;
    bool isGameOver;
    GameObject previewObj;

    void Awake() => Instance = this;

    void Start()
    {
        nextLevel = Random.Range(0, FruitSpawner.Instance.MaxDropLevel + 1);
        RefreshPreview();
    }

    void Update()
    {
        if (isGameOver) return;

        var mouse = Mouse.current;
        if (mouse == null) return;

        Vector2 mpos = mouse.position.ReadValue();
        float x = Mathf.Clamp(
            Camera.main.ScreenToWorldPoint(new Vector3(mpos.x, mpos.y, 0)).x,
            minX, maxX);

        if (previewObj != null)
            previewObj.transform.position = new Vector3(x, dropY, 0);

        if (mouse.leftButton.wasPressedThisFrame && canDrop)
        {
            canDrop = false;
            if (previewObj != null) { Destroy(previewObj); previewObj = null; }

            FruitSpawner.Instance.Spawn(nextLevel, new Vector2(x, dropY));
            nextLevel = Random.Range(0, FruitSpawner.Instance.MaxDropLevel + 1);

            Invoke(nameof(RefreshPreview), 0.5f);
            Invoke(nameof(EnableDrop),    0.5f);
        }
    }

    void EnableDrop() => canDrop = true;

    void RefreshPreview()
    {
        if (previewObj != null) Destroy(previewObj);
        previewObj = FruitSpawner.Instance.Spawn(nextLevel, new Vector2(0, dropY), isPreview: true);
    }

    public void SpawnMerged(int level, Vector2 pos)
    {
        score += FruitSpawner.Instance.fruitDataList[level].score;
        if (scoreText != null) scoreText.text = "Score: " + score;
        FruitSpawner.Instance.Spawn(level, pos);
    }

    public void AddScore(int amount)
    {
        score += amount;
        if (scoreText != null) scoreText.text = "Score: " + score;
    }

    public void TriggerGameOver()
    {
        if (isGameOver) return;
        isGameOver = true;
        canDrop    = false;
        if (previewObj != null) { Destroy(previewObj); previewObj = null; }
        if (gameOverScoreText != null) gameOverScoreText.text = "Score: " + score;
        if (gameOverPanel    != null) gameOverPanel.SetActive(true);
    }

    public void Restart() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
}
