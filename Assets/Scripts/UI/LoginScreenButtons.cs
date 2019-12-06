using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using SocketIO;
using UnityEngine.UI;
using System.Security.Cryptography;

public class LoginScreenButtons : MonoBehaviour
{
    [SerializeField] private GameObject SignUpScreen;
    [SerializeField] private GameObject parent;
    [SerializeField] private NetworkClient networkClient;

    [Header("Buttons")]
    [SerializeField] private TMPro.TMP_InputField email;
    [SerializeField] private TMPro.TMP_InputField password;
    [Header("Popup")]
    [SerializeField] private GameObject popup;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void failedToLogin()
    {
        popup.SetActive(true);
    }

    public void onClickMain()
    {
        string _password = sha256(password.text);
        networkClient.Login(email.text, sha256(password.text));
        Debug.Log("Hashed Password: " + _password);
    }

    // Update is called once per frame
    public void onClickSignUp()
    {
        SignUpScreen.SetActive(true);
        parent.SetActive(false);
    }

    public static string sha256(string str)
    {
        SHA256Managed crypt = new SHA256Managed();
        StringBuilder hash = new StringBuilder();
        byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(str), 0, Encoding.UTF8.GetByteCount(str));
        foreach (byte bit in crypto)
        {
            hash.Append(bit.ToString("x2"));
        }
        return hash.ToString().ToLower();
    }
}
