using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using Unity.Jobs;
using Unity.Collections;

namespace CustomDLL
{ 
    public class AI : MonoBehaviour
    {
        public JobHandle MakeMoveHandle { get; set; }
        private MakeMoveJob _makeMoveJob;

        struct MakeMoveJob : IJob
        {
            public IntPtr AiPtr { get; set; }
            
            public string PlayerMove { get; set; }

            public string AiMove { get; set; }

            public void Execute()
            {
                AiMove = Internal_AI_GetMove(AiPtr, PlayerMove);
            }
        }


        private IntPtr m_AI = IntPtr.Zero;

        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr Internal_CreateAI();
        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Internal_DestroyAI(IntPtr obj);
        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern void Internal_AI_GameSetup(IntPtr obj, string board, bool firstMove, bool isSmart);
        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern string Internal_AI_GetMove(IntPtr obj, string move);
        [DllImport("AI", CallingConvention = CallingConvention.Cdecl)]
        private static extern string Internal_AI_View(IntPtr obj);

        public void Destroy()
        {
            Debug.Log("AI Destroyed");
            if (m_AI != IntPtr.Zero)
            {
                Internal_DestroyAI(m_AI);
                m_AI = IntPtr.Zero;
            }
        }

        public void GameSetup(string board, bool goesFirst, bool isSmart = true)
        {
            Debug.Log("AI Board Get");
            if (m_AI == IntPtr.Zero)
                throw new Exception("No native object");
            Internal_AI_GameSetup(m_AI, board, goesFirst, isSmart);
        }

        public void MakeMove(string move)
        {
            Debug.Log($"Move given: {move}");

            if (m_AI == IntPtr.Zero)
                throw new Exception("No native AI object");

            _makeMoveJob = new MakeMoveJob
            { 
                AiPtr = m_AI,
                PlayerMove = move,
                AiMove = ""
            };

            MakeMoveHandle = _makeMoveJob.Schedule();

        }


        public string GetMove()
        {
            if(!MakeMoveHandle.IsCompleted)
                throw new Exception("Move not complete yet");
            return _makeMoveJob.AiMove;
        }


        public string View()
        {
            Debug.Log("VIEW AI");
            if (m_AI == IntPtr.Zero)
                throw new Exception("No native object");
            var response = Internal_AI_View(m_AI);
            return response;
        }

        void Awake()
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


