using UnityEngine;
using System.Collections.Generic;
using System.Linq; // Required for LINQ methods like ToArray() and FirstOrDefault()

public class SymbolMatcher : MonoBehaviour
{
    [Tooltip("Reference to the StoneController that contains the primary symbols for comparison.")]
    public StoneController referenceStone;

    [Tooltip("Optional: If set, only gates with this specific tag will be considered for matching.")]
    public string gateTagFilter = "Gate"; // Default tag for gates

    void Start()
    {
        // It's good practice to call the matching logic when the game starts
        // or when the relevant game state changes.
        // For demonstration, we'll call it in Start.
        // In a real game, you might call this when a puzzle is activated, or a level loads.
        PerformSymbolMatching();
    }

    // This method performs the symbol matching and updates gate colors
    public void PerformSymbolMatching()
    {
        if (referenceStone == null)
        {
            Debug.LogError("SymbolMatcher: Reference Stone is not assigned. Please assign a StoneController to the 'Reference Stone' field.");
            return;
        }

        // Get the first symbol from the reference stone
        SymbolType referenceSymbol = referenceStone.GetFirstSymbol();

        if (referenceSymbol == SymbolType.None)
        {
            Debug.LogWarning("SymbolMatcher: Reference stone has no symbols defined. No comparison will be performed.");
            return;
        }

        Debug.Log($"SymbolMatcher: Using reference symbol: {referenceSymbol} from stone: {referenceStone.name}");

        // Find all GateControllers in the scene, optionally filtered by tag
        GateController[] gates;
        if (!string.IsNullOrEmpty(gateTagFilter))
        {
            // Find GameObjects with the specified tag, then get their GateController components
            GameObject[] taggedGates = GameObject.FindGameObjectsWithTag(gateTagFilter);
            List<GateController> filteredGates = new List<GateController>();
            foreach (GameObject go in taggedGates)
            {
                GateController gc = go.GetComponent<GateController>();
                if (gc != null)
                {
                    filteredGates.Add(gc);
                }
            }
            gates = filteredGates.ToArray();
        }
        else
        {
            // Find all GateControllers if no tag filter is specified
            gates = FindObjectsOfType<GateController>();
        }

        if (gates.Length == 0)
        {
            Debug.LogWarning("SymbolMatcher: No GateControllers found in the scene (or with the specified tag). No gates will be colored.");
            return;
        }

        // Iterate through each gate and compare its symbol with the reference symbol
        foreach (GateController gate in gates)
        {
            bool isMatched = (gate.gateSymbol == referenceSymbol);
            gate.SetGateColor(isMatched);
            Debug.Log($"SymbolMatcher: Gate '{gate.gameObject.name}' with symbol '{gate.gateSymbol}' is {(isMatched ? "matched" : "not matched")} with reference symbol '{referenceSymbol}'.");
        }

        Debug.Log("SymbolMatcher: Comparison and coloring complete.");
    }

    // You might want to add a public method to trigger this from other scripts
    // For example, when the player interacts with something that reveals the stone's symbol.
    public void TriggerSymbolCheck()
    {
        PerformSymbolMatching();
    }
}