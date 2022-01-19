using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Statistics
{
    public class DataRecord
    {
        public float time;
        public float money;
        public int units;
        
        public DataRecord(float time, float money, int units)
        {
            this.time = time;
            this.money = money;
            this.units = units;
        }
    }
}

