using System;

namespace CQ8Start
{
    internal class HealthPickup
    {
        //attribute variables to define the state of our health pick up
        private int _healthValue;


        //a constructor to set the default attribute values
        public HealthPickup()
        {
            _healthValue = 5;
        }


        //accessor methods to retrieve attribute values
        public int GetHealthValue() { return _healthValue; }


    }

}
