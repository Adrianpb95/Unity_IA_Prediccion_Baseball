using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

using UnityEngine;


public class DataRecord
{

    
        public Dictionary<char, int> counts; 
        public int total; 

        public DataRecord() 
        {
            total = 0; 
            counts = new Dictionary<char, int>(); 
    }
    
}
