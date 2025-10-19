using System;


namespace CQ8Start
{
    internal class Gremlin
    {
        //attribute variables to define the state of our hero
        private int _gremlinHealth;


        //a constructor to set the default attribute values
        public Gremlin(int gremlinHealth)
        {
            _gremlinHealth = gremlinHealth; 
        }

        //accessor methods to retrieve attribute values
        public int GetHealth() { return _gremlinHealth; }


        //mutator methods to change attribute values
        public void TakeDamage(int damageAmount) { _gremlinHealth -= damageAmount; }


        //helper methods that don’t affect attribute values – this
        //method uses a Random object to return a random gremlin damage value
        //between 2 and 4.
        public int DealDamage() { Random rng = new Random(); return rng.Next(2, 5); }

    }

}
