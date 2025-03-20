using NUnit.Framework;
using TMPro;
using UnityEngine;

public class StoneStepping : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject[] letterStonesPrefabs;

    [SerializeField]
    GameObject wordCanvas;

    [SerializeField]
    private float startX;
    [SerializeField]
    private float startY;
    [SerializeField]
    private float endX;
    [SerializeField]
    private float endY;

    private string[] words = { "PAN", "ANT" };

    private string word;
    private GameObject currLetterStones;

    private int currWordInd = 0;
    private int currLetterInd = 0;

    private void Start()
    {
        StartNewLevel();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var rayHit = Physics2D.GetRayIntersection(ray);

            if (rayHit.collider)
            {
                if (currLetterInd == word.Length && rayHit.collider.gameObject.tag == "Grass")
                {
                    player.transform.position = new Vector3(endX, endY, player.transform.position.z);
                    currWordInd = (currWordInd + 1) % words.Length;

                    if (currWordInd < words.Length)
                    {
                        Destroy(currLetterStones);
                        currLetterInd = 0;
                        StartNewLevel();
                    }
                }
                else
                {
                    GameObject stone = rayHit.collider.gameObject;
                    char stoneText = stone.GetComponentInChildren<TextMeshProUGUI>().text[0];

                    if (currLetterInd < word.Length && stoneText == word[currLetterInd])
                    {
                        player.transform.position = new Vector3(stone.transform.position.x, stone.transform.position.y + 1.0f, stone.transform.position.z);
                        currLetterInd += 1;
                    }
                    else
                    {
                        player.transform.position = new Vector3(startX, startY, player.transform.position.z);
                        currLetterInd = 0;
                    }
                }
            }    
        }
    }

    private void StartNewLevel()
    {
        word = words[currWordInd];
        wordCanvas.GetComponentInChildren<TextMeshProUGUI>().text = word;

        currLetterStones = Instantiate(letterStonesPrefabs[currWordInd]);

        player.transform.position = new Vector3(startX, startY, player.transform.position.z);
    }
}
