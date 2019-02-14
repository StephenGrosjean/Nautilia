using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Points : MonoBehaviour
{
    [SerializeField] private int score;

    public void AddScore(int value) {
        score += value;
    }

    public int GetScore() {
        return score;
    }
}
