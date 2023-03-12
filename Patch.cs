using HarmonyLib;
using Il2CppInterop.Runtime;
using Il2CppSystem;
using NoIntro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

class Patch
{
    private static bool skipMovie = false;

    [HarmonyPatch(typeof(MainTitle))]
    [HarmonyPatch("Start")]
    [HarmonyPrefix]
    static void preStart()
    {
        GameObject logo = GameObject.Find("Logo");
        UnityEngine.Object.Destroy(logo);
        Plugin.Logger.LogMessage("Logo destroyed");
    }

    [HarmonyPatch(typeof(Movie))]
    [HarmonyPatch("Play")]
    [HarmonyPrefix]
    static void preMovieStart(string movie_path, Il2CppSystem.Action call_back, PadManager.BUTTON skipButton)
    {
        Plugin.Logger.LogMessage(movie_path);

        skipMovie = movie_path == "movie00";

        return;
    }

    [HarmonyPatch(typeof(Movie))]
    [HarmonyPatch("Play")]
    [HarmonyPostfix]
    static void postMovieStart()
    {
        if (skipMovie)
        {
            try
            {
                MainTitle t = GameObject.Find("scene/titlescene").GetComponent<MainTitle>();
                t.m_movie._PlayEnd();
                Plugin.Logger.LogMessage("Movie skipped");
            }
            catch (System.Exception ex)
            {
                Plugin.Logger.LogMessage(ex.Message);
            }
        }
        return;
    }

    [HarmonyPatch(typeof(SceneManager))]
    //[HarmonyPatch("Push")]
    //[HarmonyPatch("Clear")]
    //[HarmonyPatch("CurrentSceneDestroy")]
    //[HarmonyPatch("CurrentScenePause")]
    //[HarmonyPatch("ApplicationQuit")]
    //[HarmonyPatch("RebootApplication")]
    //[HarmonyPatch("OnFadeoutOfReboot")]
    //[HarmonyPatch("Awake")]
    //[HarmonyPatch("Start")]
    //[HarmonyPatch("idle")]
    //[HarmonyPatch("read")]
    //[HarmonyPatch("OnFinishedLoad")]
    //[HarmonyPatch("OnDestroy")]
    //[HarmonyPatch("_DestroyCurrentScene")]
    //[HarmonyPatch("Update")]
    [HarmonyPatch(new System.Type[] { })]
    [HarmonyPostfix]
    static void alala(MethodBase __originalMethod)
    {
        Plugin.Logger.LogMessage(__originalMethod.Name + " called");
    }


    //UI Root/ui/common_message_window_ui_root/ui/common_message_window_ui_00_R/Root/Anim/

    // uCommonMessageWindow;

    // UIRoot;



}
