using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs;


internal class Program
{
    private static void Main(string[] args)
    {
        BenchmarkDotNet.Running.BenchmarkRunner.Run<OddRegression>();
    }
}

[MemoryDiagnoser]
[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
[SimpleJob(RuntimeMoniker.Net80, baseline: true)]
[SimpleJob(RuntimeMoniker.Net90)]
[SimpleJob(RuntimeMoniker.Net10_0)]
public class OddRegression
{
    private byte[] first = null!;
    private byte[] second = null!;


    [Params(1073741824)]
    public int Length { get; set; }

    [GlobalSetup]
    public void Setup()
    {
        var r = new Random(0);

        first = new byte[Length];
        second = new byte[Length];

        r.NextBytes(first);
        Array.Copy(first, second, Length);
    }

    [Benchmark]
    public void Loop()
    {
        EqualsLoop(first, second);
    }

    public static bool EqualsLoop(byte[] b1, byte[] b2)
    {

        if (b1.Length != b2.Length) return false;
        for (int i = 0; i < b1.Length; i++)
        {
            if (b1[i] != b2[i]) return false;
        }

        return true;
    }

}