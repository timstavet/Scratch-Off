using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneChanger : MonoBehaviour {

    private Animator animator;
    private int level;

    public void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void LoadLevel()
    {
        SceneManager.LoadScene(level, LoadSceneMode.Single);
    }

    public void FadeOut(int n)
    {
        animator.SetTrigger("Fade_Out");
        level = n;
    }

}
