```

BenchmarkDotNet v0.13.7, macOS Ventura 13.5 (22G74) [Darwin 22.6.0]
Apple M1 Max, 1 CPU, 10 logical and 10 physical cores
.NET SDK 7.0.306
  [Host]     : .NET 7.0.9 (7.0.923.32018), Arm64 RyuJIT AdvSIMD
  DefaultJob : .NET 7.0.9 (7.0.923.32018), Arm64 RyuJIT AdvSIMD


```
|     Method |     Mean |    Error |   StdDev |      Gen0 |     Gen1 | Allocated |
|----------- |---------:|---------:|---------:|----------:|---------:|----------:|
|   CoreRepo | 57.30 ms | 1.141 ms | 1.842 ms | 1000.0000 |        - |   6.46 MB |
| ModernRepo | 58.52 ms | 1.161 ms | 2.345 ms | 1111.1111 | 333.3333 |    7.2 MB |
