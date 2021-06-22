using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBehaviour : MonoBehaviour
{
    // kenapa dia di public supaya bisa di akses sama si peluru untuk nilai tetanya
    // video 3 hide inspector adalah untuk tidak menampilkan di inspector si nilai rotasi Ynya tapi tetap bisa public 
    //karena nanti peluru keluar bedasarkan posisi si selongsong    
    
    //4 (V1) inisialisasi untuk parameter pembatas si arah selongsong (arah vertical)
        [HideInInspector]
        public float nilaiRotasiY;

        public float sudutMeriam;

    //1 (V1) jadi yang pertama di buat variabel myTransform tipe datanya Transform , 
        public Transform myTransform;

    //3 (V1) inisialisasi untuk parameter gameobjek selongsong
     private GameObject selongsong;

    //seri ke 2 (2)
        public GameObject pointer;
    //seri ke 2 (3)
    private AudioSource audioSource;

    //5 (V1) inisialisasi untuk parameter state dalam kondisi pembatas nilai untuk arah vertical state
        private string stateRotasiVertikal;
    // ketentunan kondisi state,
    // aman --> bisa dilakukan interaksi vertical atas bawah
    // bawah --> mentok ke bawah mendekati 0 (nilaiRotasiY),, harus direset ke -0,5
    // atas --> mentok atas mendekati 15 (nilaiRotasiY) ,, harus di reset ke -14,5 

    //2 (V1) inisialisasi parameter kecepatan rotasi untuk putar meriam
    public float kecepatanRotasi = 20;

    //video 3 baut inisialisasi kecepatan awal peluru
        public float kecepatanpeluru = 20;
        public float gravitasi = 10;





    //1 (V2) inisialisasi objek 
        public GameObject peluruMeriam;
        public GameObject objekTembakan;
        public GameObject objekLedakan;

        public AudioClip audioTembakan;
        public AudioClip audioLedakan;
        public float sudutTembak;
        

        




    // Start is called before the first frame update
    void Start()
    {
       //1 (V1)  
        //transform adalah bawaan dari unity, jadi dia merefrenses gameobjek tersebut dari sisi rotasi , posisi dan scale dan nanti kita bisa update dari mytransform
        //inisialisasi parameter myTransform adalah sebagai transform dari objek putar
        myTransform = transform;


       //2 (V1) inisialisasi , -->untuk menemukan objek selongsong di myTransform (putar) , dan selongsong ini merupakan childnya , jadi di panggil dulu 
        selongsong = myTransform.Find("selongsong").gameObject;

       //3 (V1) inisialisasi , -->untuk menemukan objek pointer di myTransform (selongsong) , dan pointer ini merupakan childnya si selongsong , jadi di panggil dulu 
        pointer = selongsong.transform.Find("pointer").gameObject;






        //seri ke 2 inisialisasi audio tembakan
        //parameter audiosource yang di temukan di gameobjek selongsong.
        audioSource = selongsong.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //1 (V1) untuk insisialisasi get key untuk dapat mengfungsikan putar meriam dengan vektor 3 melihat dari sumbu z yang berubah , dan spacenya bedasarkan self.
        #region rotasi horizontal
        //1 (V1) untuk insisialisasi get key untuk dapat mengfungsikan putar meriam dengan vektor 3 melihat dari sumbu z yang berubah , dan spacenya bedasarkan self.
        if (Input.GetKey(KeyCode.T)){
            myTransform.Rotate(Vector3.back * kecepatanRotasi * Time.deltaTime, Space.Self);
        }
        if (Input.GetKey(KeyCode.U)){
            myTransform.Rotate(Vector3.forward * kecepatanRotasi * Time.deltaTime, Space.Self);
        }
        #endregion

        //2 inisialisasi paramter untuk sebagai nilai pembatas arah vertical pada si selongsong, mengikuti x dan 360 - dengan x artinya dapat nilai 15 
        nilaiRotasiY = 360 - selongsong.transform.localEulerAngles.x;

        //3 (V1) untuk membatasi arah pergerakan vertical // arah rotasi vertical dari 0 -15 
        #region Menentukan State
        if (nilaiRotasiY == 0 || nilaiRotasiY ==360){
            stateRotasiVertikal = "aman";
        }
        else if (nilaiRotasiY > 0 && nilaiRotasiY < 15){
            stateRotasiVertikal = "aman";
        }
        else if (nilaiRotasiY > 15 && nilaiRotasiY < 16){
            stateRotasiVertikal = "atas";
        }
        else if (nilaiRotasiY > 350 ){
            stateRotasiVertikal = "bawah";
        }
        #endregion

        //4 (V1) untuk membatasi arah pergerakan vertical // arah rotasi vertical dari 0 -15  
        #region arah rotasi vertikal berdasarkan state
        if (stateRotasiVertikal == "aman"){
            if(Input.GetKey(KeyCode.Y)){
                selongsong.transform.Rotate(Vector3.left * kecepatanRotasi * Time.deltaTime,Space.Self);
            }else if(Input.GetKey(KeyCode.H)){
                selongsong.transform.Rotate(Vector3.right * kecepatanRotasi * Time.deltaTime,Space.Self);
            }
        }

        //4 (V1)  artinya jika kondisi statenya bawah , maka yang bergerak ke bawah adalah 0.5fke bawah dengan otomatis reset jika masuk kek konndisi itu
        else if (stateRotasiVertikal == "bawah"){
            selongsong.transform.localEulerAngles = new Vector3(
                -0.5f,
                selongsong.transform.localEulerAngles.y
                ,selongsong.transform.localEulerAngles.z);
        }
        //4 (V1)  untuk -14,5 karena negatif posisinya ke atas
        else if (stateRotasiVertikal == "atas"){
            selongsong.transform.localEulerAngles = new Vector3(
                -14.5f,
                selongsong.transform.localEulerAngles.y,
                selongsong.transform.localEulerAngles.z);
        }
        #endregion
        
        
        //video 3 bagain insialisasi sudut meriam biar peluru gerak secara horizontal
        sudutMeriam = myTransform.localEulerAngles.z;
        sudutTembak = nilaiRotasiY;
       

        //video 2 bagian (1) kenapa ada game objek peluru , supaya ya pengen aja di wakilin sama variabel biar enak kalau 
        // ada insialisasi buar si instantiate pelurunya , pointer artinya titik tembakan 
        //trus quartenio buat arah si pelurunya itu kemana kan , nah itu tergantung sama koordinat si selongsong x,z,y=0
        //kenapa pake quartenion euler ? supaya bisa di atur sama koordinatnya , identitity jadi kordinat 0,0,0
        //untuk yang z nya ngikutin yang (putar) kalau yang x nya ngikutin selongsong karena kan vertical ke atas tea
        if(Input.GetKeyDown(KeyCode.Space)){
            GameObject peluru = Instantiate(peluruMeriam,pointer.transform.position,
            Quaternion.Euler(selongsong.transform.localEulerAngles.x,myTransform.localEulerAngles.z,0));
        
        //untuk inisialisasi efek tembakan pada selongsong untuk koordinat sama kaya peluru karena ngikutin si selongsong
        GameObject efekTembakan = Instantiate(objekTembakan,pointer.transform.position,
            Quaternion.Euler(selongsong.transform.localEulerAngles.x,myTransform.localEulerAngles.z,0));

        //untuk menghapus objek yang muncul dari efek tembakan dengan waktu hancur 
        //lama waktu yang diberikan menahan sebelum objek di destroy
        Destroy(efekTembakan,1f);
        
            //untuk inisialisasi audio , di main kamera bisa di kustomisasi karena ada audiolistener di dalam cameranya
        audioSource.PlayOneShot(audioTembakan);
        }
    }
}
