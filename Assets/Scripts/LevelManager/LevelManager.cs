
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*Damayor - based on one of my projects used years ago*/
public class LevelManager : Singleton<LevelManager>
{

    private bool awaked = false; //:v

    private string instaceType;

    public event Action OnWin;
    public event Action OnLose;

    public int level;

    

    protected LevelManager() { } // guarantee this will be always a singleton only - can't use the constructor!


    //Se ejecuta siempre que carga escena
    void Awake()
    {
        //si no se ha creado 
        if (!awaked)
        {
            // Your initialization code here
            DontDestroyOnLoad(this.gameObject);
        }


        LevelManager[] lm = FindObjectsOfType(GetType()) as LevelManager[];

        if (lm.Length > 1)
        {
            //Soy el 2do porque el primero hizo que no entrara aca
            if (instaceType != "Instancia Original") //osea que es el nuevo, creado al comienzo del nivel
            {
                enabled = false;
                gameObject.SetActive(false);
                instaceType = "Nuevon";
                Destroy(this.gameObject); //ojo porque me activa applicationIsQuitting = false y despues ne lo devuelve nulo
            }


        }
        else //si es el original
        {
            instaceType = "Instancia Original";
            awaked = true;

            //ToDo change level
            level ++;
}
    }

    // Start is called before the first frame update
    void Start()
    {
        
        OnWin += Win;
        OnLose += Lose;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void someMethod()
    {
        Debug.Log("suscripcion a event");
    }


    public void Win()
    {
        Debug.Log("Event from LevelManager!");
        //if (OnWin != null)
        //    OnWin();
    }

    public void Lose()
    {
        Debug.Log("Event from LevelManager!");
        //if (OnWin != null)
        //    OnWin();
    }


}