#pragma once

/* The following ifdef block is the standard way of creating macros which make exporting from a DLL simpler.
All files within this DLL are compiled with the NATIVECPPLIBRARY_EXPORTS symbol defined on the command line.
This symbol should not be defined on any project that uses this DLL. This way any other project whose source
files include this file see NATIVECPPLIBRARY_API functions as being imported from a DLL, whereas this DLL
sees symbols defined with this macro as being exported.
*/

#ifdef NATIVECPPLIBRARY_EXPORTS
#define NATIVECPPLIBRARY_API __declspec(dllexport)
#else
#define NATIVECPPLIBRARY_API __declspec(dllimport)
#endif

#include "AI.h"

extern "C" {
    NATIVECPPLIBRARY_API AI* Internal_CreateAI();
    NATIVECPPLIBRARY_API void Internal_DestroyAI(AI* obj);
    NATIVECPPLIBRARY_API void Internal_AI_GetBoard(AI* obj, const char* board);
    NATIVECPPLIBRARY_API char* Internal_AI_RandomMove(AI* obj, const char* move);
    NATIVECPPLIBRARY_API char* Internal_AI_SmartMove(AI* obj, const char* move);
}