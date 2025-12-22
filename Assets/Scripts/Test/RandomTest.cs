using UnityEngine;
using Zenject;

public class RandomTest : MonoBehaviour
{
    [Inject] private IRandomService _randomService;

    private void Start()
    {
        Debug.Log("Random number: " + _randomService.Next(1, 100));
    }
}
