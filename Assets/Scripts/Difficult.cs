using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Difficult : MonoBehaviour
{
    public static Difficult instance;
    public TMP_InputField widthInput, heightInput;
    public int difficult;
    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        width = 18;
        height = 18;
    }

    void Update()
    {

    }

    public void DifficultButton(int index)
    {
        difficult = index;
        width = int.Parse(widthInput.text);
        height = int.Parse(heightInput.text);

        SceneManager.LoadSceneAsync(2);
    }

    public int width;
    public int height;

    public void CancelButton()
    {
        SceneManager.LoadScene(0);
    }
}
