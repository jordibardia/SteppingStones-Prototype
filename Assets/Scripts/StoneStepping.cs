using System.Net;
using TMPro;
using UnityEngine;

public class StoneStepping : MonoBehaviour
{

    [SerializeField]
    GameObject player;

    [SerializeField]
    private float startX;
    [SerializeField]
    private float startY;
    [SerializeField]
    private float endX;
    [SerializeField]
    private float endY;

    private string word = "PAN";
    private int currInd = 0;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            var rayHit = Physics2D.GetRayIntersection(ray);

            if (rayHit.collider)
            {
                if (currInd == word.Length && rayHit.collider.gameObject.tag == "Grass")
                {
                    player.transform.position = new Vector3(endX, endY, player.transform.position.z);
                }
                else
                {
                    GameObject stone = rayHit.collider.gameObject;
                    char stoneText = stone.GetComponentInChildren<TextMeshProUGUI>().text[0];

                    if (currInd < word.Length && stoneText == word[currInd])
                    {
                        player.transform.position = stone.transform.position;
                        currInd += 1;
                    }
                    else
                    {
                        player.transform.position = new Vector3(startX, startY, player.transform.position.z);
                        currInd = 0;
                    }
                }
            }    
        }
    }
}
