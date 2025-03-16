using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace CrypticCatacombs
{
    public class CustomTimer
    {
        public bool gameStarted;
        protected int mSec;
        protected TimeSpan timer = new TimeSpan();
        

        public CustomTimer(int m)
        {
			gameStarted = false;
			mSec = m;
        }
        public CustomTimer(int m, bool STARTLOADED)
        {
			gameStarted = STARTLOADED;
			mSec = m;
        }

        public int MSec
		{
            get { return mSec; }
            set { mSec = value; }
        }
        public int Timer
        {
            get { return (int)timer.TotalMilliseconds; }
        }

        

        public void UpdateTimer()
        {
            timer += Globals.gameTime.ElapsedGameTime;
        }

        //function to speed up or slowdown time "SPEED"
        public void UpdateTimer(float SPEED)
        {
            timer += TimeSpan.FromTicks((long)(Globals.gameTime.ElapsedGameTime.Ticks * SPEED));
        }

        //keeps track of timers if used in game (abilities)
        public virtual void AddToTimer(int MSec)
        {
            timer += TimeSpan.FromMilliseconds((long)(MSec));
        }

        public bool Test()
        {
            if(timer.TotalMilliseconds >= mSec || gameStarted)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void Reset()
        {
            timer = timer.Subtract(new TimeSpan(0, 0, mSec / 60000, mSec / 1000, mSec % 1000));
            if(timer.TotalMilliseconds < 0)
            {
                timer = TimeSpan.Zero;
            }
			gameStarted = false;
        }

        public void Reset(int NEWTIMER)
        {
            timer = TimeSpan.Zero;
			MSec = NEWTIMER;
			gameStarted = false;
        }

        //main function to reset timer
        public void ResetToZero()
        {
            timer = TimeSpan.Zero;
			gameStarted = false;
        }

        public virtual XElement ReturnXML()
        {
            XElement xml= new XElement("Timer",
                                    new XElement("mSec", mSec),
                                    new XElement("timer", Timer));



            return xml;
        }

        public void SetTimer(TimeSpan TIME)
        {
            timer = TIME;
        }

        public virtual void SetTimer(int MSec)
        {
            timer = TimeSpan.FromMilliseconds((long)(MSec));
        }
    }
}
