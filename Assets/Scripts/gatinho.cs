using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;


public enum Savetype
{
    json,
    xml
}

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]

public class gatinho : MonoBehaviour
{
    Rigidbody2D Rigidbody2D;
    Animator animator;
    SpriteRenderer SpriteRenderer;
    public int movementSpeed;

    //public int collectedObjects = 0;

    public Savetype savetype;
    [SerializeField] private CollectableSpawner collectableSpawner;
    [SerializeField] private Transform bearTransform;
    [Header("Save Data")]
    [SerializeField] private GatinhoData gatinhodata;

    public GatinhoData GatinhoData => gatinhodata;
    public event Action<GatinhoData> OnGatinhoDataLoaded;

    void Awake()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        LoadData();
    }
    public void SaveData()
    {

        GatinhoData.storageData(transform.position, SpriteRenderer.flipX);
        GatinhoData.GafanhotoPositions = collectableSpawner.GafanhotosPositions();
        GatinhoData.BearPosition = bearTransform.position;

        if (savetype == Savetype.json)
        {
            string jsonToData = JsonUtility.ToJson(gatinhodata);
            //Debug.Log(jsonData);
            File.WriteAllText(Application.persistentDataPath + "/GatinhoData.json", jsonToData);
        }

        else if (savetype == Savetype.xml)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(GatinhoData));
            StreamWriter writer = new StreamWriter("gatinhodata.xml");
            serializer.Serialize(writer.BaseStream, gatinhodata);
            writer.Close();
        }
    }

    private void LoadData()
    {


        if (savetype == Savetype.xml)
        {
            try
            {
                XmlSerializer serializer = new XmlSerializer(typeof(GatinhoData));
                StreamReader reader = new StreamReader("gatinhodata.xml");
                gatinhodata = (GatinhoData)serializer.Deserialize(reader.BaseStream);
                reader.Close();
            } catch
            {

            }

        }

        else if (savetype == Savetype.json)
        {
            string dataFromJson = File.ReadAllText(Application.persistentDataPath + "/GatinhoData.json");
            gatinhodata = JsonUtility.FromJson<GatinhoData>(dataFromJson);
        }

        transform.position = gatinhodata.Position;
        SpriteRenderer.flipX = gatinhodata.Fliped;
        bearTransform.position = gatinhodata.BearPosition;
        

        OnGatinhoDataLoaded(gatinhodata);

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var horizontalInput = Input.GetAxisRaw("Horizontal");
        var verticalInput = Input.GetAxisRaw("Vertical");
        Vector2 movementInput = new Vector2(horizontalInput, verticalInput).normalized;
        Rigidbody2D.velocity = movementInput * movementSpeed * Time.fixedDeltaTime;

      
        animator.SetBool("velocityMag", Rigidbody2D.velocity.magnitude > 1);


        /////////////////////



    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            SpriteRenderer.flipX = false;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            SpriteRenderer.flipX = true;
        }




        if (Input.GetKeyDown(KeyCode.F2))
        {
            SaveData();



        }
    }


    //public void Pause()
    //{
    //    panelImage.SetActive(!panelImage.activeSelf);
    //    Time.timeScale = (Time.timeScale == 1) ? 0 : 1;

    //    //float targetTimeScale = 1-  Mathf.Abs(Mathf.Cos(Time.timeScale * Mathf.PI));
    //    //Time.timeScale = targetTimeScale;
    //}


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Collectable"))
        {
            collectableSpawner.RemoveGafanhoto(collision.gameObject);
            Destroy(collision.gameObject);
            //collectedObjects++;
            gatinhodata.IncreaseScore();
            
        }
    }
}