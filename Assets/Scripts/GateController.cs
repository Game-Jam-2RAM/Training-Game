using UnityEngine;

public class GateController : MonoBehaviour
{
    [Tooltip("The symbol displayed above this gate.")]
    public SymbolType gateSymbol;

    [Tooltip("The Renderer component of the gate to change its color.")]
    public Renderer gateRenderer;

    [Tooltip("The color to set the gate to when the symbol matches.")]
    public Color matchedColor = Color.blue;

    [Tooltip("The default color of the gate when the symbol does not match.")]
    public Color defaultColor = Color.white; // Or whatever your gate's initial color is

    void Start()
    {
        // Ensure the renderer is assigned, otherwise try to get it from the GameObject
        if (gateRenderer == null)
        {
            gateRenderer = GetComponent<Renderer>();
            if (gateRenderer == null)
            {
                Debug.LogError("GateController: No Renderer found on " + gameObject.name + ". Please assign one or add a Renderer component.");
            }
        }

        // Set the initial color of the gate
        if (gateRenderer != null)
        {
            gateRenderer.material.color = defaultColor;
        }
    }

    // Method to update the gate's color based on a match
    public void SetGateColor(bool isMatched)
    {
        if (gateRenderer != null)
        {
            gateRenderer.material.color = isMatched ? matchedColor : defaultColor;
        }
    }
}