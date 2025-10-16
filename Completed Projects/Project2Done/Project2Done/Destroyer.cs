using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Project2Done
{
    internal class Destroyer
    {
        //this is an unusual "helper" class. We won't see it at all in the game
        //but it simply moves up and down to "carve" a path in the wall of obstacles.

        //coordinates and a speed
        private float _destroyerX, _destroyerY, _destroyerSpeed;

        //a boolean to know if we're going up or down
        private bool _detroyerMovingDown;

        //argumented constructor (a zero arg one doesn't make sense for this object)
        public Destroyer(float destroyerX, float destroyerY, float destroyerSpeed)
        {
            //set all the attributes!
            _destroyerX = destroyerX;
            _destroyerY = destroyerY;
            _destroyerSpeed = destroyerSpeed;

            //hard code this because it doesn't matter too much, but you could set it with an argument
            //if you wanted.
            _detroyerMovingDown = true;
        }

        //looks complex, but this simply says to change from up to down when you get to the
        //edge of the screen. If we're going up, subtract from myY, if we're going down
        //add to myY. The destroy ONLY moves up and down. 
        public void Update()
        {
            if( _destroyerY > 600 ) { _detroyerMovingDown = false; }
            if( _destroyerY < 0) { _detroyerMovingDown = true; }

            if( _detroyerMovingDown ) { _destroyerY += _destroyerSpeed; }
            else _destroyerY -= _destroyerSpeed;
        }

        //these two methods will help us with collision dection between the obstacles and the
        //destroyer object
        public float getX() { return _destroyerX; }
        public float getY() { return _destroyerY; }

    }
}
