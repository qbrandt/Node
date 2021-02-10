#include "pch.h"
#include "framework.h"
#include"AI External.h"


AI* Internal_CreateAI()
{
    AI* obj = new AI();
    // you might want to store the object reference on the native side for tracking
    return obj;
}
void Internal_DestroyAI(AI* obj)
{
    // may need to update your tracking in native code
    delete obj;
}
void Internal_AI_GetBoard(AI* obj, const char* board)
{
    obj->GetBoard(board);
}

char* Internal_AI_RandomMove(AI* obj, const char* move)
{
    return _strdup(obj->RandomMove(string(move)).c_str());
}

char* Internal_AI_SmartMove(AI* obj, const char* move)
{
    return _strdup(obj->SmartMove(string(move)).c_str());
}
