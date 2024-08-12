using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour
{
    Animator anim;
    Transform Player;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Player = GameObject.FindWithTag("Player").transform;
        transform.position = Player.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Player.position;
        anim.SetBool("Win", WinDoor.isWin);
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            PlayerMovement.isDed = false;
        }
    }
    public void HideTransition()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    public void ShowTransition()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    public void NextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerMovement.isDed = false;
        WinDoor.isWin = false;
    }

}
