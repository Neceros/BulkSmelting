using HarmonyLib;
using System.Collections.Generic;
using System.Reflection;
using Verse;

namespace BulkSmelting
{

    public static IEnumerable<Thing> MoreMeatPlease(IEnumerable<Thing> __result)
    {
      foreach (var thing in __result)
      {
        if (thing.def.IsMeat)
        {
          thing.stackCount = (thing.stackCount * GiveMeMeatSettings.MeatAmountToMultiply);
          yield return thing;
        }   

        yield return thing;
      }
    }

    public static IEnumerable<CodeInstruction> ChangeCorpseDisplay(IEnumerable<CodeInstruction> instructions)
    {
      List<CodeInstruction> instructionList = instructions.ToList();
      int amountToMul = GiveMeMeatSettings.MeatAmountToMultiply;

      for (int i = 0; i < instructionList.Count; i++)
      {
        CodeInstruction instruction = instructionList[i];

        if (instruction.opcode == OpCodes.Stloc_S &&
            instruction.OperandIs(AccessTools.Method(type: typeof(Corpse), name: nameof(Corpse.SpecialDisplayStats))))
        {
          Log.Message("GMM: Corpse Patch");
          yield return new CodeInstruction(opcode: OpCodes.Ldc_I4, operand: amountToMul);
          yield return new CodeInstruction(opcode: OpCodes.Mul);
        }

        yield return instruction;
      }
    }
  }
}
