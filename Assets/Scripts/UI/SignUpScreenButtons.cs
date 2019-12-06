using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Security.Cryptography;
using System.Text;


public class SignUpScreenButtons : MonoBehaviour
{
    [SerializeField] private GameObject LoginScreen;
    [SerializeField] private GameObject parent;
    [SerializeField] private NetworkClient networkClient;

    [Header("Buttons")]
    [SerializeField] private TMPro.TMP_InputField email;
    [SerializeField] private TMPro.TMP_InputField password;
    [SerializeField] private TMPro.TMP_InputField username;
    [Header("Popup")]
    [SerializeField] private GameObject popup;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void onClickMain()
    {
        string _password = sha256(password.text);
        networkClient.SignUp(email.text, username.text, sha256(password.text));
        Debug.Log("Hashed Password: " + _password);
    }

    public void onClickLogin()
    {
        LoginScreen.SetActive(true);
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
