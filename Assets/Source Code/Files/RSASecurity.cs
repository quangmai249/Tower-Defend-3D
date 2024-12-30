using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEngine;
public class RSASecurity
{
    private static int p = 139;
    private static int q = 163;
    private static int e = 16451;

    private static Dictionary<int, int> pu = GetPublicKey(p, q, e);
    private static Dictionary<int, int> pr = GetPrivateKey(p, q, e);
    public static string Encrypt(string m)
    {
        string res = string.Empty;
        foreach (var temp in m)
            res += Convert.ToChar((int)(BigInteger.Pow((int)temp, pu.FirstOrDefault().Key) % pu.FirstOrDefault().Value));
        return res;
    }
    public static string Decrypt(string c)
    {
        string res = string.Empty;
        foreach (int temp in c)
            res += Convert.ToChar((int)(BigInteger.Pow(temp, pr.FirstOrDefault().Key) % pr.FirstOrDefault().Value));
        return res;
    }
    private static Dictionary<int, int> GetPublicKey(int p, int q, int e)
    {
        Dictionary<int, int> res = new Dictionary<int, int>();
        res.Add(e, p * q);
        return res;
    }
    private static Dictionary<int, int> GetPrivateKey(int p, int q, int e)
    {
        Dictionary<int, int> res = new Dictionary<int, int>();

        int d = 0;
        while (true)
        {
            d++;
            if ((e * d) % ((p - 1) * (q - 1)) == 1)
                break;
        }

        res.Add(d, p * q);

        return res;
    }
    /*
    private static int randomPrimeNumber(int p, int q)
    {
        int res = 0;
        while (true)
        {
            res = Random.Range(1, (p - 1) * (q - 1));
            if (isPrime(res) && gcd(res, (p - 1) * (q - 1)) == 1)
                return res;
        }
    }
    private static bool isPrime(int number)
    {
        if (number <= 1)
            return false;
        if (number <= 3)
            return true;
        if (number % 2 == 0 || number % 3 == 0)
            return false;
        for (int i = 5; i * i <= number; i += 6)
        {
            if (number % i == 0 || number % (i + 2) == 0)
                return false;
        }
        return true;
    }
    private static int gcd(int a, int b)
    {
        int temp;
        while (b != 0)
        {
            temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
     */
}
