using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class gamemanagerscript : MonoBehaviour
{
    public GameObject go_SudutMeriam;
    public GameObject go_SudutTembak;
    public GameObject go_Gravitasi;
    public GameObject go_KecepatanAwalPeluru;
    public GameObject go_WaktuTerbang;
    public GameObject go_JarakPeluru;
    public GameObject _Selongsong;
    public GameObject _putar;

    public float _lamaWaktuTerbang;
    public float _JarakPeluru;
    
    
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        go_SudutMeriam.GetComponent<Text>().text = 
        _putar.GetComponent<TankBehaviour>().sudutMeriam.ToString();

        go_SudutTembak.GetComponent<Text>().text = 
        _putar.GetComponent<TankBehaviour>().sudutTembak.ToString();

        go_Gravitasi.GetComponent<Text>().text = 
        _putar.GetComponent<TankBehaviour>().gravitasi.ToString();

        go_KecepatanAwalPeluru.GetComponent<Text>().text = 
        _putar.GetComponent<TankBehaviour>().kecepatanpeluru.ToString();

        go_WaktuTerbang.GetComponent<Text>().text = _lamaWaktuTerbang.ToString();

        go_JarakPeluru.GetComponent<Text>().text = _JarakPeluru.ToString();
        

    }
}
