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
        private static ManagedObjectWorld _refs = new ManagedObjectWorld();
        public JobHandle MakeMoveHandle { get; set; }
        private MakeMoveJob _makeMoveJob;

        struct MakeMoveJob : IJob
        {
            
            [NativeDisableUnsafePtrRestriction]
            public IntPtr AiPtr;
            [ReadOnly]
            public ManagedObjectRef<string> PlayerMove;

            public NativeArray<ManagedObjectRef<string>> AiMove;

            public void Execute()
            {                
                var result = Internal_AI_GetMove(AiPtr, _refs.Get(PlayerMove));
                var resultRef = _refs.Add(result);
                AiMove[0] = resultRef;
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

        public void GameSetup(string board, bool aiGoesFirst, bool isSmart = true)
        {
            Debug.Log($@"AI Game Setup
Board: {board}
AI goes {(aiGoesFirst ? "first" : "second")}
AI is {(isSmart ? "smart" : "dumb")}");
            if (m_AI == IntPtr.Zero)
                throw new Exception("No native object");
            Internal_AI_GameSetup(m_AI, board, aiGoesFirst, isSmart);
        }

        public void MakeMove(string move)
        {
            Debug.Log($"Move given: {move}");

            if (m_AI == IntPtr.Zero)
                throw new Exception("No native AI object");

            var nativeMove = _refs.Add(move);
            var nativeAiMove = new NativeArray<ManagedObjectRef<string>>(1, Allocator.TempJob);

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
            MakeMoveHandle.Complete();
            var resultRef = _makeMoveJob.AiMove[0];
            var result =_refs.Get(resultRef);
            _refs.Remove(resultRef);

            _makeMoveJob.AiMove.Dispose();

            Debug.Log($"Move recieved: {result}");

            return result;
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


