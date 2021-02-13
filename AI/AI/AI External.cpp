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
void Internal_AI_GameSetup(AI* obj, const char* board, bool goFirst, bool isSmart)
{
    obj->GameSetup(board, goFirst, isSmart);
}
char* Internal_AI_GetMove(AI* obj, const char* move)
{
    return _strdup(obj->GetMove(string(move)).c_str());
}
