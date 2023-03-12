using HarmonyLib;
using NoIntro;
using UnityEngine;

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
}
