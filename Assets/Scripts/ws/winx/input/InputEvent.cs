﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;
using System.Threading;

namespace ws.winx.input
{
    public class InputEvent
    {



        protected int _stateNameHash;
     
        //public static delegate bool GetInputDelegate(int stateNameHash,bool atOnce);
		protected static Func<int,bool,bool> _action;

 


        public InputEvent(int stateNameHash)
        {
            _stateNameHash = stateNameHash;

           
        }

        public InputEvent(string stateName):this(Animator.StringToHash(stateName))
        {
          
        }


       



        private  Dictionary<int, Delegate[]> __events;
        public  Dictionary<int, Delegate[]> Events
        {
            get
            {
                if (__events == null)
                    __events = new Dictionary<int, Delegate[]>();
                return __events;
            }

        }

        //TODO optimize this
        public event EventHandler UP
        {
            add
            {
                AddHandler(_stateNameHash, value,1);

            }
            remove
            {
                RemoveHandler(_stateNameHash, value,1);
            }
        }



        public event EventHandler DOWN
        {
            add
            {
                AddHandler(_stateNameHash, value,2);

              
            }
            remove
            {
                RemoveHandler(_stateNameHash, value,2);
            }
        }


       



        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="inx">0-Continuios events,1-Up,2-Down events</param>
        protected void AddHandler(int key, Delegate value,uint inx)
        {

            Delegate[] d;


            if (Events.TryGetValue(key, out d))
            {
                if (d[inx] != null)
                    d[inx] = Delegate.Combine(d[inx], value);
                else
                    Events[key][inx] = value;
            }
            else
            {
                Events[key]=new Delegate[3];
                Events[key][inx] = value;

            }




        }


        protected void RemoveHandler(int key, Delegate value,uint inx)
        {
            Delegate[] d;

            if (Events.TryGetValue(key, out d))
            {
                Events[key][inx] = Delegate.Remove(d[inx], value);
            }
            // else... no error for removal of non-existant delegate
            //
        }




        public void Dispose()
        {
       

            if (this.Events != null)
            {

                this.Events.Remove(_stateNameHash);
           
            }

            
        }



      
    }
}
