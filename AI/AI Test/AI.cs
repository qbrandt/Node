using System.Collections;
using System.Collections.Generic;
using System;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace AI_Test
{
    public class AI
    {
        private IntPtr m_AI = IntPtr.Zero;

        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Internal_CreateAI();
        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Internal_DestroyAI(IntPtr obj);
        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Internal_AI_GameSetup(IntPtr obj, string board, bool firstMove, bool isSmart);
        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern string Internal_AI_GetMove(IntPtr obj, string move);

        public void Destroy()
        {
            Debug.WriteLine("AI Destroyed");
            if (m_AI != IntPtr.Zero)
            {
                Internal_DestroyAI(m_AI);
                m_AI = IntPtr.Zero;
            }
        }

        public void GameSetup(string board, bool goesFirst, bool isSmart = true)
        {
            Debug.WriteLine("AI Board Get");
            if (m_AI == IntPtr.Zero)
                throw new Exception("No native object");
            Internal_AI_GameSetup(m_AI, board, goesFirst, isSmart);
        }

        public string GetMove(string move)
        {
            Debug.WriteLine("Random AI Move");
            if (m_AI == IntPtr.Zero)
                throw new Exception("No native object");
            return Internal_AI_GetMove(m_AI, move);
        }


        // Start is called before the first frame update
        public AI()
        {
            Debug.WriteLine("AI Created");
            m_AI = Internal_CreateAI();
        }


        ~AI()
        {
            Destroy();
        }
    }
}