using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class peluru : MonoBehaviour
{
    //1 tipe data transform ke variabel supaya engga ngutak ngatik si transformnya tinggal manggil 
    //parameter mytransform
    private Transform myTransform;

    //2 waktu terbang peluru
    public float waktu,temp;

    //3
    private float kecepatanAwal;
    //4
    private float sudutTembak;

    private TankBehaviour tankBehaviour;
    private AudioSource audioSource;
    private AudioClip audioLedakan;
    public GameObject trace;
    private GameObject ledakan;
    private Vector3 posisiAwal;
    private float sudutmeriam;
    private bool stop;

    public gamemanagerscript gameManager;

    

    

    // Start is called before the first frame update
    void Start()
    {
        //pastikan tank behaviour scrip ambil nilai dari tankbehaviour script yang ada di object putar
        tankBehaviour = GameObject.FindObjectOfType<TankBehaviour>();
        //1 inisialisasi Mytransform sebagai transform
        myTransform = transform;

        //3 akses ke tankbehaviour ambil fungsi fungsi untuk dibutuhkan oleh peluru
        kecepatanAwal  = tankBehaviour.kecepatanpeluru;
        sudutTembak = tankBehaviour.nilaiRotasiY;

        posisiAwal = myTransform.position;
        temp = 0.01f;
        sudutmeriam = tankBehaviour.sudutMeriam;
        stop = false;
        audioSource= GetComponent<AudioSource>();
        ledakan = tankBehaviour.objekLedakan;
        audioLedakan = tankBehaviour.audioLedakan;

        gameManager = GameObject.FindObjectOfType<gamemanagerscript>();

        
        
    }

    // Update is called once per frame
    void Update()
    {
            
        // myTransform.position = new Vector3(transform.position.x,transform.position.y,transform.position.z+kecepatanAwal);
        if(waktu >= temp )
            {
                temp += 0.1f;
                Instantiate(trace, transform.position, transform.rotation);
            }
        if ((transform.position.y >= -1) ){
            //timer time.deltaime waktu engine unity
                waktu += Time.deltaTime;
                gameManager._lamaWaktuTerbang = this.waktu;
                
                
            //myTransform.position = pergerakanPeluru(myTransform.position);
                
            //1 jadi posisi dia setiap di update maka nanti kita punya sebuah fungsi posisi di unity kan set vector 3
            //
                myTransform.position = pergerakanPeluru(waktu);
                
                
            }else{
                // stop = true;
            }
        if((stop == true))
            {
                
                stop = false;
                GameObject duar = Instantiate(ledakan,transform.position,Quaternion.identity);
                audioSource.PlayOneShot(audioLedakan);
                Destroy(gameObject,2f);
                Destroy(duar,2f);

                gameManager._JarakPeluru = Vector3.Distance(posisiAwal, myTransform.position);
                
            }
    }
 
    Vector3 pergerakanPeluru(float t)
    {
        // x
        float x = posisiAwal.x+(kecepatanAwal * t * Mathf.Sin(sudutmeriam * Mathf.PI /180));

        // y
        float y = ((kecepatanAwal * t * Mathf.Sin(sudutTembak*Mathf.PI /180)) - (0.5f * 9.8f * Mathf.Pow(t,2)))+posisiAwal.y;
            // float y = transform.position.y;
        // z
        float z = (kecepatanAwal * t * Mathf.Cos(sudutmeriam * Mathf.PI /180))+posisiAwal.z;
    
        return new Vector3(x, y, z);
    }
    private void OnTriggerEnter(Collider other) {
        if (other.tag == "Tanah"){
            stop = true;
        }
    }

}
