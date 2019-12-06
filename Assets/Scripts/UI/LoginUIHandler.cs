using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginUIHandler : MonoBehaviour
{

    [Header("GameObjects for the login UI")]
    [SerializeField] private GameObject email;
    [SerializeField] private GameObject password;
    [SerializeField] private GameObject login;


    private InputField emailInputField;
    private InputField passwordInputField;
    private Button loginButton;
    // Start is called before the first frame update
    void Start()
    {
        emailInputField = email.GetComponent<InputField>();
        passwordInputField = password.GetComponent<InputField>();
        loginButton = login.GetComponent<Button>();
    }

    // Update is called once per frame
    //void Update()
    //{
    //    if(mouseOver)
    //}


    //private bool mouseOver()
    //{
    //    return EventSystem.current.IsPointerOverGameObject();
    //}

}
