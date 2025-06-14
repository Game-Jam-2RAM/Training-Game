using UnityEngine;
using System.Collections.Generic;

public class StoneController : MonoBehaviour
{
    [Tooltip("The list of symbols on this stone. The first symbol (index 0) will be used for comparison.")]
    public List<SymbolType> stoneSymbols = new List<SymbolType>();

    // Helper to get the first symbol for comparison
    public SymbolType GetFirstSymbol()
    {
        if (stoneSymbols != null && stoneSymbols.Count > 0)
        {
            return stoneSymbols[0];
        }
        return SymbolType.None; // Return None if no symbols are defined
    }
}