using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using UnityEngine;

public class Predictor
{
    
        private Dictionary<string, DataRecord> data;
        private string possibleActions;        

        public Predictor(string _possibleActions)
        {            
            possibleActions = _possibleActions;
            data = new Dictionary<string, DataRecord>();
        }

        //Devuelve la eleccion mas probable segun las acciones anteriores, en caso de que no existan devuelve una accioin random
        public string GetMostLikely(string actions, int rndNum)
        {
         
            DataRecord keyData;
            int highestValue = 0;
            char bestAction = ' ';

            if (data.ContainsKey(actions))
            {
                keyData = data[actions];

                foreach (char action in keyData.counts.Keys)
                {
                    if (keyData.counts[action] > highestValue)
                    {
                        highestValue = keyData.counts[action];
                        bestAction = action;
                    }
                }
            }
            else
            {
            if(rndNum == 0)
                bestAction = 't';
            else if(rndNum == 1)
                bestAction = 'm';
            else if (rndNum == 2)
                bestAction = 'b';
        }

            return bestAction + "";
        }

        //Guarda un registro de todas las secuencias realizadas durante la partida
        public void RegisterSequence(string actions)
        {
            string key = actions.Substring(0, actions.Length - 1);
            char value = actions[actions.Length - 1];

            if (!data.ContainsKey(key))
            {
                data[key] = new DataRecord();
            }

            DataRecord keyData = data[key];

            if (!keyData.counts.ContainsKey(value))
            {
                keyData.counts[value] = 0;
            }

            keyData.counts[value]++;
            keyData.total++;
        }

       /* public void PrintData()
        {
            //Console.WriteLine("PREDICTOR DATA");
            foreach (string key in data.Keys)
            {
                //Debug.Log(key);
                DataRecord keyData = data[key];
                foreach (char action in keyData.counts.Keys)
                {
                    Debug.Log("\t" + action + " -> " + keyData.counts[action]);
                }
            }
        }
        */

        public Dictionary<string, DataRecord> GetDictionary()
         {
             return data;
         }

        
        
    
}
