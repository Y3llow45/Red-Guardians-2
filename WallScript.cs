using UnityEngine;
using TMPro;

public class MathWall : MonoBehaviour
{
    public TMPro.TMP_Text mathText;
    private bool triggered = false; // trigger once

    private string mathSymbol;
    private int value;

    private void Start()
    {
        GenerateRandomMath();
    }

    private void GenerateRandomMath()
    {
        if (mathText == null)
        {
            Debug.LogError("MathWall: TextMeshProUGUI is not assigned!");
            return;
        }

        string[] weightedOptions = { "+1", "+2", "+3", "-1", "-2", "-3", "+4", "-4", "/2", // More likely options
                                     "*2", "/3", "*3", "+5", "-5" }; // Less likely options

        int[] weights = { 8, 8, 8, 8, 8, 8, 6, 6, 10,  // Higher weight means more likely
                          4, 3, 3, 2, 2 }; // Lower weight means less likely

        mathText.text = GetWeightedRandom(weightedOptions, weights);
    }

    private string GetWeightedRandom(string[] options, int[] weights)
    {
        int totalWeight = 0;
        foreach (int weight in weights) totalWeight += weight;

        int randomValue = Random.Range(0, totalWeight);
        int currentSum = 0;

        for (int i = 0; i < options.Length; i++)
        {
            currentSum += weights[i];
            if (randomValue < currentSum)
                return options[i];
        }
        return options[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;
            ApplyMathEffect();
            Debug.Log($"##############Math effect applied: {mathText.text}");
        }
    }

    private void ApplyMathEffect()
    {
        GameController gameController = FindObjectOfType<GameController>();
        if (gameController == null) return;

        string formula = mathText.text;
        char operation = formula[0];
        int number = int.Parse(formula.Substring(1));

        switch (operation)
        {
            case '+':
                gameController.SpawnSoldiers(number);
                break;
            case '-':
                gameController.RemoveSoldiers(number);
                break;
            case '*':
                gameController.MultiplySoldiers(number);
                break;
            case '/':
                gameController.DivideSoldiers(number);
                break;
        }
    }
}