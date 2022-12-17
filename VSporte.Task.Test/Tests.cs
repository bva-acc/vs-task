using Newtonsoft.Json;
using NUnit.Framework;
using System;
using VSporte.Task.Solution;
using VSporte.Task.Solution.Models;
using VSporte.Task.Test.Providers;

namespace VSporte.Task.Test;

public class Tests
{
    private Fingerprint fingerprint1 = null!;
    private Fingerprint fingerprint2 = null!;
    private Fingerprint fingerprint3 = null!;
    private Fingerprint correctFingerprint1 = null!;
    private Fingerprint correctFingerprint2 = null!;
    private Fingerprint correctFingerprint3 = null!;
    private DuplicateResolver duplicateResolver = new();

    [SetUp]
    public void Setup()
    {
        var fingerprintProvider = new FingerprintProvider();
        fingerprint1 = fingerprintProvider.Get(1);
        fingerprint2 = fingerprintProvider.Get(2);
        fingerprint3 = fingerprintProvider.Get(3);

        correctFingerprint1 = fingerprintProvider.Get(1, true);
        correctFingerprint2 = fingerprintProvider.Get(2, true);
        correctFingerprint3 = fingerprintProvider.Get(3, true);

        var duplicateResolver = new DuplicateResolver();
    }

    [Test]
    //<summary>Задание 1.1</summary>
    public void Task1_1()
    {
        
        var resolve = duplicateResolver.Resolve(fingerprint1);
        Assert.AreEqual(JsonConvert.SerializeObject(resolve), JsonConvert.SerializeObject(correctFingerprint1));

    }

    [Test]
    //<summary>Задание 1.2</summary>
    public void Task1_2()
    {
        var resolve = duplicateResolver.Resolve2(fingerprint2);
        Assert.AreEqual(JsonConvert.SerializeObject(resolve), JsonConvert.SerializeObject(correctFingerprint2));
    }

    [Test]
    //<summary>Задание 1.3</summary>
    public void Task1_3()
    {
        var resolve = duplicateResolver.Resolve3(fingerprint3);
        Assert.AreEqual(JsonConvert.SerializeObject(resolve), JsonConvert.SerializeObject(correctFingerprint3));
    }
}