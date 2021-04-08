using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using Unity.Jobs;
using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using System.Text;

namespace CustomDLL
{ 
    public class AI : MonoBehaviour
    {
        public JobHandle MakeMoveHandle { get; set; }
        private MakeMoveJob _makeMoveJob;

        struct MakeMoveJob : IJob
        {
            
            [NativeDisableUnsafePtrRestriction]
            public IntPtr AiPtr;
            [ReadOnly]
            public NativeArray<byte> PlayerMove;

            public NativeArray<byte> AiMove;

            public void Execute()
            {
                var result = Internal_AI_GetMove(AiPtr, Encoding.ASCII.GetString(PlayerMove.ToArray()));

                AiMove = new NativeArray<byte>(result.Length, Allocator.Temp);

                AiMove.CopyFrom(Encoding.ASCII.GetBytes(result));
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

            var nativeMove = new NativeArray<byte>(move.Length,Allocator.TempJob);
            nativeMove.CopyFrom(Encoding.ASCII.GetBytes(move));

            var nativeAiMove = new NativeArray<byte>("".Length, Allocator.TempJob);
            nativeAiMove.CopyFrom(Encoding.ASCII.GetBytes(""));

            _makeMoveJob = new MakeMoveJob
            {
                AiPtr = m_AI,
                PlayerMove = nativeMove,
                AiMove = nativeAiMove
            };

            MakeMoveHandle = _makeMoveJob.Schedule();

        }


        public string GetMove()
        {
            if(!MakeMoveHandle.IsCompleted)
                throw new Exception("Move not yet completed");
            return Encoding.ASCII.GetString(_makeMoveJob.AiMove.ToArray());
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


