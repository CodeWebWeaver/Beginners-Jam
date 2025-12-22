using NUnit.Framework;
using System;
using System.Collections.Generic;

public class RandomService : IRandomService {
    private readonly Random _random;
    private readonly object _lock = new object();
    private bool _disposed;

    public RandomService(RandomServiceSettings settings) {
        _random = new Random(settings.Seed);
    }

    // Копіювання основних методів
    public int Next(int minValue, int maxValue) {
        lock (_lock) {
            return _random.Next(minValue, maxValue);
        }
    }

    public int Next(int maxValue) {
        lock (_lock) {
            return _random.Next(maxValue);
        }
    }

    public int Next() {
        lock (_lock) {
            return _random.Next();
        }
    }

    public double NextDouble() {
        lock (_lock) {
            return _random.NextDouble();
        }
    }

    public void NextBytes(byte[] buffer) {
        lock (_lock) {
            _random.NextBytes(buffer);
        }
    }
    public T GetRandomFromArray<T>(T[] list) {
        if (list == null || list.Length == 0) {
            throw new ArgumentException("List cannot be null or empty", nameof(list));
        }
        int index = Next(0, list.Length);
        return list[index];
    }

    public T GetRandomFromList<T>(List<T> list) {
        if (list == null || list.Count == 0) {
            throw new ArgumentException("List cannot be null or empty", nameof(list));
        }
        int index = Next(0, list.Count);
        return list[index];
    }
}