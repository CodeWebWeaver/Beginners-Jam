using System;

public interface IRandomService {
    int Next();
    int Next(int maxValue);
    int Next(int minValue, int maxValue);
    void NextBytes(byte[] buffer);
    double NextDouble();
}