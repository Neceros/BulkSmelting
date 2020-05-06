using HarmonyLib;
using System.Reflection;
using Verse;

namespace BulkSmelting
{
  [StaticConstructorOnStartup]
  public static class StartUp
  {
    static StartUp()
    {
      var harmony = new Harmony("BulkSmelting.neceros");
      harmony.PatchAll(Assembly.GetExecutingAssembly());

      harmony.Patch(original: AccessTools.Method(type: typeof(Settlement_TraderTracker), name: nameof(Settlement_TraderTracker.GiveSoldThingToPlayer)),
                  transpiler: new HarmonyMethod(typeof(StartUp), nameof(GiveSoldThingsToSRTSTranspiler)));
    }
  }
}
