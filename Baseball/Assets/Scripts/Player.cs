using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour {
        
    private int rnd;
    public GameObject porcentajeText;
    public GameObject hashText;
    public GameObject AIPlayer, Ball;
    public Button top, mid, bot;
    string opcion = "";
    Predictor predictor = new Predictor("tmb");
    string totalElecciones = "", eleccionesPredecir = "", eleccionesRegistrar = "";
    public int windowSize = 3;
    public int total = 0, acierto = 0;
    string prediccion;



    public void Top()
    {
        opcion = "t";
        Ball.GetComponent<Transform>().position = new Vector3(Ball.transform.position.x, top.transform.position.y, Ball.transform.position.z);
        IAPlay();

    }

    public void Mid()
    {
        opcion = "m";
        Ball.GetComponent<Transform>().position = new Vector3(Ball.transform.position.x, mid.transform.position.y, Ball.transform.position.z);
        IAPlay();
    }

    public void Bot()
    {
        opcion = "b";
        Ball.GetComponent<Transform>().position = new Vector3(Ball.transform.position.x, bot.transform.position.y, Ball.transform.position.z);
        IAPlay();
        
    }



    public void IAPlay()
    {
        //Desactiva los botones
        top.gameObject.SetActive(false);
        mid.gameObject.SetActive(false);
        bot.gameObject.SetActive(false);

        StartCoroutine(delayBall(1.5f, true));

        //Recoge el data del predictor para pintar la tabla hash
        Dictionary<string, DataRecord > data = predictor.GetDictionary();

            hashText.GetComponent<Text>().text = "Tabla Hash: \n\n";
            
            foreach (string key in data.Keys)
            {

            hashText.GetComponent<Text>().text = hashText.GetComponent<Text>().text + " " + key + "\n";
            DataRecord keyData = data[key];
                foreach (char action in keyData.counts.Keys)
                {
                hashText.GetComponent<Text>().text = hashText.GetComponent<Text>().text + "\t" + action + " -> " + keyData.counts[action] + "\n";
                
                }
            }

        
        //Recoge la prediccion del "predictor" en caso de que no haya informacion le manda un random para sacar una eleccion
        rnd = Random.Range(0, 3);
        prediccion = predictor.GetMostLikely(eleccionesPredecir, rnd);
        
        total++;

        //comprueba si la prediccion es igual a la jugada
        if (prediccion == opcion)
        {
            acierto++;
            Debug.Log("La IA ha acertado.");
            Ball.GetComponent<Animator>().SetInteger("State", 2);
            
        }
        else
        {
            Debug.Log("La IA ha fallado.");           
            Ball.GetComponent<Animator>().SetInteger("State", 1);   
        }

        totalElecciones += opcion;
         

        //Guarda la esleciones anteriores para la siguiente jugada
        if (totalElecciones.Length - windowSize < 0)                                                                
        {                                                                                                           
            eleccionesPredecir += opcion;
        }
        else
        {
            eleccionesPredecir = totalElecciones.Substring(totalElecciones.Length - windowSize);
        }




        //Guarda en el predictor la elección del jugador junto con las anteriores registradas
        if (totalElecciones.Length - windowSize - 1 < 0)
        {
            eleccionesRegistrar += opcion;
        }
        else
        {
            eleccionesRegistrar = totalElecciones.Substring(totalElecciones.Length - (windowSize + 1));
            predictor.RegisterSequence(eleccionesRegistrar);
        }

        //Muestra el porcentaje por pantalla
        float porcent = (float)(acierto * 100) / total;
        porcentajeText.GetComponent<Text>().text = "Porcentaje de acierto: " +porcent.ToString("0.00")+ "%";
        
        
    }
    public void getPredicction()
    {
        
       // Debug.Log("**************** " + predictor.GetMostLikely(eleccionesPredecir, rnd));        
        StartCoroutine(delayBall(0.4f, false));
        
    }
    public void resetBallState()
    {
        Ball.GetComponent<Animator>().SetInteger("State", 0);
    }

    IEnumerator delayBall(float timeDelay, bool boton)
    {

        yield return new WaitForSeconds(timeDelay);
        if (!boton)
        {            
            AIPlayer.GetComponent<animationController>().changeState(prediccion);           
        }
        else {
            top.gameObject.SetActive(true);
            mid.gameObject.SetActive(true);
            bot.gameObject.SetActive(true);
        }        
    }

    
}

