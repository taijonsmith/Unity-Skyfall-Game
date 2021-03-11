using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    private Image image;
    Color[] colors = new Color[3];
    GameObject platform_1, platform_2, platform_3, character;
    public Text score_text;
    Button start_game_button;
    int i;
    public int score = 0;
    bool on_platform;
    public AudioSource correct;
    public AudioSource incorrect;

    private void Start()
    {
        image = GameObject.Find("Canvas/Color Picker").GetComponent<Image>();
        start_game_button = GameObject.Find("Canvas/Start Game Button").GetComponent<Button>();
        start_game_button.interactable = true;
        colors[0] = Color.red;
        colors[1] = Color.green;
        colors[2] = Color.blue;
        platform_1 = GameObject.Find("Platforms/Platform16x01 (1)");
        platform_2 = GameObject.Find("Platforms/Platform16x01 (2)");
        platform_3 = GameObject.Find("Platforms/Platform16x01 (3)");
        character = GameObject.Find("2DCharacter");
        score_text = GameObject.Find("Canvas/Score").GetComponent<Text>();
        AudioSource[] allMyAudioSources = GetComponents<AudioSource>();
        correct = allMyAudioSources[0];
        incorrect = allMyAudioSources[1];
        Data.score = score;
    }

    private void Update()
    {
        on_platform = character.GetComponent<Animator>().GetBool("Ground") == true;
        if (on_platform)
        {
            start_game_button.interactable = true;
        }
        else
        {
            start_game_button.interactable = false;
        }
        score_text.text = "Score: " + score;
    }

    public void Play()
    {
        start_game_button.interactable = false;
        image.color = colors[Random.Range(0, colors.Length)];
        if (image.color == Color.red)
        {
            platform_2.SetActive(false);
            platform_3.SetActive(false);
        }
        else if (image.color == Color.green)
        {
            platform_1.SetActive(false);
            platform_3.SetActive(false);
        }
        else if (image.color == Color.blue)
        {
            platform_1.SetActive(false);
            platform_2.SetActive(false);
        }
        StartCoroutine(Waiting());
    }

    IEnumerator Waiting()
    {
        for (int i = 0; i < 5; i++)
        {
            yield return null;
        }
        if (on_platform)
        {
            Debug.Log("alive");
            correct.Play();
            score++;
            yield return new WaitForSeconds(1);
            //bring back platforms
            start_game_button.interactable = true;
            if (image.color == Color.red)
            {
                platform_2.SetActive(true);
                platform_3.SetActive(true);
            }
            else if (image.color == Color.green)
            {
                platform_1.SetActive(true);
                platform_3.SetActive(true);
            }
            else if (image.color == Color.blue)
            {
                platform_1.SetActive(true);
                platform_2.SetActive(true);
            }
            image.color = Color.white;
        }
        else
        {
            Debug.Log("dead");
            incorrect.Play();
        }
        Data.score = score;
    }
}
