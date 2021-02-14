using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;

namespace CustomDLL
{ 
    public class AI : MonoBehaviour
    {
        private IntPtr m_AI = IntPtr.Zero;

        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Internal_CreateAI();
        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Internal_DestroyAI(IntPtr obj);
        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Internal_AI_GetBoard(IntPtr obj, string board);
        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern string Internal_AI_RandomMove(IntPtr obj, string move);
        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern string Internal_AI_SmartMove(IntPtr obj, string move);

        public void Destroy()
        {
            Debug.Log("AI Destroyed");
            if (m_AI != IntPtr.Zero)
            {
                Internal_DestroyAI(m_AI);
                m_AI = IntPtr.Zero;
            }
        }

        public void GetBoard(string board)
        {
            Debug.Log("AI Board Get");
            if (m_AI == IntPtr.Zero)
                throw new Exception("No native object");
            Internal_AI_GetBoard(m_AI, board);
        }

        public string RandomMove(string move)
        {
            Debug.Log("Random AI Move");
            if (m_AI == IntPtr.Zero)
                throw new Exception("No native object");
            return Internal_AI_RandomMove(m_AI, move);
        }

        public string SmartMove(string move)
        {
            Debug.Log("Smart AI Move");
            if (m_AI == IntPtr.Zero)
                throw new Exception("No native object");
            return Internal_AI_SmartMove(m_AI, move);
        }

        

        // Start is called before the first frame update
        void Start()
        {
            Debug.Log("AI Created");
            m_AI = Internal_CreateAI();
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void OnDestroy()
        {
            Destroy();
        }
    }
}


